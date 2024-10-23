using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel.Project
{
    public class ProjectWeekAssocVMV2:BaseViewModel
    {
        public int project_week_id { get; set; }
        public int project_id { get; set; }
        public int week_id { get; set; }
        public string week_name { get; set; }
        public string lesson_plan { get; set; }
        public string step_caption { get; set; }
        public string objectives { get; set; }
        public string activities { get; set; }
        public List<ToolDedailVM> projectTools { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime modification_date { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
    }
}
