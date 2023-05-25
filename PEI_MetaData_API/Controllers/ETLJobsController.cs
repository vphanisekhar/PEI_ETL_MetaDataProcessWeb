using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEI_ETL.Core.Entities;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Service;
using PEI_MetaData_API.Common;
using System.Text.Json;

namespace PEI_ETL_MetaDataProcess_APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ETLJobsController : ControllerBase
    {
        private readonly ETLJobsService _eTLJobsService;
        private readonly ILogger<ETLJobsController> _logger;


        public ETLJobsController(ETLJobsService service, ILogger<ETLJobsController> logger)
        {
            _eTLJobsService = service;
            _logger = logger;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/ETLJobs/GetETLJobsList")]
        public async Task<IActionResult> GetETLJobsList()
        {
            var eTLJobsList = await _eTLJobsService.GetETLJobsAsync();
            APIResponce obj = new APIResponce();
            if (eTLJobsList == null)
            {
                // return NotFound();
                // return StatusCode(StatusCodes.Status404NotFound, "No data available!");
                obj.StatusCode = StatusCodes.Status404NotFound;
                obj.Message = PEIConstants.NO_DATA_AVAIL;
                obj.Result = "";

                _logger.LogWarning(PEIConstants.NO_DATA_WITH_ONE_PRMTR_LOG, nameof(GetETLJobsList));

                return NotFound(obj);
            }
            //return Ok(eTLJobsSrcList);

            // return StatusCode(StatusCodes.Status200OK, eTLJobsList);

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = PEIConstants.DATA_AVAIL;
            obj.Result = eTLJobsList;

            _logger.LogInformation(PEIConstants.DATA_AVAIL_TWO_PRMTR_LOG, nameof(GetETLJobsList), JsonSerializer.Serialize(eTLJobsList.Count()));

            return Ok(obj);
        }


        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="productDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ETLJobs/CreateETLJobs")]
        public async Task<IActionResult> CreateETLJobs(ETLJobsDTO eTLJobs)
        {
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(CreateETLJobs), JsonSerializer.Serialize(eTLJobs));

            var isETLJobsCreated = await _eTLJobsService.InsertAsync(eTLJobs);
            await _eTLJobsService.CompletedAsync();
            APIResponce obj = new APIResponce();

            if (isETLJobsCreated)
            {
                //return Ok(isETLJobsSrcCreated);

               // return StatusCode(StatusCodes.Status200OK, "Data created successfully!");

                obj.StatusCode = StatusCodes.Status200OK;
                obj.Message = PEIConstants.DATA_CREATE_MSG;
                obj.Result = "";

                return Ok(obj);
            }
            else
            {
                _logger.LogError(PEIConstants.ISSUE_CREATE_MSG);

                // return StatusCode(StatusCodes.Status400BadRequest, "Issue while creating the record in the database table!");
                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = PEIConstants.ISSUE_CREATE_MSG;
                obj.Result = "";

                return BadRequest(obj);

            }
        }

        /// <summary>
        /// Update ETL Jobs 
        /// </summary>
        /// <param name="eTLJobsDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/ETLJobs/UpdateETLJobs")]
        public async Task<IActionResult> UpdateETLJobs(ETLJobsDTO eTLJobsDTO)
        {
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(UpdateETLJobs), JsonSerializer.Serialize(eTLJobsDTO));

            APIResponce obj = new APIResponce();
            if (eTLJobsDTO != null)
            {
                var iseTLJobsUpdated = await _eTLJobsService.UpdateETLJobs(eTLJobsDTO);
                await _eTLJobsService.CompletedAsync();

                if (iseTLJobsUpdated)
                {
                    //return Ok(iseTLJobsSrcUpdated);
                    // return StatusCode(StatusCodes.Status200OK, "Data updated successfully!");

                    obj.StatusCode = StatusCodes.Status200OK;
                    obj.Message = PEIConstants.DATA_UPDATE_MSG;
                    obj.Result = "";

                    return Ok(obj);
                }

                _logger.LogError(PEIConstants.ISSUE_UPDATE_MSG);

                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = PEIConstants.ISSUE_UPDATE_MSG;
                obj.Result = "";

                return BadRequest(obj);
            }
            else
            {
                // return StatusCode(StatusCodes.Status400BadRequest, "Invalid data!");

                _logger.LogWarning(PEIConstants.INVALID_DATA_MSG);

                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = PEIConstants.INVALID_DATA_MSG;
                obj.Result = "";

                return BadRequest(obj);
            }
        }


        [HttpPut]
        [Route("api/ETLJobs/DeleteETLJobs")]
        public async Task<IActionResult> DeleteETLJobs(int Id)
        {
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(DeleteETLJobs), JsonSerializer.Serialize(Id));


            APIResponce obj = new APIResponce();
            if (Id != 0)
            {
                var iseTLJobsUpdated = await _eTLJobsService.DeleteETLJobs(Id);
                await _eTLJobsService.CompletedAsync();

                if (iseTLJobsUpdated)
                {

                    //return StatusCode(StatusCodes.Status200OK, "Data deleted successfully!");

                    obj.StatusCode = StatusCodes.Status200OK;
                    obj.Message = PEIConstants.DATA_DELETE_MSG;
                    obj.Result = "";

                    return Ok(obj);
                }
                //  return StatusCode(StatusCodes.Status400BadRequest, "Issue while deleting the record in the database table!");
                _logger.LogError(PEIConstants.ISSUE_DELETE_MSG);

                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = PEIConstants.ISSUE_DELETE_MSG;
                obj.Result = "";

                return BadRequest(obj);
            }
            else
            {
                //    return StatusCode(StatusCodes.Status400BadRequest, "Invalid data!");
                _logger.LogWarning(PEIConstants.INVALID_DATA_MSG);

                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = PEIConstants.INVALID_DATA_MSG;
                obj.Result = "";

                return BadRequest(obj);
            }
        }       
    }
}
