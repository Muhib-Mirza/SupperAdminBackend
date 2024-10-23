using LMS.API.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.API.DataModel.ViewModel.Project;
using LMS.API.DataManagement;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections;
using LMS.API.DataModel.DataModel.Project;
using System.Transactions;
using Ancera.API.DataManagement;
using Dapper;
using LMS.API.DataModel.ViewModel.LMS.API.ViewModel;
using LMS.API.DataModel.ViewModel;

namespace LMS.API.Business
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IConfiguration _config;
        private readonly IProjectRepository _projRepo;
        string SQL_SELECT_PROJECT()
        {
            return @"
              select Projects.project_id AS projectId,
     Projects.project_name AS projectName,
     Projects.project_description AS projectDescription,
     Projects.industry_id AS industryId,
     Projects.grade_level AS gradeLevel,
     Projects.prerequisite_learnings AS prerequisiteLearnings,
     Projects.curriculum_points AS curriculumPoints,
     Projects.droject_duration AS ProjectDuration,
     Projects.industry_pecific_problem AS IndustrySpecificProblem,
     pi.id as image_id,
     convert(pi.image using utf8)  AS projectImagesList,Roles.role_id as 'roleId',Roles.role_name as 'roleName',Subject.subject_id as 'subjectID',Subject.subject_name as 'subjectName',
     Industry.industry_id as 'industryID',Industry.industry_name as 'industryName',Grade.grade_id as 'GradeID',Grade.grade_name as 'GradeName' 
     from Projects 
     left join Project_Role_ASOC on Projects.project_id=Project_Role_ASOC.project_id
     left join Roles on Roles.role_id=Project_Role_ASOC.role_id
     left join Project_Grade_ASOC on Projects.project_id=Project_Grade_ASOC.project_id
     left join Grade on Grade.grade_id=Project_Grade_ASOC.grade_id
     left join Project_Subject_ASOC on Projects.project_id=Project_Subject_ASOC.project_id
     left join Subject on Subject.subject_id=Project_Subject_ASOC.subject_id
     left join Project_Industry_ASOC on Projects.project_id=Project_Industry_ASOC.project_id
     left join Industry on Industry.industry_id=Project_Industry_ASOC.industry_id
     LEFT JOIN 
     Project_Images pi ON Projects.project_id = pi.project_id 
     group by Projects.project_id ,
     Projects.project_name ,
     Projects.project_description ,
     Projects.industry_id ,
     Projects.grade_level ,
     Projects.prerequisite_learnings,
     Projects.curriculum_points ,
     Projects.start_date ,
     Projects.end_date ,Roles.role_id ,Roles.role_name,pi.image,pi.id ,Subject.subject_id,Subject.subject_name,
     Industry.industry_id ,Industry.industry_name,Grade.grade_id,Grade.grade_name;
            ";
        }
        string SQL_SELECT_PROJECT_BY_ID_V2()
        {
            return @"
              SELECT 
    Projects.project_id AS projectId,    
    Projects.project_name AS projectName,    
    Projects.project_description AS projectDescription,    
    Projects.industry_id AS industryId,    
    Projects.grade_level AS gradeLevel,    
    Projects.prerequisite_learnings AS prerequisiteLearnings,    
    Projects.curriculum_points AS curriculumPoints,    
    Projects.industry_pecific_problem AS IndustrySpecificProblem,    
    Projects.droject_duration AS ProjectDuration,    
    pi.id AS image_id,    
    CONVERT(pi.image USING utf8) AS projectImagesList,
    Roles.role_id AS roleId,
    Roles.role_name AS roleName,
    Subject.subject_id AS subjectID,
    Subject.subject_name AS subjectName,    
    Industry.industry_id AS industryID,
    Industry.industry_name AS industryName,
    Grade.grade_id AS GradeID,
    Grade.grade_name AS GradeName,  
    -- Week details  
    w.week_id AS Week_id, -- Maps to WeekVM.WeekId   
    w.week_lesson AS lesson_plan, -- Maps to WeekVM.WeekLesson   
    w.week_objective AS objectives, -- Maps to WeekVM.WeekObjective   
    w.week_activities AS activities, -- Maps to WeekVM.WeekActivities  
    w.step_caption AS step_caption,
    -- Tool details for each week   
    t.Tool_id AS ToolId, -- Maps to ToolVM.ToolId   
    t.Tool_name AS ToolName -- Maps to ToolVM.ToolName
FROM Projects    
LEFT JOIN Project_Role_ASOC ON Projects.project_id = Project_Role_ASOC.project_id    
LEFT JOIN Roles ON Roles.role_id = Project_Role_ASOC.role_id    
LEFT JOIN Project_Grade_ASOC ON Projects.project_id = Project_Grade_ASOC.project_id    
LEFT JOIN Grade ON Grade.grade_id = Project_Grade_ASOC.grade_id    
LEFT JOIN Project_Subject_ASOC ON Projects.project_id = Project_Subject_ASOC.project_id    
LEFT JOIN Subject ON Subject.subject_id = Project_Subject_ASOC.subject_id    
LEFT JOIN Project_Industry_ASOC ON Projects.project_id = Project_Industry_ASOC.project_id    
LEFT JOIN Industry ON Industry.industry_id = Project_Industry_ASOC.industry_id    
LEFT JOIN Project_Images pi ON Projects.project_id = pi.project_id    
-- Join week details   
LEFT JOIN project_week_asoc pwa ON pwa.project_id = Projects.project_id   
LEFT JOIN week w ON w.week_id = pwa.week_id    
-- Join tool details for each week   
LEFT JOIN project_week_tool_asoc pwta ON pwta.week_id = w.week_id   
LEFT JOIN tools t ON t.Tool_id = pwta.Tool_id   
WHERE Projects.project_id = @Id    
GROUP BY 
    Projects.project_id,    
    Projects.project_name,    
    Projects.project_description,    
    Projects.industry_id,    
    Projects.grade_level,    
    Projects.prerequisite_learnings,    
    Projects.curriculum_points,    
    Projects.start_date,    
    Projects.end_date, 
    Roles.role_id, 
    Roles.role_name,
    pi.image,
    pi.id,
    Subject.subject_id,
    Subject.subject_name,    
    Industry.industry_id,
    Industry.industry_name,
    Grade.grade_id,
    Grade.grade_name,
    w.week_id, 
    w.week_lesson, 
    w.week_objective, 
    w.week_activities, 
    t.Tool_id, 
    t.Tool_name;

    
            ";
        }

        string SQL_SELECT_PROJECT_BY_ID()
        {
            return @"
                            SELECT 
                    p.project_id AS ProjectId, -- Maps to ProjectVM.ProjectId
                    p.project_name AS ProjectName, -- Maps to ProjectVM.ProjectName
                    p.project_description AS ProjectDescription, -- Maps to ProjectVM.ProjectDescription
                    p.curriculum_points AS CurriculumPoints, -- Maps to ProjectVM.CurriculumPoints

                    -- Industry details
                    i.industry_id AS IndustryId, -- Maps to IndustryVM.IndustryId
                    i.industry_name AS IndustryName, -- Maps to IndustryVM.IndustryName

                    -- Role details
                    r.role_id AS RoleId, -- Maps to RoleVM.RoleId
                    r.role_name AS RoleName, -- Maps to RoleVM.RoleName

                    -- Grade details
                    g.grade_id AS GradeId, -- Maps to GradeVM.GradeId
                    g.grade_name AS GradeName, -- Maps to GradeVM.GradeName

                    -- Subject details
                    s.subject_id AS SubjectId, -- Maps to SubjectVM.SubjectId
                    s.subject_name AS SubjectName, -- Maps to SubjectVM.SubjectName

                    -- Project image details
                    pi.id AS ImageId, -- Maps to the image ID if needed (could be used in ProjectImages list)
                    convert(pi.image using utf8) AS ImageUrl, -- Maps to ProjectVM.ProjectImages

                    -- Week details
                    w.week_id AS WeekId, -- Maps to WeekVM.WeekId
                    w.week_lesson AS WeekLesson, -- Maps to WeekVM.WeekLesson
                    w.week_objective AS WeekObjective, -- Maps to WeekVM.WeekObjective
                    w.week_activities AS WeekActivities, -- Maps to WeekVM.WeekActivities

                    -- Tool details for each week
                    t.Tool_id AS ToolId, -- Maps to ToolVM.ToolId
                    t.Tool_name AS ToolName -- Maps to ToolVM.ToolName

                FROM 
                    projects p
                    -- Join industry details
                    LEFT JOIN project_industry_asoc pia ON pia.project_id = p.project_id
                    LEFT JOIN industry i ON i.industry_id = pia.industry_id
    
                    -- Join role details
                    LEFT JOIN project_role_asoc pra ON pra.project_id = p.project_id
                    LEFT JOIN roles r ON r.role_id = pra.role_id

                    -- Join grade details
                    LEFT JOIN project_grade_asoc pga ON pga.project_id = p.project_id
                    LEFT JOIN grade g ON g.grade_id = pga.grade_id

                    -- Join subject details
                    LEFT JOIN project_subject_asoc psa ON psa.project_id = p.project_id
                    LEFT JOIN subject s ON s.subject_id = psa.subject_id

                    -- Join project images
                    LEFT JOIN project_images pi ON pi.project_id = p.project_id

                    -- Join week details
                    LEFT JOIN project_week_asoc pwa ON pwa.project_id = p.project_id
                    LEFT JOIN week w ON w.week_id = pwa.week_id

                    -- Join tool details for each week
                    LEFT JOIN project_week_tool_asoc pwta ON pwta.week_id = w.week_id
                    LEFT JOIN tools t ON t.Tool_id = pwta.Tool_id

                WHERE 
                    p.project_id = @Id
                ORDER BY 
                    p.project_id, i.industry_id, r.role_id, g.grade_id, s.subject_id, pi.id, w.week_id, t.Tool_id;
                ";
        }

        
        string SQL_UPDATE_PROJECT()
        {
            return @"
                UPDATE Projects
                SET
                    project_name = @projectName,
                    project_description = @projectDescription,
                    industry_id = @industryId,
                    grade_level = @gradeLevel,
                    prerequisite_learnings = @prerequisiteLearnings,
                    curriculum_points = @curriculumPoints,
                    droject_duration = @ProjectDuration,
                    industry_pecific_problem = @IndustrySpecificProblem,
                    start_date = @startDate,
                    end_date = @endDate
                WHERE
                    project_id = @projectId;
                ";
        }

        string SQL_MARK_DELETED_BY_PROJECT()
        {
            return @"UPDATE Project
            SET is_project_deleted = 1
            WHERE project_id = @Id";
        }
        string SQL_UPDATE_PROJECT_IMAGE()
        {
            return @"UPDATE Project_Images
            SET is_project_deleted = 1
            WHERE project_id = @projectId";
        }
        private string SQL_INSERT_PROJECT()
        {
             
            return @"INSERT INTO Projects (project_name, project_description, industry_id, grade_level, prerequisite_learnings, curriculum_points, droject_duration, industry_pecific_problem)
             VALUES (@ProjectName, @ProjectDescription, @IndustryId, @GradeLevel, @PrerequisiteLearnings, @CurriculumPoints, @ProjectDuration, @IndustrySpecificProblem);
             SELECT LAST_INSERT_ID();";
        }

        private string SQL_INSERT_PROJECT_Images()
        {
            return @"INSERT INTO Project_Images (project_id, image) VALUES (@project_id, @image);";
        }
        private string SQL_SELECT_PROJECT_META()
        {
            return @"select subject_id as Id,subject_name as Name from subject";
        }
        private string SQL_SELECT_ROLE_META()
        {
            return @"select role_id as Id,role_name as Name from roles";
        }
        private string SQL_SELECT_INDUSTRY_META()
        {
            return @"select industry_id as Id,industry_name as Name from industry";
        }
        private string SQL_SELECT_GRADE_META()
        {
            return @"select grade_id as Id,grade_name as Name from grade";
        }
        private string SQL_SELECT_TOOL_META()
        {
            return @"select tool_id as Id,tool_name as name from tools";
        }

        private string SQL_INSERT_PROJECT_INDUSTRY()
        {
            return @"INSERT INTO Project_Industry_ASOC (project_id, industry_id) VALUES (@project_id, @industry_id);";
        }

        private string SQL_INSERT_PROJECT_GRADE()
        {
            return @"INSERT INTO Project_Grade_ASOC (project_id, grade_id) VALUES (@project_id, @grade_id);";
        }

        private string SQL_INSERT_PROJECT_ROLE()
        {
            return @"INSERT INTO Project_Role_ASOC (project_id, role_id) VALUES (@project_id, @role_id);";
        }

        private string SQL_INSERT_PROJECT_SUBJECT()
        {
            return @"INSERT INTO Project_Subject_ASOC (project_id, subject_id) VALUES (@project_id, @subject_id);";
        }

        private string SQL_INSERT_WEEK()
        {
            return @"INSERT INTO Week (week_lesson, week_objective,week_activities,step_caption) VALUES (@lessonPlan, @objectives,@activities,@stepCaption);
            SELECT LAST_INSERT_ID()";
        }

        private string SQL_INSERT_PROJECT_WEEK()
        {
            return @"INSERT INTO Project_Week_ASOC (project_id, week_id) VALUES (@project_id, @week_id);";
        }

        private string SQL_INSERT_PROJECT_WEEK_TOOLS()
        {
            return @"INSERT INTO Project_Week_Tool_ASOC (project_id, week_id, tool_id) VALUES (@project_id, @week_id, @tool_id);";
        }
        private string ADD_ROLE_QUERY()
        {
            return @"
        SET @nextId  = (SELECT IFNULL(MAX(role_id), 0) + 1 FROM Roles);
        INSERT INTO Roles (role_id, role_name, role_description, tools_skills, assessment_criteria, creation_date, modification_date, created_by, modified_by) 
        VALUES (@nextId, @name, @role_description, @tools_skills, @assessment_criteria, @creation_date, @modification_date, @created_by, @modified_by);
        SELECT MAX(role_id) FROM Roles;";
        }

        private string ADD_SUBJECT_QUERY()
        {
            return @"
        SET  @nextId  = (SELECT IFNULL(MAX(subject_id), 0) + 1 FROM Subject);
        INSERT INTO Subject (subject_id, subject_name, subject_description, creation_date, modification_date, created_by, modified_by) 
        VALUES (@nextId, @name, @subject_description, @creation_date, @modification_date, @created_by, @modified_by);
SELECT MAX(subject_id) FROM Subject;
    ";
        }

        private string ADD_INDUSTRY_QUERY()
        {
            return @"
        SET  @nextId = (SELECT IFNULL(MAX(industry_id), 0) + 1 FROM Industry);
        INSERT INTO Industry (industry_id, industry_name, industry_description, creation_date, modification_date, created_by, modified_by) 
        VALUES (@nextId, @name, @industry_description, @creation_date, @modification_date, @created_by, @modified_by);
        SELECT MAX(industry_id) FROM Industry;;
    ";
        }

        private string ADD_GRADE_QUERY()
        {
            return @"
        SET  @nextId = (SELECT IFNULL(MAX(grade_id), 0) + 1 FROM Grade);
        INSERT INTO Grade (grade_id, grade_name, grade_description, creation_date, modification_date, created_by, modified_by) 
        VALUES (@nextId, @name, @grade_description, @creation_date, @modification_date, @created_by, @modified_by);
SELECT MAX(grade_id) FROM Grade;
    ";
        }
        private string ADD_TOOL_QUERY()
        {
            return @"
        SET  @nextId = (SELECT IFNULL(MAX(Tool_id), 0) + 1 FROM Tools);
        INSERT INTO Tools (Tool_id, Tool_name, Tool_description, creation_date, modification_date, created_by, modified_by) 
        VALUES (@nextId, @name, @Tool_description, @creation_date, @modification_date, @created_by, @modified_by);
SELECT MAX(Tool_id) FROM Tools;
    ";
        }

      

      

        private string SQL_INSERT_PROJECT_IMAGES()
        {
            return @"INSERT INTO Project_Images (project_id, image) 
             VALUES (@ProjectId, @Image);";
        }

        private string SQL_DELETE_PROJECT_IMAGES()
        {
            return @"DELETE FROM Project_Images 
             WHERE project_id = @ProjectId;";
        }

      

        private string SQL_DELETE_PROJECT_INDUSTRY()
        {
            return @"DELETE FROM Project_Industry_ASOC 
             WHERE project_id = @ProjectId and industry_id = @Id and project_industry_id > 0;";
        }

      

        private string SQL_DELETE_PROJECT_GRADE()
        {
            return @"DELETE FROM Project_Grade_ASOC 
             WHERE project_id = @ProjectId and grade_id = @Id and project_grade_Id > 0;";
        }

        private string SQL_DELETE_PROJECT_ROLE()
        {
            return @"DELETE FROM Project_Role_ASOC 
             WHERE project_id = @ProjectId and role_id = @Id and project_role_Id > 0;";
        }

        private string SQL_SELECT_PROJECT_IMAGES()
        {
            return @"SELECT id, project_id, image 
             FROM Project_Images 
             WHERE project_id = @ProjectId;";
        }

        private string SQL_DELETE_PROJECT_IMAGE()
        {
            return @"DELETE FROM Project_Images 
             WHERE id = @ImageId;";
        }

        private string SQL_SELECT_PROJECT_INDUSTRY()
        {
            return @"SELECT project_industry_id, project_id, industry_id 
             FROM Project_Industry_ASOC 
             WHERE project_id = @ProjectId;";
        }
        private string SQL_SELECT_PROJECT_GRADE()
        {
            return @"SELECT project_grade_id, project_id, grade_id 
             FROM Project_Grade_ASOC 
             WHERE project_id = @ProjectId;";
        }
        private string SQL_SELECT_PROJECT_ROLE()
        {
            return @"SELECT project_role_id, project_id, role_id 
             FROM Project_Role_ASOC 
             WHERE project_id = @ProjectId;";
        }
        private string SQL_SELECT_PROJECT_SUBJECT()
        {
            return @"SELECT project_subject_id, project_id, subject_id 
             FROM Project_Subject_ASOC 
             WHERE project_id = @ProjectId;";
        }
        private string SQL_DELETE_PROJECT_SUBJECT()
        {
            return @"DELETE FROM Project_Subject_ASOC 
             WHERE project_id = @ProjectId 
             AND subject_id = @Id and project_subject_Id > 0;";
        }
        private string SQL_UPDATE_WEEK()
        {
            return @"UPDATE Week 
             SET week_lesson = @lessonPlan, 
                 week_objective = @objectives, 
                 week_activities = @activities, 
                 modification_date = NOW(), 
                 modified_by = @modifiedBy
             WHERE week_id = @weekId;";
        }

        private string SQL_SELECT_WEEK_TOOLS()
        {
            return @"SELECT project_week_tool_id, project_id, week_id, tool_id 
             FROM Project_Week_Tool_ASOC 
             WHERE project_id = @ProjectId 
             AND week_id = @WeekId;";
        }
        private string SQL_DELETE_PROJECT_WEEK_TOOLS()
        {
            return @"DELETE FROM Project_Week_Tool_ASOC 
             WHERE project_id = @ProjectId 
             AND week_id = @WeekId;";
        }
        private string SQL_SELECT_PROJECT_WEEK()
        {
            return @"SELECT * 
             FROM Project_Week_ASOC 
             WHERE project_id = @project_id 
             AND week_id = @week_id;";
        }
        private string SQL_SELECT_PROJECT_WEEKS_FOR_PROJECT()
        {
            return @"SELECT project_id,project_week_id as week_id
             FROM Project_Week_ASOC 
             WHERE project_id = @project_id;";
        }
        private string SQL_DELETE_PROJECT_WEEK()
        {
            return @"DELETE FROM Project_Week_ASOC 
             WHERE project_id = @project_id 
             AND week_id = @week_id;";
        }
        private string SQL_DELETE_PROJECT_WEEKS_AND_TOOLS_BY_PROJECT_ID()
        {
            return @"
        -- First, delete all tool associations for the weeks related to the project
        DELETE FROM Project_Week_Tool_ASOC
        WHERE week_id IN (
            SELECT week_id FROM Project_Week_ASOC WHERE project_id = @project_id
        );

        -- Then, delete the project-week associations
        DELETE FROM Project_Week_ASOC
        WHERE project_id = @project_id;

        -- Optionally, delete the weeks themselves if they are no longer needed
        DELETE FROM Week
        WHERE week_id IN (
            SELECT week_id FROM Project_Week_ASOC WHERE project_id = @project_id
        );
    ";
        }
        private string SQL_DELETE_PROJECT_IMAGES_BY_PROJECT_ID()
        {
            return @"
        -- Delete images associated with the project
        DELETE FROM Project_Images
        WHERE project_id = @project_id;
    ";
        }
        string SQL_SELECT_FILTER_DATA()
        {
            return @"select Roles.role_id as 'roleId',Roles.role_name as 'roleName',Subject.subject_id as 'subjectID',Subject.subject_name as 'subjectName',
                    Industry.industry_id as 'industryID',Industry.industry_name as 'industryName',Grade.grade_id as 'GradeID',Grade.grade_name as 'GradeName' from Projects 
                    inner join Project_Role_ASOC on Projects.project_id=Project_Role_ASOC.project_id
                    inner join Roles on Roles.role_id=Project_Role_ASOC.role_id
                    inner join Project_Grade_ASOC on Projects.project_id=Project_Grade_ASOC.project_id
                    inner join Grade on Grade.grade_id=Project_Grade_ASOC.grade_id
                    inner join Project_Subject_ASOC on Projects.project_id=Project_Subject_ASOC.project_id
                    inner join Subject on Subject.subject_id=Project_Subject_ASOC.subject_id
                    inner join Project_Industry_ASOC on Projects.project_id=Project_Industry_ASOC.project_id
                    inner join Industry on Industry.industry_id=Project_Industry_ASOC.industry_id
                    ";
        }

        public IEnumerable<ProjectVMV2> GetALLProjects()
        {
            DataManagementProperties dataManagementProperties = new DataManagementProperties();
            dataManagementProperties.config = _config;
            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                conn.Open();
                // Step 1: Set session group_concat_max_len
                var setGroupConcatLimitQuery = "SET SESSION group_concat_max_len = 1000000;";
                conn.Execute(setGroupConcatLimitQuery);
                IEnumerable<ProjectDMV2> lstProjects = QueryExecuter.APLQuerySelectAll<ProjectDMV2>(conn, SQL_SELECT_PROJECT(), null, null, dataManagementProperties);

                // Get list of distinct projects
                var projectsList = lstProjects.Select(p => p.projectId).Distinct().ToList();

                List<ProjectVMV2> projects = new List<ProjectVMV2>();
                foreach (var project in projectsList)
                {
                    var projectInfo = lstProjects.Where(x => x.projectId == project).FirstOrDefault();
                    ProjectVMV2 objProject = new ProjectVMV2();
                    objProject.projectId = projectInfo.projectId;
                    objProject.projectName = projectInfo.projectName;
                    objProject.projectDescription = projectInfo.projectDescription;
                    objProject.curriculumPoints = projectInfo.curriculumPoints;
                    objProject.IndustrySpecificProblem = projectInfo.IndustrySpecificProblem;
                    objProject.ProjectDuration = projectInfo.ProjectDuration;

                    //Get list of Project Week and fill the main object with week
                    var projectWeek = lstProjects.Where(x => x.projectId == project).GroupBy(x => new { x.Week_id, x.Week_name })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            WeekId = g.Key.Week_id,
                                            WeekName = g.Key.Week_name
                                        })
                                        .ToList();

                    objProject.projectWeek = new List<ProjectWeekAssocVMV2>();
                    foreach (var week in projectWeek)
                    {
                        ProjectWeekAssocVMV2 objWeekAssoc = new ProjectWeekAssocVMV2();
                        objWeekAssoc.week_id = week.WeekId;
                        objWeekAssoc.week_name = week.WeekName;
                        objProject.projectWeek.Add(objWeekAssoc);
                    }

                    //Get list of Project Grades and fill the main object with grades
                    var projectGrade = lstProjects.Where(x => x.projectId == project).GroupBy(x => new { x.GradeID, x.GradeName })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            GradeID = g.Key.GradeID,
                                            GradeName = g.Key.GradeName
                                        })
                                        .ToList();
                    objProject.projectGrades = new List<ProjectGradeAssocVM>();
                    foreach (var grade in projectGrade)
                    {
                        ProjectGradeAssocVM objGrade = new ProjectGradeAssocVM();
                        objGrade.grade_id = grade.GradeID;
                        objGrade.grade_name = grade.GradeName;
                        objProject.projectGrades.Add(objGrade);
                    }

                    //Get List of Project Industry and fill the main object with Industry
                    var projectIndustry = lstProjects.Where(x => x.projectId == project).GroupBy(x => new { x.industryId, x.industryName })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            IndustryID = g.Key.industryId,
                                            IndustryName = g.Key.industryName
                                        })
                                        .ToList();
                    objProject.projectIndustry = new List<ProjectIndustryAssocVM>();
                    foreach (var industry in projectIndustry)
                    {
                        ProjectIndustryAssocVM objIndustry = new ProjectIndustryAssocVM();
                        objIndustry.industry_id = industry.IndustryID;
                        objIndustry.industry_name = industry.IndustryName;
                        objProject.projectIndustry.Add(objIndustry);
                    }

                    //Get List of Project Role and fill the main object with Role
                    var projectRole = lstProjects.Where(x => x.projectId == project).GroupBy(x => new { x.roleId, x.roleName })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            RoleID = g.Key.roleId,
                                            RoleName = g.Key.roleName
                                        })
                                        .ToList();
                    objProject.projectRole = new List<ProjectRoleAssocVMV2>();
                    foreach (var role in projectRole)
                    {
                        ProjectRoleAssocVMV2 objRole = new ProjectRoleAssocVMV2();
                        objRole.role_id = role.RoleID;
                        objRole.role_name = role.RoleName;
                        objProject.projectRole.Add(objRole);
                    }

                    //Get List of Subject Role and fill the main object with subject
                    var projectSubject = lstProjects.Where(x => x.projectId == project).GroupBy(x => new { x.subjectID, x.subjectName })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            subjectID = g.Key.subjectID,
                                            subjectName = g.Key.subjectName
                                        })
                                        .ToList();
                    objProject.projectSubject = new List<ProjectSubjectAssocVMV2>();
                    foreach (var subject in projectSubject)
                    {
                        ProjectSubjectAssocVMV2 objSubject = new ProjectSubjectAssocVMV2();
                        objSubject.subject_id = subject.subjectID;
                        objSubject.subject_name = subject.subjectName;
                        objProject.projectSubject.Add(objSubject);
                    }


                    //Get List of Images and fill the main object with Images List
                    var projectImages = lstProjects.Where(x => x.projectId == project).GroupBy(x => new { x.image_id, x.projectImagesList })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            imageID = g.Key.image_id,
                                            imageName = g.Key.projectImagesList
                                        })
                                        .ToList();
                    objProject.projectImagesList = new List<ProjectImagesVM>();
                    foreach (var image in projectImages)
                    {
                        ProjectImagesVM objImage = new ProjectImagesVM();
                        objImage.id = image.imageID;
                        objImage.image = image.imageName;
                        objProject.projectImagesList.Add(objImage);
                    }

                    projects.Add(objProject);

                }

                return projects;

            }

        }

        public ProjectRepository(IConfiguration config)
        {
            
                _config = config;

        }
     
        public void SaveProject(CreateProjectVM project)
        {
            DataManagementProperties dataManagementProperties = new DataManagementProperties();
            dataManagementProperties.config = _config;

            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert Project into Projects Table
                        long projectID = QueryExecuter.APLQueryInsertSingle<long>(
                            conn,
                            SQL_INSERT_PROJECT(),
                            project,
                            transaction,
                            dataManagementProperties
                        );

                        // Insert associated Project Images
                        List<ProjectImagesVM> lstProjectImages = new List<ProjectImagesVM>();
                        foreach (var image in project.ProjectImages)
                        {
                            var projectImage = new ProjectImagesVM
                            {
                                project_id = projectID,
                                image = image
                            };
                            lstProjectImages.Add(projectImage);
                        }
                        QueryExecuter.APLQueryInsertAll<long>(
                            conn,
                            SQL_INSERT_PROJECT_Images(),
                            lstProjectImages,
                            transaction,
                            dataManagementProperties
                        );

                        // Insert Industry Associations
                        List<Project_Industry_ASOC_VM> lstIndustry = new List<Project_Industry_ASOC_VM>();
                        foreach (var industryId in project.IndustryIds)
                        {
                            lstIndustry.Add(new Project_Industry_ASOC_VM { project_id = projectID, industry_id = industryId });
                        }
                        QueryExecuter.APLQueryInsertAll<long>(conn, SQL_INSERT_PROJECT_INDUSTRY(), lstIndustry, transaction, dataManagementProperties);

                        // Insert Grade Associations
                        List<Project_Grade_ASOC_VM> lstGrades = new List<Project_Grade_ASOC_VM>();
                        foreach (var gradeId in project.GradeIds)
                        {
                            lstGrades.Add(new Project_Grade_ASOC_VM { project_id = projectID, grade_id = gradeId });
                        }
                        QueryExecuter.APLQueryInsertAll<long>(conn, SQL_INSERT_PROJECT_GRADE(), lstGrades, transaction, dataManagementProperties);

                        // Insert Role Associations
                        List<Project_Role_ASOC_VM> lstRoles = new List<Project_Role_ASOC_VM>();
                        foreach (var roleId in project.RoleIds)
                        {
                            lstRoles.Add(new Project_Role_ASOC_VM { project_id = projectID, role_id = roleId });
                        }
                        QueryExecuter.APLQueryInsertAll<long>(conn, SQL_INSERT_PROJECT_ROLE(), lstRoles, transaction, dataManagementProperties);

                        // Insert Subject Associations
                        List<Project_Subject_ASOC_VM> lstSubjects = new List<Project_Subject_ASOC_VM>();
                        foreach (var subjectId in project.SubjectIds)
                        {
                            lstSubjects.Add(new Project_Subject_ASOC_VM { project_id = projectID, subject_id = subjectId });
                        }
                        QueryExecuter.APLQueryInsertAll<long>(conn, SQL_INSERT_PROJECT_SUBJECT(), lstSubjects, transaction, dataManagementProperties);

                        // Insert Weeks and their Tools
                        foreach (var week in project.Weeks)
                        {
                            long weekID = QueryExecuter.APLQueryInsertSingle<long>(
                                conn,
                                SQL_INSERT_WEEK(),
                                week,
                                transaction,
                                dataManagementProperties
                            );

                            // Associate Week with the Project
                            var projectWeek = new Project_Week_ASOC_VM
                            {
                                project_id = projectID,
                                week_id = weekID
                            };
                            QueryExecuter.APLQueryInsertSingle<long>(conn, SQL_INSERT_PROJECT_WEEK(), projectWeek, transaction, dataManagementProperties);

                            // Insert Week Tools
                            List<Project_Week_Tool_ASOC_VM> weekTools = new List<Project_Week_Tool_ASOC_VM>();
                            foreach (var tool in week.Tools)
                            {
                                weekTools.Add(new Project_Week_Tool_ASOC_VM { project_id = projectID, week_id = weekID, tool_id = tool });
                            }
                            QueryExecuter.APLQueryInsertAll<long>(conn, SQL_INSERT_PROJECT_WEEK_TOOLS(), weekTools, transaction, dataManagementProperties);
                        }

                        // Commit Transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction if something goes wrong
                        transaction.Rollback();
                        throw new Exception("An error occurred while saving the project", ex);
                    }
                }
            }
        }

        public bool DeleteFarm(FarmDeleteVM input)
        {

            DataManagementProperties dataManagementProperties = new DataManagementProperties();
            dataManagementProperties.config = _config;
            bool result = true;
            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {


                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    int rows = QueryExecuter.APLQueryDeleteSingle(conn, SQL_MARK_DELETED_BY_PROJECT(), input, transaction, dataManagementProperties);

                    transaction.Commit();
                }

            }
            return result;
        }
        public IEnumerable<ProjectVMV1> GetProjectDetail(FarmDeleteVM Id)
        {
            DataManagementProperties dataManagementProperties = new DataManagementProperties();
            dataManagementProperties.config = _config;
            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                conn.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", Id.Id);
                // Step 1: Set session group_concat_max_len
                var setGroupConcatLimitQuery = "SET SESSION group_concat_max_len = 1000000;";
                conn.Execute(setGroupConcatLimitQuery);
                return QueryExecuter.APLQuerySelectAll<ProjectVMV1>(conn, SQL_SELECT_PROJECT_BY_ID(), parameters, null, dataManagementProperties);
            }

        }
        public MetaDataVM GetMedaData()
        {
            DataManagementProperties dataManagementProperties = new DataManagementProperties();
            dataManagementProperties.config = _config;
            MetaDataVM metaDataVM  = new MetaDataVM();
            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                conn.Open();
                DynamicParameters parameters = new DynamicParameters();
                
                // Step 1: Set session group_concat_max_len
                var setGroupConcatLimitQuery = "SET SESSION group_concat_max_len = 1000000;";
                conn.Execute(setGroupConcatLimitQuery);
                metaDataVM.subjectList = QueryExecuter.APLQuerySelectAll<SubjectVM>(conn, SQL_SELECT_PROJECT_META(), parameters, null, dataManagementProperties);
                metaDataVM.roleList = QueryExecuter.APLQuerySelectAll<RoleVM>(conn, SQL_SELECT_ROLE_META(), parameters, null, dataManagementProperties);
                metaDataVM.industryList = QueryExecuter.APLQuerySelectAll<IndustryVM>(conn, SQL_SELECT_INDUSTRY_META(), parameters, null, dataManagementProperties);
                metaDataVM.gradeList = QueryExecuter.APLQuerySelectAll<GradeVM>(conn, SQL_SELECT_GRADE_META(), parameters, null, dataManagementProperties);
                metaDataVM.toolList = QueryExecuter.APLQuerySelectAll<ToolMetaVM>(conn, SQL_SELECT_TOOL_META(), parameters, null, dataManagementProperties);
            }

            return metaDataVM;

        }
        public long AddMedaData(ConfigurtionVm input)
        {
            DataManagementProperties dataManagementProperties = new DataManagementProperties();
            dataManagementProperties.config = _config;
            long result = 0;
            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                conn.Open();
                DynamicParameters parameters = new DynamicParameters();
                
                if (input.type == "Role")
                {
                    result = QueryExecuter.APLQueryInsertSingle<long>(conn, ADD_ROLE_QUERY(), input, null, dataManagementProperties);


                }
                if (input.type == "Subject")
                {
                     result = QueryExecuter.APLQueryInsertSingle<long>(conn, ADD_SUBJECT_QUERY(), input, null, dataManagementProperties);

                }
                if (input.type == "Industry")
                {
                     result = QueryExecuter.APLQueryInsertSingle<long>(conn, ADD_INDUSTRY_QUERY(), input, null, dataManagementProperties);

                }
                if (input.type == "Grade")
                {
                     result = QueryExecuter.APLQueryInsertSingle<long>(conn, ADD_GRADE_QUERY(), input, null, dataManagementProperties);

                }
                if (input.type == "Tool")
                {
                     result = QueryExecuter.APLQueryInsertSingle<long>(conn, ADD_TOOL_QUERY(), input, null, dataManagementProperties);

                }
            }

            return result;

        }

        public void EditProject(CreateProjectVM project)
        {
            DataManagementProperties dataManagementProperties = new DataManagementProperties();
            dataManagementProperties.config = _config;

            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                conn.Open();
                
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert or Update Project
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("ProjectId", project.ProjectId);
                        long projectID = project.ProjectId ?? 0;
                        long result = QueryExecuter.APLQueryUpdateSingle<long>(conn, SQL_UPDATE_PROJECT(), project, transaction, dataManagementProperties);


                        // Insert associated Project Images
                        DynamicParameters parameters2 = new DynamicParameters();
                        parameters2.Add("project_id", project.ProjectId);
                        List<Project_Week_ASOC_VM> t = QueryExecuter
                            .APLQuerySelectAll<Project_Week_ASOC_VM>(conn, SQL_DELETE_PROJECT_IMAGES_BY_PROJECT_ID(), parameters2, transaction, dataManagementProperties)
                            .ToList();
                        List<ProjectImagesVM> lstProjectImages = new List<ProjectImagesVM>();
                        foreach (var image in project.ProjectImages)
                        {
                            var projectImage = new ProjectImagesVM
                            {
                                project_id = projectID,
                                image = image
                            };
                            lstProjectImages.Add(projectImage);
                        }
                        QueryExecuter.APLQueryInsertAll<long>(
                            conn,
                            SQL_INSERT_PROJECT_Images(),
                            lstProjectImages,
                            transaction,
                            dataManagementProperties
                        );

                        // Manage Industry Associations
                        List<Project_Industry_ASOC_VM> existingIndustries = QueryExecuter.APLQuerySelectAll<Project_Industry_ASOC_VM>(conn, SQL_SELECT_PROJECT_INDUSTRY(), parameters, transaction, dataManagementProperties).ToList();
                        if (project.IndustryIds != null && project.IndustryIds.Any())
                        {
                            List<long> industryIdsAsLong = project.IndustryIds.Select(id => (long)id).ToList();

                            // Safely call UpdateAssociations with the converted list of IndustryIds
                            UpdateAssociations(conn, existingIndustries, industryIdsAsLong, SQL_INSERT_PROJECT_INDUSTRY(), SQL_DELETE_PROJECT_INDUSTRY(), transaction, dataManagementProperties, projectID, "industry_id");
                        }
                        else
                        {
                            // Handle cases with no IndustryIds if needed
                            QueryExecuter.APLQueryDeleteAll(conn, SQL_DELETE_PROJECT_INDUSTRY(), parameters, transaction, dataManagementProperties);
                        }

                        // Manage Grade Associations
                        List<Project_Grade_ASOC_VM> existingGrades = QueryExecuter.APLQuerySelectAll<Project_Grade_ASOC_VM>(conn, SQL_SELECT_PROJECT_GRADE(), parameters, transaction, dataManagementProperties).ToList();
                        if (project.GradeIds != null && project.GradeIds.Any())
                        {
                            List<long> gradeIdsAsLong = project.GradeIds.Select(id => (long)id).ToList();

                            // Safely call UpdateAssociations with the converted list of GradeIds
                            UpdateAssociations(conn, existingGrades, gradeIdsAsLong, SQL_INSERT_PROJECT_GRADE(), SQL_DELETE_PROJECT_GRADE(), transaction, dataManagementProperties, projectID, "grade_id");
                        }
                        else
                        {
                            // Handle cases with no GradeIds if needed
                            QueryExecuter.APLQueryDeleteAll(conn, SQL_DELETE_PROJECT_GRADE(), parameters, transaction, dataManagementProperties);
                        }

                        // Manage Role Associations
                        List<Project_Role_ASOC_VM> existingRoles = QueryExecuter.APLQuerySelectAll<Project_Role_ASOC_VM>(conn, SQL_SELECT_PROJECT_ROLE(), parameters, transaction, dataManagementProperties).ToList();
                        if (project.RoleIds != null && project.RoleIds.Any())
                        {
                            List<long> roleIdsAsLong = project.RoleIds.Select(id => (long)id).ToList();

                            // Safely call UpdateAssociations with the converted list of RoleIds
                            UpdateAssociations(conn, existingRoles, roleIdsAsLong, SQL_INSERT_PROJECT_ROLE(), SQL_DELETE_PROJECT_ROLE(), transaction, dataManagementProperties, projectID, "role_id");
                        }
                        else
                        {
                            // Handle cases with no RoleIds if needed
                            QueryExecuter.APLQueryDeleteAll(conn, SQL_DELETE_PROJECT_ROLE(), parameters, transaction, dataManagementProperties);
                        }

                        // Manage Subject Associations
                        List<Project_Subject_ASOC_VM> existingSubjects = QueryExecuter.APLQuerySelectAll<Project_Subject_ASOC_VM>(conn, SQL_SELECT_PROJECT_SUBJECT(), parameters, transaction, dataManagementProperties).ToList();
                        if (project.SubjectIds != null && project.SubjectIds.Any())
                        {
                            List<long> subjectIdsAsLong = project.SubjectIds.Select(id => (long)id).ToList();

                            // Safely call UpdateAssociations with the converted list of SubjectIds
                            UpdateAssociations(conn, existingSubjects, subjectIdsAsLong, SQL_INSERT_PROJECT_SUBJECT(), SQL_DELETE_PROJECT_SUBJECT(), transaction, dataManagementProperties, projectID, "subject_id");
                        }
                        else
                        {
                            // Handle cases with no SubjectIds if needed, such as clearing associations
                            QueryExecuter.APLQueryDeleteAll(conn, SQL_DELETE_PROJECT_SUBJECT(), parameters, transaction, dataManagementProperties);
                        }


                        // Manage Weeks and their Tools
                        // Get the list of existing project-week associations for the project
                        DynamicParameters parameters1 = new DynamicParameters();
                        parameters1.Add("project_id", project.ProjectId);
                        List<Project_Week_ASOC_VM> existingProjectWeeks = QueryExecuter
                            .APLQuerySelectAll<Project_Week_ASOC_VM>(conn, SQL_DELETE_PROJECT_WEEKS_AND_TOOLS_BY_PROJECT_ID(), parameters1, transaction, dataManagementProperties)
                            .ToList();

                        // Insert Weeks and their Tools
                        foreach (var week in project.Weeks)
                        {
                            long weekID = QueryExecuter.APLQueryInsertSingle<long>(
                                conn,
                                SQL_INSERT_WEEK(),
                                week,
                                transaction,
                                dataManagementProperties
                            );

                            // Associate Week with the Project
                            var projectWeek = new Project_Week_ASOC_VM
                            {
                                project_id = projectID,
                                week_id = weekID
                            };
                            QueryExecuter.APLQueryInsertSingle<long>(conn, SQL_INSERT_PROJECT_WEEK(), projectWeek, transaction, dataManagementProperties);

                            // Insert Week Tools
                            List<Project_Week_Tool_ASOC_VM> weekTools = new List<Project_Week_Tool_ASOC_VM>();
                            foreach (var tool in week.Tools)
                            {
                                weekTools.Add(new Project_Week_Tool_ASOC_VM { project_id = projectID, week_id = weekID, tool_id = tool });
                            }
                            QueryExecuter.APLQueryInsertAll<long>(conn, SQL_INSERT_PROJECT_WEEK_TOOLS(), weekTools, transaction, dataManagementProperties);
                        }

                        // Commit Transaction

                        // After processing all weeks, delete any remaining associations that were not updated
                        foreach (var remainingWeek in existingProjectWeeks)
                        {
                            QueryExecuter.APLQueryDeleteAll(conn, SQL_DELETE_PROJECT_WEEK(), new { project_id = projectID, week_id = remainingWeek.week_id }, transaction, dataManagementProperties);
                        }




                        // Commit Transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction if something goes wrong
                        transaction.Rollback();
                        throw new Exception("An error occurred while saving the project", ex);
                    }
                }
            }
        }

        // Helper method to manage association insertions and deletions
        private void UpdateAssociations<T>(IDbConnection conn, List<T> existingItems, List<long> newItems, string insertSql, string deleteSql, IDbTransaction transaction, DataManagementProperties dataManagementProperties, long projectId, string idField)
        {
            // Find items to delete
            var itemsToDelete = existingItems.Where(e => !newItems.Contains((long)typeof(T).GetProperty(idField).GetValue(e))).ToList();
            foreach (var item in itemsToDelete)
            {
                DynamicParameters parameters = new DynamicParameters();
                var gradeIdProperty = item.GetType().GetProperty("grade_id");
                var RoleIdProperty = item.GetType().GetProperty("role_id");
                var SubjectIdProperty = item.GetType().GetProperty("subject_id");
                var IndustryIdProperty = item.GetType().GetProperty("industry_id");
                if (gradeIdProperty != null)
                {
                    var IdValue = gradeIdProperty.GetValue(item);
                    parameters.Add("Id", IdValue);
                }
                if (RoleIdProperty != null)
                {
                    var IdValue = RoleIdProperty.GetValue(item);
                    parameters.Add("Id", IdValue);
                }
                if (SubjectIdProperty != null)
                {
                    var IdValue = SubjectIdProperty.GetValue(item);
                    parameters.Add("Id", IdValue);

                }
                if (IndustryIdProperty != null)
                {
                    var IdValue = IndustryIdProperty.GetValue(item);
                    parameters.Add("Id", IdValue);

                }
                parameters.Add("ProjectId", projectId);
                QueryExecuter.APLQueryDeleteAll(conn, deleteSql, parameters, transaction, dataManagementProperties);
            }

            // Find items to add
            var itemsToAdd = newItems.Where(n => !existingItems.Any(e => (long)typeof(T).GetProperty(idField).GetValue(e) == n)).ToList();
            foreach (var newItem in itemsToAdd)
            {
                var associationItem = Activator.CreateInstance<T>();
                typeof(T).GetProperty("project_id").SetValue(associationItem, projectId);
                typeof(T).GetProperty(idField).SetValue(associationItem, newItem);
                QueryExecuter.APLQueryInsertSingle<long>(conn, insertSql, associationItem, transaction, dataManagementProperties);
            }
        }

        private void UpdateAssociationsTool<T>(IDbConnection conn, List<T> existingItems, List<int> newItems, string insertSql, string deleteSql, IDbTransaction transaction, DataManagementProperties dataManagementProperties, long projectId, long weekId, string idField)
        {
            // Convert int tools to long since newItems is List<int>
            List<long> newItemsAsLong = newItems.Select(i => (long)i).ToList();

            // Get the property information for the given idField and project_id, week_id
            var idProperty = typeof(T).GetProperty(idField);
            var projectIdProperty = typeof(T).GetProperty("project_id");
            var weekIdProperty = typeof(T).GetProperty("week_id");

            // Ensure the properties exist
            if (idProperty == null || projectIdProperty == null || weekIdProperty == null)
                throw new InvalidOperationException($"The type {typeof(T)} does not contain the necessary properties.");

            // Find items to delete (existing items that are not in newItems)
            var itemsToDelete = existingItems
                .Where(e => !newItemsAsLong.Contains(Convert.ToInt64(idProperty.GetValue(e))))
                .ToList();

            foreach (var item in itemsToDelete)
            {
                QueryExecuter.APLQueryDeleteSingle(conn, deleteSql, item, transaction, dataManagementProperties);
            }

            // Find items to add (new items that are not in existingItems)
            var itemsToAdd = newItemsAsLong
                .Where(n => !existingItems.Any(e => Convert.ToInt64(idProperty.GetValue(e)) == n))
                .ToList();

            foreach (var newItem in itemsToAdd)
            {
                // Create a new instance of T (the association class)
                var associationItem = Activator.CreateInstance<T>();

                // Set the project_id, week_id, and tool_id on the new instance
                projectIdProperty.SetValue(associationItem, projectId);
                weekIdProperty.SetValue(associationItem, weekId);
                idProperty.SetValue(associationItem, newItem);

                // Insert the new association into the database
                QueryExecuter.APLQueryInsertSingle<long>(conn, insertSql, associationItem, transaction, dataManagementProperties);
            }
        }

        public ProjectFiltersVM GetProjectFilters()
        {
            ProjectFiltersVM lstFilters = new ProjectFiltersVM();
            lstFilters.lstRoleFilter = new List<string>();
            lstFilters.lstGradeFilter = new List<string>();
            lstFilters.lstSubjectFilter = new List<string>();
            lstFilters.lstIndustryFilter = new List<string>();

            DataManagementProperties dataManagementProperties = new DataManagementProperties();
            dataManagementProperties.config = _config;
            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                conn.Open();
                // Step 1: Set session group_concat_max_len
                var setGroupConcatLimitQuery = "SET SESSION group_concat_max_len = 1000000;";
                conn.Execute(setGroupConcatLimitQuery);
                IEnumerable<ProjectsFiltersDM> lstProjectFilters = QueryExecuter.APLQuerySelectAll<ProjectsFiltersDM>(conn, SQL_SELECT_FILTER_DATA(), null, null, dataManagementProperties);

                var projecRole = lstProjectFilters.GroupBy(x => new { x.roleId, x.roleName })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            RoleID = g.Key.roleId,
                                            RoleName = g.Key.roleName
                                        })
                                        .ToList();

                foreach (var role in projecRole)
                {
                    //RoleFilterVM roleFilterVM = new RoleFilterVM();
                    //roleFilterVM.Id = role.RoleID;
                    //roleFilterVM.Name = role.RoleName;

                    lstFilters.lstRoleFilter.Add(role.RoleName);
                }

                var projecSubject = lstProjectFilters.GroupBy(x => new { x.subjectID, x.subjectName })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            SubjectID = g.Key.subjectID,
                                            SubjectName = g.Key.subjectName
                                        })
                                        .ToList();

                foreach (var subject in projecSubject)
                {
                    //SubjectFilterVM subjectFilterVM = new SubjectFilterVM();
                    //subjectFilterVM.Id = subject.SubjectID;
                    //subjectFilterVM.Name = subject.SubjectName;

                    lstFilters.lstSubjectFilter.Add(subject.SubjectName);
                }

                var projecIndustry = lstProjectFilters.GroupBy(x => new { x.industryID, x.industryName })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            IndustryID = g.Key.industryID,
                                            IndustryName = g.Key.industryName
                                        })
                                        .ToList();

                foreach (var industry in projecIndustry)
                {
                    //IndustryFilterVM industryFilterVM = new IndustryFilterVM();
                    //industryFilterVM.Id = industry.IndustryID;
                    //industryFilterVM.Name = industry.IndustryName;
                    lstFilters.lstIndustryFilter.Add(industry.IndustryName);
                }
                var projecGrade = lstProjectFilters.GroupBy(x => new { x.GradeID, x.GradeName })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            GradeID = g.Key.GradeID,
                                            GradeName = g.Key.GradeName
                                        })
                                        .ToList();

                foreach (var grade in projecGrade)
                {
                    //GradeFilterVM gradeFilterVM = new GradeFilterVM();
                    //gradeFilterVM.Id = grade.GradeID;
                    //gradeFilterVM.Name = grade.GradeName;

                    lstFilters.lstGradeFilter.Add(grade.GradeName);
                }
            }
            return lstFilters;
        }
        public IEnumerable<ProjectVMV2> GetProjectDetailV2(FarmDeleteVM Id)
        {
            DataManagementProperties dataManagementProperties = new DataManagementProperties();
            dataManagementProperties.config = _config;
            using (IDbConnection conn = DBMSConnection.GetConnection(_config))
            {
                conn.Open();
                // Step 1: Set session group_concat_max_len
                var setGroupConcatLimitQuery = "SET SESSION group_concat_max_len = 1000000;";
                conn.Execute(setGroupConcatLimitQuery);
                IEnumerable<ProjectDMV2> lstProjects = QueryExecuter.APLQuerySelectAll<ProjectDMV2>(conn, SQL_SELECT_PROJECT_BY_ID_V2(), Id, null, dataManagementProperties);

                // Get list of distinct projects
                var projectsList = lstProjects.Select(p => p.projectId).Distinct().ToList();

                List<ProjectVMV2> projects = new List<ProjectVMV2>();
                foreach (var project in projectsList)
                {
                    var projectInfo = lstProjects.Where(x => x.projectId == project).FirstOrDefault();
                    ProjectVMV2 objProject = new ProjectVMV2();
                    objProject.projectId = projectInfo.projectId;
                    objProject.projectName = projectInfo.projectName;
                    objProject.projectDescription = projectInfo.projectDescription;
                    objProject.curriculumPoints = projectInfo.curriculumPoints;
                    objProject.IndustrySpecificProblem = projectInfo.IndustrySpecificProblem;
                    objProject.ProjectDuration = projectInfo.ProjectDuration;
                    objProject.endDate = projectInfo.end_date;

                    //Get list of Project Week and fill the main object with week
                    var projectWeek = lstProjects.Where(x => x.projectId == project).GroupBy(x => new { x.Week_id, x.lesson_plan,x.objectives,x.activities,x.step_caption })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            WeekId = g.Key.Week_id,
                                            lesson_plan = g.Key.lesson_plan,
                                            objectives = g.Key.objectives,
                                            activities = g.Key.activities,
                                            step_caption = g.Key.step_caption,
                                            tools = lstProjects.Where(w => w.Week_id == g.Key.Week_id)
                                                               .GroupBy(w => new { w.toolId, w.toolName })
                                                               .Select(t => new ToolDedailVM
                                                               {
                                                                   ToolId = t.Key.toolId,
                                                                   ToolName = t.Key.toolName
                                                               }).ToList()
                                        })
                                        .ToList();

                    objProject.projectWeek = new List<ProjectWeekAssocVMV2>();
                    foreach (var week in projectWeek)
                    {
                        ProjectWeekAssocVMV2 objWeekAssoc = new ProjectWeekAssocVMV2();
                        objWeekAssoc.week_id = week.WeekId;
                        objWeekAssoc.lesson_plan = week.lesson_plan;
                        objWeekAssoc.objectives = week.objectives;
                        objWeekAssoc.activities = week.activities;
                        objWeekAssoc.step_caption = week.step_caption;
                        objWeekAssoc.projectTools = week.tools;
                        objProject.projectWeek.Add(objWeekAssoc);
                        
                    }

                    //Get list of Project Grades and fill the main object with grades
                    var projectGrade = lstProjects.Where(x => x.projectId == project).GroupBy(x => new { x.GradeID, x.GradeName })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            GradeID = g.Key.GradeID,
                                            GradeName = g.Key.GradeName
                                        })
                                        .ToList();
                    objProject.projectGrades = new List<ProjectGradeAssocVM>();
                    foreach (var grade in projectGrade)
                    {
                        ProjectGradeAssocVM objGrade = new ProjectGradeAssocVM();
                        objGrade.grade_id = grade.GradeID;
                        objGrade.grade_name = grade.GradeName;
                        objProject.projectGrades.Add(objGrade);
                    }

                    //Get List of Project Industry and fill the main object with Industry
                    var projectIndustry = lstProjects.Where(x => x.projectId == project).GroupBy(x => new { x.industryId, x.industryName })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            IndustryID = g.Key.industryId,
                                            IndustryName = g.Key.industryName
                                        })
                                        .ToList();
                    objProject.projectIndustry = new List<ProjectIndustryAssocVM>();
                    foreach (var industry in projectIndustry)
                    {
                        ProjectIndustryAssocVM objIndustry = new ProjectIndustryAssocVM();
                        objIndustry.industry_id = industry.IndustryID;
                        objIndustry.industry_name = industry.IndustryName;
                        objProject.projectIndustry.Add(objIndustry);
                    }

                    //Get List of Project Role and fill the main object with Role
                    var projectRole = lstProjects.Where(x => x.projectId == project).GroupBy(x => new { x.roleId, x.roleName })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            RoleID = g.Key.roleId,
                                            RoleName = g.Key.roleName
                                        })
                                        .ToList();
                    objProject.projectRole = new List<ProjectRoleAssocVMV2>();
                    foreach (var role in projectRole)
                    {
                        ProjectRoleAssocVMV2 objRole = new ProjectRoleAssocVMV2();
                        objRole.role_id = role.RoleID;
                        objRole.role_name = role.RoleName;
                        objProject.projectRole.Add(objRole);
                    }

                    //Get List of Subject Role and fill the main object with subject
                    var projectSubject = lstProjects.Where(x => x.projectId == project).GroupBy(x => new { x.subjectID, x.subjectName })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            subjectID = g.Key.subjectID,
                                            subjectName = g.Key.subjectName
                                        })
                                        .ToList();
                    objProject.projectSubject = new List<ProjectSubjectAssocVMV2>();
                    foreach (var subject in projectSubject)
                    {
                        ProjectSubjectAssocVMV2 objSubject = new ProjectSubjectAssocVMV2();
                        objSubject.subject_id = subject.subjectID;
                        objSubject.subject_name = subject.subjectName;
                        objProject.projectSubject.Add(objSubject);
                    }


                    //Get List of Images and fill the main object with Images List
                    var projectImages = lstProjects.Where(x => x.projectId == project).GroupBy(x => new { x.image_id, x.projectImagesList })  // Group by week_id and week_name
                                        .Select(g => new
                                        {
                                            imageID = g.Key.image_id,
                                            imageName = g.Key.projectImagesList
                                        })
                                        .ToList();
                    objProject.projectImagesList = new List<ProjectImagesVM>();
                    foreach (var image in projectImages)
                    {
                        ProjectImagesVM objImage = new ProjectImagesVM();
                        objImage.id = image.imageID;
                        objImage.image = image.imageName;
                        objProject.projectImagesList.Add(objImage);
                    }

                    projects.Add(objProject);

                }

                return projects;

            }

        }
    }
}
