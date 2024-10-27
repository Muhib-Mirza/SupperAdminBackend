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
 //   [ServiceFilter(typeof(AsyncActionFilter))]
    [Route("api/[controller]")]
    [ApiController]
 //   [Authorize]
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

        [HttpPost("GetProjectDetail")]
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
