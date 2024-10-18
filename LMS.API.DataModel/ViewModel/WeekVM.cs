using APL.API.Extensions.DataModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel
{
    public class WeekVM : BaseViewModel, IModel
    {
        public int WeekId { get; set; }
        public string WeekName { get; set; }
        public string WeekDescription { get; set; }
    }
}
