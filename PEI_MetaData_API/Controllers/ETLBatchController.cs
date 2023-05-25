using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEI_ETL.Core.Entities;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Service;
using System.Text.Json;
using PEI_MetaData_API.Common;

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
                obj.Message = PEIConstants.NO_DATA_AVAIL;
                obj.Result = "";

                _logger.LogWarning(PEIConstants.NO_DATA_WITH_ONE_PRMTR_LOG, nameof(GetETLBatchList));

                return NotFound(obj);
            }
            //return Ok(eTLBatchSrcList);

            // return StatusCode(StatusCodes.Status200OK, eTLBatchList);

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = PEIConstants.DATA_AVAIL;
            obj.Result = eTLBatchList;

            _logger.LogInformation(PEIConstants.DATA_AVAIL_TWO_PRMTR_LOG, nameof(GetETLBatchList), JsonSerializer.Serialize(eTLBatchList.Count()));

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
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(CreateETLBatch), JsonSerializer.Serialize(eTLBatch));

            var isETLBatchCreated = await _eTLBatchService.InsertAsync(eTLBatch);
            await _eTLBatchService.CompletedAsync();
            APIResponce obj = new APIResponce();

            if (isETLBatchCreated)
            {
                //return Ok(isETLBatchSrcCreated);

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
        /// Update ETL batch 
        /// </summary>
        /// <param name="eTLBatchDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/ETLBatch/UpdateETLBatch")]
        public async Task<IActionResult> UpdateETLBatch(ETLBatchDTO eTLBatchDTO)
        {
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(UpdateETLBatch), JsonSerializer.Serialize(eTLBatchDTO));

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
        [Route("api/ETLBatch/DeleteETLBatch")]
        public async Task<IActionResult> DeleteETLBatch(int Id)
        {
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(DeleteETLBatch), JsonSerializer.Serialize(Id));


            APIResponce obj = new APIResponce();
            if (Id != 0)
            {
                var iseTLBatchUpdated = await _eTLBatchService.DeleteETLBatch(Id);
                await _eTLBatchService.CompletedAsync();

                if (iseTLBatchUpdated)
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

        [HttpGet]
        [Route("api/ETLBatch/GetETLBatchByBatchName")]
        public async Task<IActionResult> GetETLBatchByBatchName(string batchName)
        {
            var eTLBatchSrcList = await _eTLBatchService.GetETLBatchByBatchNameAsync(batchName);
            APIResponce obj = new APIResponce();
            if (eTLBatchSrcList == null)
            {
                obj.StatusCode = StatusCodes.Status404NotFound;
                obj.Message = PEIConstants.NO_DATA_AVAIL;
                obj.Result = "";

                _logger.LogWarning(PEIConstants.NO_DATA_WITH_ONE_PRMTR_LOG, nameof(GetETLBatchByBatchName));

                return NotFound(obj);
            }

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = PEIConstants.DATA_AVAIL;
            obj.Result = eTLBatchSrcList;

            _logger.LogInformation(PEIConstants.DATA_AVAIL_TWO_PRMTR_LOG, nameof(GetETLBatchByBatchName), JsonSerializer.Serialize(eTLBatchSrcList.Count()));

            return Ok(obj);

        }
    }
}
