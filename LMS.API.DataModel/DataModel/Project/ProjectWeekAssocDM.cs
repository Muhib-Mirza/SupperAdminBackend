using LMS.API.DataModel.ViewModel.Project;
using LMS.API.Extensions.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.DataModel.Project
{
    public class ProjectWeekAssocDM:BaseDataModel
    {
        public ProjectWeekAssocDM(ProjectWeekAssocVMV2 obj)
        {
            this.project_week_id = obj.project_week_id;
            this.project_id = obj.project_id;
            this.week_id = obj.week_id;
            this.lesson_plan = obj.lesson_plan; 
            this.objectives = obj.objectives;
            this.activities = obj.activities;
            this.projectTools  =new List<ProjectToolsAssocDM>();
        }
        public int project_week_id { get; set; }
        public long project_id { get; set; }
        public int week_id { get; set; }
        public string lesson_plan { get; set; }
        public string objectives { get; set; }
        public string activities { get; set; }
        public List<ProjectToolsAssocDM> projectTools { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime modification_date { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
    }
}
