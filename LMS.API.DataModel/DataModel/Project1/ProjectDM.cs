using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.API.Extensions.DataModel;

namespace LMS.API.DataModel.DataModel.Project
{
    public class ProjectDM : BaseDataModel
    {
        public int project_id { get; set; }
        public string project_name { get; set; }
        public string project_description { get; set; }
        public int industry_id { get; set; }
        public string grade_level { get; set; }
        public string prerequisite_learnings { get; set; }
        public int curriculum_points { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string[] images { get; set; }
    }
}
