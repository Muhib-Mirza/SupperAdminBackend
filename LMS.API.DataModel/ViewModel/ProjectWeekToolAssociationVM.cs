using APL.API.Extensions.DataModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel
{
    public class ProjectWeekToolAssociationVM : BaseViewModel, IModel
    {
        public int ProjectWeekToolId { get; set; }
        public int ProjectId { get; set; }
        public int WeekId { get; set; }
        public int ToolId { get; set; }
    }
}
