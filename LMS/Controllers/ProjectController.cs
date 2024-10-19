using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS.API.IntEngine.Utils;
using LMS.API.DataModel.ViewModel.Project;
using LMS.API.DataModel.DataModel.Project;
using LMS.API.Extensions.Service;
using LMS.API.Extensions.Enums;
using LMS.API.Business;
using System.Collections.Generic;
using LMS.API.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using LMS.API.IntEngine.ActionFilters;
using LMS.API.DataModel.ViewModel.LMS.API.ViewModel;
using LMS.API.DataModel.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LMS.API.IntEngine.Controllers
{
    //[ServiceFilter(typeof(AsyncActionFilter))]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProjectController : CustomControllerBase
    {
        public readonly IConfiguration _config;
        private IProjectRepository _repo;
        public ProjectController(IProjectRepository repo,IConfiguration config) : base(config)
        {
            _config = config;
            _repo = repo;
        }

   
        [HttpPost("Project")]
        public async Task<IActionResult> Project()
        {
            IEnumerable<ProjectVMV2> repoResult;
            ResponseObject<IEnumerable<ProjectVMV2>> response = new ResponseObject<IEnumerable<ProjectVMV2>>();
            try
            {
                repoResult = _repo.GetALLProjects();
            }
            catch (Exception ex)
            {
                return Ok(ExceptionHandler.Proceed(ex));
            }
            response.setDataObject(MessageCodeEnum.SELECT_SUCCESS, repoResult);

            return Ok(response);
        }

        [HttpPost("saveProject")]
        public async Task<IActionResult> SaveProject([FromBody] CreateProjectVM project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     _repo.SaveProject(project);
                    return Ok(new { message = "Project saved successfully" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

            return BadRequest(ModelState);
        }
        [HttpPost("EditProject")]
        public async Task<IActionResult> EditProject(CreateProjectVM project)
        {
            bool repoResult;
            ResponseObject<bool> response = new ResponseObject<bool>();
            try
            {
                _repo.EditProject(project);
                return Ok(new { message = "Project saved successfully" });
            }
            catch (Exception ex)
            {
                return Ok(ExceptionHandler.Proceed(ex));
            }
            response.setDataObject(MessageCodeEnum.SELECT_SUCCESS, repoResult);

            return Ok(response);
        }
        [HttpPost("DeleteFarm")]
        public async Task<IActionResult> DeleteProject([FromBody] FarmDeleteVM input)
        {
            ResponseObject<bool> response = new ResponseObject<bool>();
            bool repoResult = false;
            try
            {



                repoResult = _repo.DeleteFarm(input);
            }
            catch (Exception ex)
            {

                return Ok(ExceptionHandler.Proceed(ex));
            }

            response.setDataObject(MessageCodeEnum.DELETE_SUCCESS, repoResult, PlaceHoldersEnum.Data, PlaceHoldersEnum.Farm.GetDescription());

            return Ok(response);
        }
        [HttpPost("GetProjectDetail")]
        public async Task<IActionResult> GetProjectDetail([FromBody] FarmDeleteVM Id)
        {
            IEnumerable<ProjectVMV1> repoResult;
            ProjectVMV2 projectVM = null;
            ResponseObject<List<ProjectVMV1>> response = new ResponseObject<List<ProjectVMV1>>();
            List<ProjectVMV1> result = new List<ProjectVMV1>();
            try
            {
                repoResult = _repo.GetProjectDetail(Id);
                // Dictionary to hold unique project data with weeks and tools
                Dictionary<int, ProjectVMV1> projectDictionary = new Dictionary<int, ProjectVMV1>();

                // Loop through the result set from the query (assuming repoResult contains the data)
                foreach (var record in repoResult)
                {
                    // Check if the project already exists in the dictionary
                    if (!projectDictionary.ContainsKey(record.ProjectId ?? 0))
                    {
                        // If not, create a new ProjectVM and add it to the dictionary
                        projectDictionary[record.ProjectId ?? 0] = new ProjectVMV1
                        {
                            ProjectId = record.ProjectId,
                            ProjectName = record.ProjectName,
                            ProjectDescription = record.ProjectDescription,
                            CurriculumPoints = record.CurriculumPoints,
                            ProjectImages = new List<string>(),
                            Industries = new List<IndustryDetailVM>(),
                            Roles = new List<RoleDetailVM>(),
                            Grades = new List<GradeDetailVM>(),
                            Subjects = new List<SubjecDetailtVM>(),
                            Weeks = new List<WeekDetailVM>()
                        };
                    }

                    // Get the current project from the dictionary
                    var currentProject = projectDictionary[record.ProjectId ?? 0];

                    // Add project images (ensure no duplicates)
                    if (!string.IsNullOrEmpty(record.ImageUrl) && !currentProject.ProjectImages.Contains(record.ImageUrl))
                    {
                        currentProject.ProjectImages.Add(record.ImageUrl);
                    }

                    // Add industries (ensure no duplicates)
                    if (record.IndustryId != null && !currentProject.Industries.Any(i => i.IndustryId == record.IndustryId))
                    {
                        currentProject.Industries.Add(new IndustryDetailVM
                        {
                            IndustryId = record.IndustryId ?? 0,
                            IndustryName = record.IndustryName
                        });
                    }

                    // Add roles (ensure no duplicates)
                    if (record.RoleId != null && !currentProject.Roles.Any(r => r.RoleId == record.RoleId))
                    {
                        currentProject.Roles.Add(new RoleDetailVM
                        {
                            RoleId = record.RoleId ?? 0,
                            RoleName = record.RoleName
                        });
                    }

                    // Add grades (ensure no duplicates)
                    if (record.GradeId != null && !currentProject.Grades.Any(g => g.GradeId == record.GradeId))
                    {
                        currentProject.Grades.Add(new GradeDetailVM
                        {
                            GradeId = record.GradeId ?? 0,
                            GradeName = record.GradeName
                        });
                    }

                    // Add subjects (ensure no duplicates)
                    if (record.SubjectId != null && !currentProject.Subjects.Any(s => s.SubjectId == record.SubjectId))
                    {
                        currentProject.Subjects.Add(new SubjecDetailtVM
                        {
                            SubjectId = record.SubjectId ?? 0,
                            SubjectName = record.SubjectName
                        });
                    }

                    // Check if the week already exists under this project
                    var existingWeek = currentProject.Weeks.FirstOrDefault(w => w.WeekId == record.WeekId);

                    // If the week does not exist, add it
                    if (existingWeek == null && record.WeekId != null)
                    {
                        existingWeek = new WeekDetailVM
                        {
                            WeekId = record.WeekId ?? 0,
                            WeekLesson = record.WeekLesson,
                            WeekObjective = record.WeekObjective,
                            WeekActivities = record.WeekActivities,
                            Tools = new List<ToolDedailVM>() // Initialize tools list for this week
                        };
                        currentProject.Weeks.Add(existingWeek);
                    }

                    // Add tools for the current week (ensure no duplicates)
                    if (existingWeek != null && record.ToolId != null && !existingWeek.Tools.Any(t => t.ToolId == record.ToolId))
                    {
                        existingWeek.Tools.Add(new ToolDedailVM
                        {
                            ToolId = record.ToolId ?? 0,
                            ToolName = record.ToolName
                        });
                    }
                }

                // Now projectDictionary contains unique projects with weeks and tools grouped under each project
                result = projectDictionary.Values.ToList();

            }
            catch (Exception ex)
            {
                return Ok(ExceptionHandler.Proceed(ex));
            }
            response.setDataObject(MessageCodeEnum.SELECT_SUCCESS, result);

            return Ok(response);
        }
        [HttpPost("getMetaData")]
        public async Task<IActionResult> getMetaData()
        {
            MetaDataVM metaDataVM = new MetaDataVM();
            ResponseObject<MetaDataVM> response = new ResponseObject<MetaDataVM>();
            try
            {
                metaDataVM = _repo.GetMedaData();
                
                
            }
            catch (Exception ex)
            {
                return Ok(ExceptionHandler.Proceed(ex));
            }
            response.setDataObject(MessageCodeEnum.SELECT_SUCCESS, metaDataVM);

            return Ok(response);
        }
        [HttpPost("addMetaData")]
        public async Task<IActionResult> addMetaData([FromBody] ConfigurtionVm input)
        {
            long res = new long();
            ResponseObject<long> response = new ResponseObject<long>();
            try
            {
                res = _repo.AddMedaData(input);


            }
            catch (Exception ex)
            {
                return Ok(ExceptionHandler.Proceed(ex));
            }
            response.setDataObject(MessageCodeEnum.SELECT_SUCCESS, res);

            return Ok(response);
        }
        [HttpGet("GetFilter")]
        public async Task<IActionResult> GetFilter()
        {
            ResponseObject<ProjectFiltersVM> response = new ResponseObject<ProjectFiltersVM>();
            ProjectFiltersVM repoResult;
            try
            {

                repoResult = _repo.GetProjectFilters();
            }
            catch (Exception ex)
            {

                return Ok(ExceptionHandler.Proceed(ex));
            }

            response.setDataObject(MessageCodeEnum.DELETE_SUCCESS, repoResult, PlaceHoldersEnum.Data, PlaceHoldersEnum.Farm.GetDescription());

            return Ok(response);
        }

        [HttpPost("GetProjectDetailV2")]
        public async Task<IActionResult> GetProjectDetailV2([FromBody] FarmDeleteVM Id)
        {
            IEnumerable<ProjectVMV2> repoResult;
            ResponseObject<IEnumerable<ProjectVMV2>> response = new ResponseObject<IEnumerable<ProjectVMV2>>();
            try
            {
                repoResult = _repo.GetProjectDetailV2(Id);
            }
            catch (Exception ex)
            {
                return Ok(ExceptionHandler.Proceed(ex));
            }
            response.setDataObject(MessageCodeEnum.SELECT_SUCCESS, repoResult);

            return Ok(response);
        }
    }
}
