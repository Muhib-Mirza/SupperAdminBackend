using APL.API.Extensions.Enums;

using LMS.API.DataModel;
using LMS.Repository.Authentication.Interfaces;

using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ancera.API.IntEngine.Utils
{
    public class ControllerHelpingFunctions
    {

      

        public static string generateToken(UserDM userFromRepo, IConfiguration _config, long sessionID = 0)
        {

            List<string> lstTenantIds = new List<string>();

           

            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier, userFromRepo.ID.ToString() ?? "DefaultUserId")

                //new Claim(ClaimTypes.Name, userFromRepo.userEmail),
                //new Claim (ClaimTypes.Role, userFromRepo.userRoleName)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sffsdfasdfasdfasdfasdfsdfadfffffffffffffffffffffffffffadfasdfasdfasdfsdfasda"));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            string expireyTime = _config.GetSection("AppSettings")["TokenExpireyHours"];
            double seconds = 5200;

            var tokenDescription = new JwtSecurityToken(
                issuer: "https://localhost:44344/",
                audience: "https://localhost:44365/",
                claims: claims,
                expires: DateTime.Now.AddSeconds(seconds),
                signingCredentials: cred
              );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescription);
            return token;
        }
        public static string refreshToken(string ActualToken, IConfiguration _config)
        {
            string token = ActualToken;
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(ActualToken);
            var claims = jwtToken.Claims;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sffsdfasdfasdfasdfasdfsdfadfffffffffffffffffffffffffffadfasdfasdfasdfsdfasda"));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            string expireyTime = _config.GetSection("AppSettings")["TokenExpireyHours"];
            double seconds = 52000;

            var tokenDescription = new JwtSecurityToken(
                 issuer: "https://localhost:44344/",
                audience: "https://localhost:44365/",
                claims: claims,
                expires: DateTime.Now.AddSeconds(seconds),
                signingCredentials: cred
              );

            token = new JwtSecurityTokenHandler().WriteToken(tokenDescription);
            return token;
        }
        public static ActiveUserSessionDM ParseRequestHeader(IHeaderDictionary headers, string token, IConfiguration _config, IAuthRepository _authRepository)
        {
            try
            {


                string userId = string.Empty;
                string tokenLcl = string.Empty;
                string sessionID = string.Empty;
                IHeaderDictionary headersLcl = headers;
                tokenLcl = token;
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var lstClaims = jwtToken.Claims.ToList();

                Claim claim = lstClaims.Find(x => x.Type == ClaimTypes.NameIdentifier);
                userId = claim.Value;
                claim = lstClaims.Find(x => x.Type == ClaimTypes.Sid);
                if (claim != null)
                    sessionID = claim.Value;
                ActiveUserSessionDM activeUserSessionDM = new ActiveUserSessionDM();
                activeUserSessionDM.userId = long.Parse(userId);
          


                


                return activeUserSessionDM;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




    }
}
