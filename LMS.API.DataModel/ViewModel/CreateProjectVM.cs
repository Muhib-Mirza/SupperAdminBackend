using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel
{
    namespace LMS.API.ViewModel
    {
        public class CreateProjectVM
        {
            // Project details
            public string ProjectName { get; set; }
            public long? ProjectId { get; set; }
            public string? ProjectDescription { get; set; }
            public string? PrerequisiteLearnings { get; set; }
            public string? CurriculumPoints { get; set; }
            public string? IndustrySpecificProblem { get; set; }
            public string? ProjectDuration { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }

            // Associated data
            public List<long> IndustryIds { get; set; }
            public List<long> GradeIds { get; set; }
            public List<long> RoleIds { get; set; }
            public List<long> SubjectIds { get; set; }
            public List<WeekVM> Weeks { get; set; }

            // Images
            public List<string> ProjectImages { get; set; }
        }

        public class WeekVM
        {
            public long? weekId { get; set; }
            public string lessonPlan { get; set; }
            public string objectives { get; set; }
            public string activities { get; set; }
            public string stepCaption { get; set; }
            public List<long> Tools { get; set; }
        }

        public class ToolVM
        {
            public long ToolId { get; set; }
            public string ToolName { get; set; }
        }
        public class Project_Industry_ASOC_VM
        {
            public long project_id { get; set; }
            public long industry_id { get; set; }
        }

        public class Project_Grade_ASOC_VM
        {
            public long project_id { get; set; }
            public long grade_id { get; set; }
        }

        public class Project_Role_ASOC_VM
        {
            public long project_id { get; set; }
            public long role_id { get; set; }
        }

        public class Project_Subject_ASOC_VM
        {
            public long project_id { get; set; }
            public long subject_id { get; set; }
        }

        public class Project_Week_ASOC_VM
        {
            public long project_id { get; set; }
            public long week_id { get; set; }
        }

        public class Project_Week_Tool_ASOC_VM
        {
            public long project_id { get; set; }
            public long week_id { get; set; }
            public long tool_id { get; set; }
        }
    }

}
