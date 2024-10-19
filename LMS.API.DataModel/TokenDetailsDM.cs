using APL.API.Extensions.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.API.DataModel.ViewModel
{
    public class TokenDetailsDM 
    {
        public TokenDetailsDM()
        {

        }
        public long actvSessionId { get; set; }
        public string token { get; set; }
        public string lastActivityDateTime { get; set; }
      
    }
}
