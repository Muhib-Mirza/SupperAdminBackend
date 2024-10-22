using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel
{
    public class AllProjectViewModel
    {
        
            public int ProjectId { get; set; }
            public string ProjectName { get; set; }
            public string ProjectDescription { get; set; }
            public int? IndustryId { get; set; }
            public string GradeLevel { get; set; }
            public string PrerequisiteLearnings { get; set; }
            public string? CurriculumPoints { get; set; }
            public string? IndustrySpecificProblem { get; set; }
            public string? ProjectDuration { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }

            public List<string> ProjectImagesList { get; set; } = new List<string>();
            public List<string> ProjectGrades { get; set; } = new List<string>();
            public List<string> ProjectIndustries { get; set; } = new List<string>();
            public List<string> ProjectRoles { get; set; } = new List<string>();
            public List<string> ProjectSubjects { get; set; } = new List<string>();
            public List<string> ProjectWeeks { get; set; } = new List<string>();
            public List<string> ProjectWeekTools { get; set; } = new List<string>();
       
    }
}
