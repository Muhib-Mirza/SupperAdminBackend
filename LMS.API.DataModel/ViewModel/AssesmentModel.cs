using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APL.API.Extensions.DataModel;

namespace LMS.API.DataModel.ViewModel
{
    public class AssesmentModel : BaseViewModel, IModel
    {
        public AssesmentModel()
        {
        }
        public string Code { get; set; }
        public int UserId { get; set; }
    }
}
