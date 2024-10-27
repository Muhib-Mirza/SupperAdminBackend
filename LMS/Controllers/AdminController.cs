using LMS.Repository.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LMS.API.IntEngine.Utils;
using LMS.API.ViewModel;
using LMS.API.Extensions.Service;
using LMS.API.Extensions.Enums;
using LMS.API.DataModel;
using APL.API.Extensions.Service.GenericRequestObjects;
using Ancera.API.IntEngine.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LMS.API.DataModel.ViewModel;

namespace LMS.API.IntEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : CustomControllerBase
    {
        private readonly IUserRepository _repo;
       


        public readonly IConfiguration _config;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="config"></param>
        public AdminController(IUserRepository repo, IConfiguration config) : base(config)
        {
            _repo = repo;
            _config = config;

        }

        
       #region Create User 
        /// <summary>
        /// Load Reference Data for Screens: Create User, Update User
        /// </summary>
        /// <returns></returns>
        

              
        //[HttpPost("CreateUser")]
        //public async Task<IActionResult> CreateUser([FromBody] UserVM input)
        //{
        //    ResponseObject<UserVM> response = new ResponseObject<UserVM>();

        //    try
        //    {
        //        UserDM repoResult = _repo.Add(input);
        //        input.EmailId = repoResult.EmailId;
        //    }
        //    catch (Exception ex)
        //    {

        //        return Ok(ExceptionHandler.Proceed(ex));
        //    }

        //    response.setDataObject(MessageCodeEnum.CREATE_SUCCESS, input, PlaceHoldersEnum.Data, PlaceHoldersEnum.User.GetDescription().ToLower());

        //    return Ok(response);
        //}
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthenticationObject authObject)
        {

            ResponseObject<AuthenticationObject> response = new ResponseObject<AuthenticationObject>();

            try
            {

                string ip = Convert.ToString(Request.HttpContext.Connection.RemoteIpAddress);

                UserDM userFromRepo = _repo.Login(authObject);
                authObject.Password = null;
                authObject.EmailId = null;
                authObject.userId = userFromRepo.UserId;
                authObject.isAssesed = userFromRepo.isAssesed;

                // authObject.userId = userFromRepo.ID;



                //userFromRepo.tenantId
                var token = ControllerHelpingFunctions.generateToken(userFromRepo, _config, authObject.userId);


                authObject.token = token;

                response.setDataObject(API.Extensions.Enums.MessageCodeEnum.LOGIN_SUCCESS, authObject);
            }
            catch (Exception ex)
            {
                return Ok(ExceptionHandler.Proceed(ex));
            }

            return Ok(response);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserModel Obj)
        {
            UserDM repoResult = _repo.Add(Obj);
            ResponseObject<UserModel> response = new ResponseObject<UserModel>();
            Obj.Password = repoResult.Password;
            Obj.EmailId = repoResult.EmailId;
            Obj.UserId = repoResult.ID;
            response.setDataObject(MessageCodeEnum.CREATE_SUCCESS, Obj, PlaceHoldersEnum.Data, PlaceHoldersEnum.User.GetDescription().ToLower());
            return Ok(response);
        }

        [HttpPost("AddAssesment")]
        public async Task<IActionResult> AddAssesment(AssesmentModel Obj)
        {
            bool result = _repo.AssesMe(Obj);
            return Ok(result);
        }

        #endregion


        [HttpPost("refreshToken")]
        public async Task<IActionResult> refreshToken(AuthenticationObject authObject)
        {
            ResponseObject<AuthenticationObject> response = new ResponseObject<AuthenticationObject>();

            try
            {
                bool isRefreshAllowed = false;
                authObject.token = authObject.token.Trim();
                string EnableRefreshToken = _config.GetSection("AppSettings")["EnableRefreshToken"];
                if (!string.IsNullOrEmpty(EnableRefreshToken) && EnableRefreshToken.Trim().ToLower().Equals("true"))
                {
                    long sessionID = new long();
                    var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(authObject.token);
                    var lstClaims = jwtToken.Claims.ToList();
                    Claim claim = lstClaims.Find(x => x.Type == ClaimTypes.Sid);
                    if (claim != null)
                        sessionID = Convert.ToInt64(claim.Value);

                    TokenDetailsDM tokenDetails = new TokenDetailsDM();
                    tokenDetails.actvSessionId = sessionID;
                    tokenDetails.token = authObject.token;
                    tokenDetails.lastActivityDateTime = DateTime.Now.ToString();

                    TokenDetailsDM tokenDetailsDM = _repo.GetTokenDetailsBySessionID(tokenDetails);
                    if (tokenDetailsDM != null)
                    {
                        DateTime lastActivityTime = DateTime.Parse(tokenDetailsDM.lastActivityDateTime);
                        double timePassed = (DateTime.Now - lastActivityTime).TotalSeconds;
                        string expireyTime = _config.GetSection("AppSettings")["TokenExpireyHours"];
                        double seconds = 20;
                        if (timePassed <= seconds)
                        {
                            isRefreshAllowed = true;
                        }
                        if (isRefreshAllowed)
                        {
                            string token = ControllerHelpingFunctions.refreshToken(authObject.token, _config);
                            authObject.token = token;
                            tokenDetails.token = token;
                            tokenDetails.lastActivityDateTime = DateTime.Now.ToString();
                            _repo.UpdateTokenDetails(tokenDetails);
                        }

                    }


                }



                response.setDataObject(MessageCodeEnum.LOGIN_SUCCESS, authObject);
            }
            catch (Exception ex)
            {
                return Ok(ExceptionHandler.Proceed(ex));
            }
            return Ok(response);
        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut(AuthenticationObject authObject)
        {
            ResponseObject<AuthenticationObject> response = new ResponseObject<AuthenticationObject>();

            try
            {
                authObject.token = authObject.token.Trim();
                TokenDetailsDM tokenDetails = new TokenDetailsDM();
                tokenDetails.token = authObject.token;
                tokenDetails.lastActivityDateTime = DateTime.Now.ToString();



                _repo.LogOut(tokenDetails);

            }
            catch (Exception ex)
            {
                return Ok(ExceptionHandler.Proceed(ex));
            }



            return Ok(response);
        }


    }
}