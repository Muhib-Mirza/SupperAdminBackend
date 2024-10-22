using System;
using System.Collections.Generic;

namespace LMS.API.DataModel.ViewModel.Project
{
    public class ProjectVMV1 : BaseViewModel
    {
        public int? ProjectId { get; set; } // Maps to p.project_id
        public int? IndustryId { get; set; } // Maps to p.project_id
        public int? RoleId { get; set; } // Maps to p.project_id
        public int? ToolId { get; set; } // Maps to p.project_id
        public int? GradeId { get; set; } // Maps to p.project_id
        public int? SubjectId { get; set; } // Maps to p.project_id
        public int? WeekId { get; set; } // Maps to p.project_id
        public string RoleName { get; set; } // Maps to p.project_id
        public string ToolName { get; set; } // Maps to p.project_id
        public string WeekLesson { get; set; } // Maps to p.project_id
        public string WeekObjective { get; set; } // Maps to p.project_id
        public string WeekActivities { get; set; } // Maps to p.project_id
        
        public string SubjectName { get; set; } // Maps to p.project_id
        public string GradeName { get; set; } // Maps to p.project_id
        public string? CurriculumPoints { get; set; } // Maps to p.project_id
        public string ProjectName { get; set; } // Maps to p.project_name
        public string IndustryName { get; set; } // Maps to p.project_name
        public string ImageUrl { get; set; } // Maps to p.project_name
        public string ProjectDescription { get; set; } // Maps to p.project_description

        // Industry details
        public List<IndustryDetailVM> Industries { get; set; } = new List<IndustryDetailVM>();

        // Role details
        public List<RoleDetailVM> Roles { get; set; } = new List<RoleDetailVM>();

        // Grade details
        public List<GradeDetailVM> Grades { get; set; } = new List<GradeDetailVM>();

        // Subject details
        public List<SubjecDetailtVM> Subjects { get; set; } = new List<SubjecDetailtVM>();

        // Project images
        public List<string> ProjectImages { get; set; } = new List<string>(); // Maps to pi.image
        public string projectImagesList { get; set; }// Maps to pi.image

        // Week details including tools
        public List<WeekDetailVM> Weeks { get; set; } = new List<WeekDetailVM>();
    }

    // Industry view model
    public class IndustryDetailVM
    {
        public int IndustryId { get; set; } // Maps to i.industry_id
        public string IndustryName { get; set; } // Maps to i.industry_name
    }

    // Role view model
    public class RoleDetailVM
    {
        public int RoleId { get; set; } // Maps to r.role_id
        public string RoleName { get; set; } // Maps to r.role_name
    }

    // Grade view model
    public class GradeDetailVM
    {
        public int GradeId { get; set; } // Maps to g.grade_id
        public string GradeName { get; set; } // Maps to g.grade_name
    }

    // Subject view model
    public class SubjecDetailtVM
    {
        public int SubjectId { get; set; } // Maps to s.subject_id
        public string SubjectName { get; set; } // Maps to s.subject_name
    }

    // Week view model including tools
    public class WeekDetailVM
    {
        public int WeekId { get; set; } // Maps to w.week_id
        public string WeekLesson { get; set; } // Maps to w.week_lesson
        public string WeekObjective { get; set; } // Maps to w.week_objective
        public string WeekActivities { get; set; } // Maps to w.week_activities
        public List<ToolDedailVM> Tools { get; set; } = new List<ToolDedailVM>(); // Maps to t.Tool_id and t.Tool_name
    }

    // Tool view model
    public class ToolDedailVM
    {
        public int ToolId { get; set; } // Maps to t.Tool_id
        public string ToolName { get; set; } // Maps to t.Tool_name
    }
}
