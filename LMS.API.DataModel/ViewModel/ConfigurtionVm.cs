using APL.API.Extensions.DataModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel
{
    public class ConfigurtionVm : BaseViewModel, IModel
    {
            public int Id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
    }
}
