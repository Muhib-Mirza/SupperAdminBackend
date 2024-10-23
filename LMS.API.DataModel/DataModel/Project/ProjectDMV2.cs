using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.API.Extensions.DataModel;

namespace LMS.API.DataModel.DataModel.Project
{
    public class ProjectDMV2 : BaseDataModel
    {
        public int projectId { get; set; }
        public string projectName { get; set; }
        public string projectDescription { get; set; }
        public string gradeLevel { get; set; }
        public string prerequisiteLearnings { get; set; }
        public string curriculumPoints { get; set; }
        public string? IndustrySpecificProblem { get; set; }
        public string? ProjectDuration { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string projectImagesList { get; set; }
        public int roleId { get; set; }
        public string roleName { get; set; }
        public int subjectID { get; set; }
        public string subjectName { get; set; }
        public int industryId { get; set; }
        public string industryName { get; set; }
        public int GradeID { get; set; }
        public string GradeName { get; set; }
        public int Week_id { get; set; }
        public string Week_name { get; set; }
        public string lesson_plan { get; set; }
        public string objectives { get; set; }
        public string step_caption { get; set; }
        public string activities { get; set; }
        public int toolId { get; set; }
        public string toolName { get; set; }
        public int image_id { get; set; }
        public string image { get; set; }

    }
}
