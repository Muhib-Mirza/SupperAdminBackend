using Microsoft.AspNetCore.Mvc;

namespace LMS.API.IntEngine.Utils
{
    public class CustomControllerBase : ControllerBase
    {
        public readonly IConfiguration _config;

        public CustomControllerBase(IConfiguration config)
        {
            _config = config;
        }

    }
}
