using LMS.Repository.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LMS.API.IntEngine.Utils;
using LMS.API.ViewModel;
using LMS.API.Extensions.Service;
using LMS.API.Extensions.Enums;
using LMS.API.DataModel;
using APL.API.Extensions.Service.GenericRequestObjects;
using Ancera.API.IntEngine.Utils;

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
        

              
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserVM input)
        {
            ResponseObject<UserVM> response = new ResponseObject<UserVM>();

            try
            {
                UserDM repoResult = _repo.Add(input);
                input.EmailId = repoResult.EmailId;
            }
            catch (Exception ex)
            {

                return Ok(ExceptionHandler.Proceed(ex));
            }

            response.setDataObject(MessageCodeEnum.CREATE_SUCCESS, input, PlaceHoldersEnum.Data, PlaceHoldersEnum.User.GetDescription().ToLower());

            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthenticationObject authObject)
        {

            ResponseObject<AuthenticationObject> response = new ResponseObject<AuthenticationObject>();

            try
            {

                string ip = Convert.ToString(Request.HttpContext.Connection.RemoteIpAddress);
               
                UserDM userFromRepo = _repo.Login(authObject);


                



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

        #endregion

    }
}