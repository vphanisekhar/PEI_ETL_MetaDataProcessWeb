using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEI_ETL.Core.Entities;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Interfaces;
using PEI_ETL.Services.Service;
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
                obj.Message = "No data available!";
                obj.Result = "";

                _logger.LogWarning("Executing {Action} and returning results with count 0", nameof(GetETLJobsList));

                return NotFound(obj);
            }
            //return Ok(eTLJobsSrcList);

            // return StatusCode(StatusCodes.Status200OK, eTLJobsList);

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = "Data retrieved successfully!";
            obj.Result = eTLJobsList;

            _logger.LogInformation("Executing {Action} and returning results with count {0}", nameof(GetETLJobsList), JsonSerializer.Serialize(eTLJobsList.Count()));

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
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(CreateETLJobs), JsonSerializer.Serialize(eTLJobs));

            var isETLJobsCreated = await _eTLJobsService.InsertAsync(eTLJobs);
            await _eTLJobsService.CompletedAsync();
            APIResponce obj = new APIResponce();

            if (isETLJobsCreated)
            {
                //return Ok(isETLJobsSrcCreated);

               // return StatusCode(StatusCodes.Status200OK, "Data created successfully!");

                obj.StatusCode = StatusCodes.Status200OK;
                obj.Message = "Data created successfully!";
                obj.Result = "";

                return Ok(obj);
            }
            else
            {
                _logger.LogError("Issue while creating the record in the database table!");

                // return StatusCode(StatusCodes.Status400BadRequest, "Issue while creating the record in the database table!");
                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = "Issue while creating the record in the database table!";
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
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(UpdateETLJobs), JsonSerializer.Serialize(eTLJobsDTO));

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
                    obj.Message = "Data updated successfully!";
                    obj.Result = "";

                    return Ok(obj);
                }

                _logger.LogError("Issue while updating the record in the database table!");

                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = "Issue while updating the record in the database table!";
                obj.Result = "";

                return BadRequest(obj);
            }
            else
            {
                // return StatusCode(StatusCodes.Status400BadRequest, "Invalid data!");

                _logger.LogWarning("Invalid data!");

                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = "Invalid data!";
                obj.Result = "";

                return BadRequest(obj);
            }
        }


        [HttpPut]
        [Route("api/ETLJobs/DeleteETLJobs")]
        public async Task<IActionResult> DeleteETLJobs(int Id)
        {
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(DeleteETLJobs), JsonSerializer.Serialize(Id));


            APIResponce obj = new APIResponce();
            if (Id != 0)
            {
                var iseTLJobsUpdated = await _eTLJobsService.DeleteETLJobs(Id);
                await _eTLJobsService.CompletedAsync();

                if (iseTLJobsUpdated)
                {

                    //return StatusCode(StatusCodes.Status200OK, "Data deleted successfully!");

                    obj.StatusCode = StatusCodes.Status200OK;
                    obj.Message = "Data deleted successfully!";
                    obj.Result = "";

                    return Ok(obj);
                }
                //  return StatusCode(StatusCodes.Status400BadRequest, "Issue while deleting the record in the database table!");
                _logger.LogError("Issue while deleting the record in the database table!");

                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = "Issue while deleting the record in the database table!";
                obj.Result = "";

                return BadRequest(obj);
            }
            else
            {
                //    return StatusCode(StatusCodes.Status400BadRequest, "Invalid data!");
                _logger.LogWarning("Invalid data!");

                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = "Invalid data!";
                obj.Result = "";

                return BadRequest(obj);
            }
        }       
    }
}
