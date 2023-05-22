using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEI_ETL.Core.Entities;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Service;
using System.Text.Json;

namespace PEI_ETL_MetaDataProcess_APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ETLBatchSrcStepController : ControllerBase
    {
        private readonly ETLBatchSrcStepService _eTLBatchSrcStepService;
        private readonly ILogger<ETLBatchSrcStepController> _logger;

        public ETLBatchSrcStepController(ETLBatchSrcStepService service, ILogger<ETLBatchSrcStepController> logger)
        {
            _eTLBatchSrcStepService = service;
            _logger = logger;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/ETLBatchSrcStep/GetETLBatchSrcStepList")]
        public async Task<IActionResult> GetETLBatchSrcStepList()
        {
            var ETLBatchSrcStepList = await _eTLBatchSrcStepService.GetETLBatchSrcStepAsync();
            APIResponce obj = new APIResponce();
            if (ETLBatchSrcStepList == null)
            {
                obj.StatusCode = StatusCodes.Status404NotFound;
                obj.Message = "No data available!";
                obj.Result = "";

                return NotFound(obj);
            }

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = "Data retrieved successfully!";
            obj.Result = ETLBatchSrcStepList;

            return Ok(obj);

        }


        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="productDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ETLBatchSrcStep/CreateETLBatchSrcStep")]
        public async Task<IActionResult> CreateETLBatchSrcStep(ETLBatchSrcStepDTO eTLBatchSrcStep)
        {
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(CreateETLBatchSrcStep), JsonSerializer.Serialize(eTLBatchSrcStep));


            var isETLBatchSrcStepCreated = await _eTLBatchSrcStepService.InsertAsync(eTLBatchSrcStep);
            await _eTLBatchSrcStepService.CompletedAsync();
            APIResponce obj = new APIResponce();
            if (isETLBatchSrcStepCreated)
            {
                //return Ok(isETLBatchStepCreated);

                //  return StatusCode(StatusCodes.Status200OK, "Data created successfully!");

                obj.StatusCode = StatusCodes.Status200OK;
                obj.Message = "Data created successfully!";
                obj.Result = "";

                return Ok(obj);
            }
            else
            {
                // return StatusCode(StatusCodes.Status400BadRequest, "Issue while creating the record in the database table!");
                _logger.LogError("Issue while creating the record in the database table!");


                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = "Issue while creating the record in the database table!";
                obj.Result = "";

                return BadRequest(obj);

            }
        }

        /// <summary>
        /// Update ETL batch Step
        /// </summary>
        /// <param name="ETLBatchSrcStepDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/ETLBatchSrcStep/UpdateETLBatchSrcStep")]
        public async Task<IActionResult> UpdateETLBatchSrcStep(ETLBatchSrcStepDTO eTLBatchSrcStepDTO)
        {
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(UpdateETLBatchSrcStep), JsonSerializer.Serialize(eTLBatchSrcStepDTO));



            APIResponce obj = new APIResponce();
            if (eTLBatchSrcStepDTO != null)
            {
                var iseTLBatchSrcStepUpdated = await _eTLBatchSrcStepService.UpdateETLBatchSrcStep(eTLBatchSrcStepDTO);
                await _eTLBatchSrcStepService.CompletedAsync();


                if (iseTLBatchSrcStepUpdated)
                {
                    //return Ok(iseTLBatchStepUpdated);
                    // return StatusCode(StatusCodes.Status200OK, "Data updated successfully!");

                    obj.StatusCode = StatusCodes.Status200OK;
                    obj.Message = "Data updated successfully!";
                    obj.Result = "";

                    return Ok(obj);
                }
                //   return StatusCode(StatusCodes.Status400BadRequest, "Issue while updating the record in the database table!");
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
        [Route("api/ETLBatchSrcStep/DeleteETLBatchSrcStep")]
        public async Task<IActionResult> DeleteETLBatchSrcStep(int Id)
        {
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(DeleteETLBatchSrcStep), JsonSerializer.Serialize(Id));

            


            APIResponce obj = new APIResponce();
            if (Id != 0)
            {
                var iseTLBatchSrcStepUpdated = await _eTLBatchSrcStepService.DeleteETLBatchSrcStep(Id);
                await _eTLBatchSrcStepService.CompletedAsync();

                if (iseTLBatchSrcStepUpdated)
                {

                    //  return StatusCode(StatusCodes.Status200OK, "Data deleted successfully!");

                    obj.StatusCode = StatusCodes.Status200OK;
                    obj.Message = "Data deleted successfully!";
                    obj.Result = "";

                    return Ok(obj);
                }

                _logger.LogError("Issue while deleting the record in the database table!");
                //  return StatusCode(StatusCodes.Status400BadRequest, "Issue while deleting the record in the database table!");
                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = "Issue while deleting the record in the database table!";
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

        [HttpGet]
        [Route("api/ETLBatchSrcStep/GetETLBatchSrcStepFilter")]
        public async Task<IActionResult> GetETLBatchSrcStepFilter(string batchName, string sourceId)
        {
            var eTLBatchSrcStepList = await _eTLBatchSrcStepService.GetETLBatchSrcStepFilterAsync(batchName, sourceId);
            APIResponce obj = new APIResponce();
            if (eTLBatchSrcStepList == null)
            {
                obj.StatusCode = StatusCodes.Status404NotFound;
                obj.Message = "No data available!";
                obj.Result = "";

                return NotFound(obj);
            }

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = "Data retrieved successfully!";
            obj.Result = eTLBatchSrcStepList;

            return Ok(obj);

        }

    }
}
