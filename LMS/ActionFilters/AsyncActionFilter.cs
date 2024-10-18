using Ancera.API.IntEngine.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System.Text;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using LMS.API.DataModel;
using LMS.Repository.Authentication.Interfaces;

namespace LMS.API.IntEngine.ActionFilters
{
    public class AsyncActionFilter : IAsyncActionFilter, IAuthorizationFilter
    {
        public readonly IConfiguration config;
        private readonly IAuthRepository authRepository;

        public AsyncActionFilter(IConfiguration _config, IAuthRepository _authRepository)
        {
            config = _config;
            //APL.API.Logging.Logger Logger = new APL.API.Logging.Logger(config);
            authRepository = _authRepository;

        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string MobileApplicationAllowed = config.GetValue<string>("MobileApplicationAllowed");
            string controllerName = Convert.ToString(context.RouteData.Values["Controller"]);
            //FarmVMHelper repoResult = new FarmVMHelper();
            //ResponseObject<string> response = new ResponseObject<string>();
            //response.setDataObject(MessageCodeEnum.SELECT_SUCCESS, null);
            switch (MobileApplicationAllowed.ToLower())
            {
                case "sampleapp":
                    if (controllerName != "MsclSampling")
                    {
                        context.Result = new BadRequestObjectResult("Bad Request");
                        return;
                    }
                    break;
                case "enduserapp":
                    if (controllerName == "MsclSampling")
                    {
                        context.Result = new BadRequestObjectResult("Bad Request");
                        return;
                    }
                    break;
            }
            string actionName = Convert.ToString(context.RouteData.Values["Action"]);
            string requestObject = string.Empty;
            string IsActivityLogEnabled = config.GetSection("DISSettings")["IsActivityLogEnabled"];
            bool isActivityLoggingEnabled = false;
            string batchNo = Guid.NewGuid().ToString();
            string correlationId = Guid.NewGuid().ToString();

            if (context.HttpContext.Request.Headers.TryGetValue("X-Correlation-ID", out StringValues correlationIds))
            {
                correlationId = correlationIds.FirstOrDefault();
            }
            else
            {
                context.HttpContext.Request.Headers.Add("X-Correlation-ID", correlationId);
            }
            //correlationId = context.HttpContext.Request.Headers.TryGetValue("X-Correlation-ID", out var values))



            if (!string.IsNullOrEmpty(IsActivityLogEnabled) && IsActivityLogEnabled.Trim().ToLower().Equals("true"))
            {
                isActivityLoggingEnabled = true;
            }
            if (isActivityLoggingEnabled)
            {
                //if (controllerName.ToLower().Equals("mscl") || controllerName.ToLower().Equals("farm"))
                //{
                var MaxVariableSize = 500 * 1024; // 500 KB limit for each variable

                batchNo = Guid.NewGuid().ToString();
                context.HttpContext.Items["batchNo"] = batchNo;
                //if (!actionName.ToLower().Equals("systemlogs") && !actionName.Equals("GetAllProcessingLogs"))
                //{

                try
                {
                    if (context.HttpContext.Request.Body.Length > 0)
                    {
                        using (StreamReader responseReader = new StreamReader(context.HttpContext.Request.Body, encoding: Encoding.UTF8))
                        {
                            responseReader.BaseStream.Seek(0, SeekOrigin.Begin);
                            requestObject = await responseReader.ReadToEndAsync();
                            JObject requestBodyObject = JObject.Parse(requestObject);

                            foreach (JProperty property in requestBodyObject.Properties().ToList())
                            {
                                int propertySize = Encoding.UTF8.GetByteCount(property.Value.ToString());

                                if (propertySize > MaxVariableSize)
                                {
                                    property.Value = JValue.CreateNull();
                                }
                            }

                            requestObject = requestBodyObject.ToString();
                        }
                    }
                 

                }
                catch (Exception ex)
                {

                }


                //}


                //}
            }


            string authHeader = context.HttpContext.Request.Headers["Authorization"];
            string token = string.Empty;
            if (authHeader != null && (authHeader.Contains("Bearer".ToLower()) || authHeader.Contains("Bearer")))
            {
                token = authHeader.Replace("Bearer", "").Replace("bearer", "");
                // Now token can be used for further authorization
            }
            if (!string.IsNullOrEmpty(token))
            {
                ActiveUserSessionDM activeUserSessionDM = ControllerHelpingFunctions.ParseRequestHeader(context.HttpContext.Request.Headers, token.Trim(), config, authRepository);

           

                context.HttpContext.Items["SessionID"] = activeUserSessionDM.actvSessionId;
                context.HttpContext.Items["UserID"] = activeUserSessionDM.userId;
             
            }

            //Logger.LogResponseActivityOnConsole("Request: DIS-API Application for " + controllerName + " " + actionName, actionName, requestObject,"Info", correlationId); 

            ActionExecutedContext result = await next();
             //string response = string.Empty;
            //var resp = (OkObjectResult)result.Result;
            //response = JsonConvert.SerializeObject(resp.Value);
            result.HttpContext.Response.Headers.Add("X-Correlation-Id", correlationId);

            //Logger.LogResponseActivityOnConsole("Response: DIS-API Application logs for " + controllerName + " " + actionName, actionName, response,"Info", correlationId);

        }



        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string actionName = Convert.ToString(context.RouteData.Values["Action"]);
            string controllerName = Convert.ToString(context.RouteData.Values["Controller"]);

            bool userAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            context.HttpContext.Request.EnableBuffering();
        }



    }
}
