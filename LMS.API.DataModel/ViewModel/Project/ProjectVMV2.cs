using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.API.DataModel.ViewModel.Project
{
    public class ProjectVMV2 : BaseViewModel
    {
        public int? projectId { get; set; }
        public string projectName { get; set; }
        public string projectDescription { get; set; }
        public string? gradeLevel { get; set; }
        public string? prerequisiteLearnings { get; set; }
        public string? curriculumPoints { get; set; }
        public string? IndustrySpecificProblem { get; set; }
        public string? ProjectDuration { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public List<ProjectImagesVM> projectImagesList { get; set; }
        public List<ProjectWeekAssocVMV2> projectWeek { get; set; }
        public List<ProjectGradeAssocVM> projectGrades { get; set; }
        public List<ProjectIndustryAssocVM> projectIndustry { get; set; }
        public List<ProjectRoleAssocVMV2> projectRole { get; set; }
        public List<ProjectSubjectAssocVMV2> projectSubject { get; set; }
    }
}
