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
    public class ETLBatchController : ControllerBase
    {
        private readonly ETLBatchService _eTLBatchService;
        private readonly ILogger<ETLBatchController> _logger;


        public ETLBatchController(ETLBatchService service, ILogger<ETLBatchController> logger)
        {
            _eTLBatchService = service;
            _logger = logger;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/ETLBatch/GetETLBatchList")]
        public async Task<IActionResult> GetETLBatchList()
        {
            var eTLBatchList = await _eTLBatchService.GetETLBatchAsync();
            APIResponce obj = new APIResponce();
            if (eTLBatchList == null)
            {
                // return NotFound();
                // return StatusCode(StatusCodes.Status404NotFound, "No data available!");
                obj.StatusCode = StatusCodes.Status404NotFound;
                obj.Message = "No data available!";
                obj.Result = "";

                _logger.LogWarning("Executing {Action} and returning results with count 0", nameof(GetETLBatchList));

                return NotFound(obj);
            }
            //return Ok(eTLBatchSrcList);

            // return StatusCode(StatusCodes.Status200OK, eTLBatchList);

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = "Data retrieved successfully!";
            obj.Result = eTLBatchList;

            _logger.LogInformation("Executing {Action} and returning results with count {0}", nameof(GetETLBatchList), JsonSerializer.Serialize(eTLBatchList.Count()));

            return Ok(obj);
        }


        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="productDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ETLBatch/CreateETLBatch")]
        public async Task<IActionResult> CreateETLBatch(ETLBatchDTO eTLBatch)
        {
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(CreateETLBatch), JsonSerializer.Serialize(eTLBatch));

            var isETLBatchCreated = await _eTLBatchService.InsertAsync(eTLBatch);
            await _eTLBatchService.CompletedAsync();
            APIResponce obj = new APIResponce();

            if (isETLBatchCreated)
            {
                //return Ok(isETLBatchSrcCreated);

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
        /// Update ETL batch 
        /// </summary>
        /// <param name="eTLBatchDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/ETLBatch/UpdateETLBatch")]
        public async Task<IActionResult> UpdateETLBatch(ETLBatchDTO eTLBatchDTO)
        {
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(UpdateETLBatch), JsonSerializer.Serialize(eTLBatchDTO));

            APIResponce obj = new APIResponce();
            if (eTLBatchDTO != null)
            {
                var iseTLBatchUpdated = await _eTLBatchService.UpdateETLBatch(eTLBatchDTO);
                await _eTLBatchService.CompletedAsync();

                if (iseTLBatchUpdated)
                {
                    //return Ok(iseTLBatchSrcUpdated);
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
        [Route("api/ETLBatch/DeleteETLBatch")]
        public async Task<IActionResult> DeleteETLBatch(int Id)
        {
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(DeleteETLBatch), JsonSerializer.Serialize(Id));


            APIResponce obj = new APIResponce();
            if (Id != 0)
            {
                var iseTLBatchUpdated = await _eTLBatchService.DeleteETLBatch(Id);
                await _eTLBatchService.CompletedAsync();

                if (iseTLBatchUpdated)
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
