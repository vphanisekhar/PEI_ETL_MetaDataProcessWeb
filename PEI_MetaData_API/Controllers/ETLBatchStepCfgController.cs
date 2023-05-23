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
    public class ETLBatchStepCfgController : ControllerBase
    {
        private readonly ETLBatchStepCfgService _eTLBatchStepCfgService;
        private readonly ILogger<ETLBatchStepCfgController> _logger;

        public ETLBatchStepCfgController(ETLBatchStepCfgService service, ILogger<ETLBatchStepCfgController> logger)
        {
            _eTLBatchStepCfgService = service;
            _logger = logger;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/ETLBatchStepCfg/GetETLBatchStepCfgList")]
        public async Task<IActionResult> GetETLBatchStepCfgList()
        {
            var eTLBatchStepCfgList = await _eTLBatchStepCfgService.GetETLBatchStepCfgAsync();
            APIResponce obj = new APIResponce();
            if (eTLBatchStepCfgList == null)
            {             
                obj.StatusCode = StatusCodes.Status404NotFound;
                obj.Message = "No data available!";
                obj.Result = "";

                _logger.LogWarning("Executing {Action} and returning results with count 0", nameof(GetETLBatchStepCfgList));


                return NotFound(obj);
            }

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = "Data retrieved successfully!";
            obj.Result = eTLBatchStepCfgList;

            _logger.LogInformation("Executing {Action} and returning results with count {0}", nameof(GetETLBatchStepCfgList), JsonSerializer.Serialize(eTLBatchStepCfgList.Count()));

            return Ok(obj);

        }


        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="productDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ETLBatchStepCfg/CreateETLBatchStepCfg")]
        public async Task<IActionResult> CreateETLBatchStepCfg(ETLBatchStepCfgDTO eTLBatchStepCfg)
        {
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(CreateETLBatchStepCfg), JsonSerializer.Serialize(eTLBatchStepCfg));


            var isETLBatchStepCfgCreated = await _eTLBatchStepCfgService.InsertAsync(eTLBatchStepCfg);
            await _eTLBatchStepCfgService.CompletedAsync();
            APIResponce obj = new APIResponce();
            if (isETLBatchStepCfgCreated)
            {
                //return Ok(isETLBatchStepCfgCreated);

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
        /// Update ETL batch StepCfg
        /// </summary>
        /// <param name="eTLBatchStepCfgDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/ETLBatchStepCfg/UpdateETLBatchStepCfg")]
        public async Task<IActionResult> UpdateETLBatchStepCfg(ETLBatchStepCfgDTO eTLBatchStepCfgDTO)
        {
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(UpdateETLBatchStepCfg), JsonSerializer.Serialize(eTLBatchStepCfgDTO));


            APIResponce obj = new APIResponce();
            if (eTLBatchStepCfgDTO != null)
            {
                var iseTLBatchStepCfgUpdated = await _eTLBatchStepCfgService.UpdateETLBatchStepCfg(eTLBatchStepCfgDTO);
                await _eTLBatchStepCfgService.CompletedAsync();
                

                if (iseTLBatchStepCfgUpdated)
                {
                    //return Ok(iseTLBatchStepCfgUpdated);
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
        [Route("api/ETLBatchStepCfg/DeleteETLBatchStepCfg")]
        public async Task<IActionResult> DeleteETLBatchStepCfg(int Id)
        {
            _logger.LogInformation("Executing {Action} with parameters: {Parameters}", nameof(DeleteETLBatchStepCfg), JsonSerializer.Serialize(Id));

            APIResponce obj = new APIResponce();
            if (Id != 0)
            {
                var iseTLBatchStepCfgUpdated = await _eTLBatchStepCfgService.DeleteETLBatchStepCfg(Id);
                await _eTLBatchStepCfgService.CompletedAsync();

                if (iseTLBatchStepCfgUpdated)
                {

                    //  return StatusCode(StatusCodes.Status200OK, "Data deleted successfully!");

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
                // return StatusCode(StatusCodes.Status400BadRequest, "Invalid data!");
                _logger.LogWarning("Invalid data!");
                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = "Invalid data!";
                obj.Result = "";

                return BadRequest(obj);
            }
        }

        [HttpGet]
        [Route("api/ETLBatchStepCfg/GetETLBatchStepCfgListFilter")]
        public async Task<IActionResult> GetETLBatchStepCfgListFilter(string batchName, string batchStep)
        {
            var eTLBatchStepCfgList = await _eTLBatchStepCfgService.GetETLBatchStepCfgFilterAsync(batchName, batchStep);
            APIResponce obj = new APIResponce();
            if (eTLBatchStepCfgList == null)
            {
                obj.StatusCode = StatusCodes.Status404NotFound;
                obj.Message = "No data available!";
                obj.Result = "";

                _logger.LogWarning("Executing {Action} and returning results with count 0", nameof(GetETLBatchStepCfgListFilter));

                return NotFound(obj);
            }

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = "Data retrieved successfully!";
            obj.Result = eTLBatchStepCfgList;

            _logger.LogInformation("Executing {Action} and returning results with count {0}", nameof(GetETLBatchStepCfgListFilter), JsonSerializer.Serialize(eTLBatchStepCfgList.Count()));

            return Ok(obj);

        }
    }
}
