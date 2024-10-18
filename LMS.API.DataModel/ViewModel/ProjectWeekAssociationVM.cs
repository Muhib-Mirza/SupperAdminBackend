using APL.API.Extensions.DataModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel
{
    public class ProjectWeekAssociationVM : BaseViewModel, IModel
    {
        public int ProjectWeekId { get; set; }
        public int ProjectId { get; set; }
        public int WeekId { get; set; }
        public string LessonPlan { get; set; }
        public string Objectives { get; set; }
        public string Activities { get; set; }
    }
}
