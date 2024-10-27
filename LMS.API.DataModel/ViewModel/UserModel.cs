using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APL.API.Extensions.DataModel;

namespace LMS.API.DataModel.ViewModel
{
    public class UserModel : BaseViewModel, IModel
    {
        public UserModel()
        {
        }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public long UserId { get; set; }
    }
}
