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
    public class ETLBatchSrcController : ControllerBase
    {
        private readonly ETLBatchSrcService _eTLBatchSrcService;
        private readonly ILogger<ETLBatchSrcController> _logger;

        public ETLBatchSrcController(ETLBatchSrcService service, ILogger<ETLBatchSrcController> logger)
        {
            _eTLBatchSrcService = service;
            _logger = logger;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/ETLBatchSrc/GetETLBatchSrcList")]
        public async Task<IActionResult> GetETLBatchSrcList()
        {
            var eTLBatchSrcList = await _eTLBatchSrcService.GetETLBatchSrcAsync();
            APIResponce obj = new APIResponce();
            if (eTLBatchSrcList == null)
            {             
                obj.StatusCode = StatusCodes.Status404NotFound;
                obj.Message = PEIConstants.NO_DATA_AVAIL;
                obj.Result = "";

                _logger.LogWarning(PEIConstants.NO_DATA_WITH_ONE_PRMTR_LOG, nameof(GetETLBatchSrcList));


                return NotFound(obj);
            }

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = "Data retrieved successfully!";
            obj.Result = eTLBatchSrcList;

            _logger.LogInformation(PEIConstants.DATA_AVAIL_TWO_PRMTR_LOG, nameof(GetETLBatchSrcList), JsonSerializer.Serialize(eTLBatchSrcList.Count()));

            return Ok(obj);

        }


        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="productDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ETLBatchSrc/CreateETLBatchSrc")]
        public async Task<IActionResult> CreateETLBatchSrc(ETLBatchSrcDTO eTLBatchSrc)
        {
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(CreateETLBatchSrc), JsonSerializer.Serialize(eTLBatchSrc));


            var isETLBatchSrcCreated = await _eTLBatchSrcService.InsertAsync(eTLBatchSrc);
            await _eTLBatchSrcService.CompletedAsync();
            APIResponce obj = new APIResponce();
            if (isETLBatchSrcCreated)
            {
                //return Ok(isETLBatchSrcCreated);

              //  return StatusCode(StatusCodes.Status200OK, "Data created successfully!");

                obj.StatusCode = StatusCodes.Status200OK;
                obj.Message = PEIConstants.DATA_CREATE_MSG;
                obj.Result = "";

                return Ok(obj);
            }
            else
            {
                // return StatusCode(StatusCodes.Status400BadRequest, "Issue while creating the record in the database table!");
                _logger.LogError(PEIConstants.ISSUE_CREATE_MSG);

                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = PEIConstants.ISSUE_CREATE_MSG;
                obj.Result = "";

                return BadRequest(obj);

            }
        }

        /// <summary>
        /// Update ETL batch src
        /// </summary>
        /// <param name="eTLBatchSrcDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/ETLBatchSrc/UpdateETLBatchSrc")]
        public async Task<IActionResult> UpdateETLBatchSrc(ETLBatchSrcDTO eTLBatchSrcDTO)
        {
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(UpdateETLBatchSrc), JsonSerializer.Serialize(eTLBatchSrcDTO));


            APIResponce obj = new APIResponce();
            if (eTLBatchSrcDTO != null)
            {
                var iseTLBatchSrcUpdated = await _eTLBatchSrcService.UpdateETLBatchSrc(eTLBatchSrcDTO);
                await _eTLBatchSrcService.CompletedAsync();
                

                if (iseTLBatchSrcUpdated)
                {
                    //return Ok(iseTLBatchSrcUpdated);
                    // return StatusCode(StatusCodes.Status200OK, "Data updated successfully!");

                    obj.StatusCode = StatusCodes.Status200OK;
                    obj.Message = PEIConstants.DATA_UPDATE_MSG;
                    obj.Result = "";

                    return Ok(obj);
                }
                //   return StatusCode(StatusCodes.Status400BadRequest, "Issue while updating the record in the database table!");

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
        [Route("api/ETLBatchSrc/DeleteETLBatchSrc")]
        public async Task<IActionResult> DeleteETLBatchSrc(int Id)
        {
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(DeleteETLBatchSrc), JsonSerializer.Serialize(Id));

            APIResponce obj = new APIResponce();
            if (Id != 0)
            {
                var iseTLBatchSrcUpdated = await _eTLBatchSrcService.DeleteETLBatchSrc(Id);
                await _eTLBatchSrcService.CompletedAsync();

                if (iseTLBatchSrcUpdated)
                {

                    //  return StatusCode(StatusCodes.Status200OK, "Data deleted successfully!");

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
                // return StatusCode(StatusCodes.Status400BadRequest, "Invalid data!");
                _logger.LogWarning(PEIConstants.INVALID_DATA_MSG);
                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = PEIConstants.INVALID_DATA_MSG;
                obj.Result = "";

                return BadRequest(obj);
            }
        }

        [HttpGet]
        [Route("api/ETLBatchSrc/GetETLBatchSrcListByBatchName")]
        public async Task<IActionResult> GetETLBatchSrcListByBatchName(string batchName)
        {
            var eTLBatchSrcList = await _eTLBatchSrcService.GetETLBatchSrcByBatchNameAsync(batchName);
            APIResponce obj = new APIResponce();
            if (eTLBatchSrcList == null)
            {
                obj.StatusCode = StatusCodes.Status404NotFound;
                obj.Message = PEIConstants.NO_DATA_AVAIL;
                obj.Result = "";

                _logger.LogWarning(PEIConstants.NO_DATA_WITH_ONE_PRMTR_LOG, nameof(GetETLBatchSrcListByBatchName));

                return NotFound(obj);
            }

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = PEIConstants.DATA_AVAIL;
            obj.Result = eTLBatchSrcList;

            _logger.LogInformation(PEIConstants.DATA_AVAIL_TWO_PRMTR_LOG, nameof(GetETLBatchSrcListByBatchName), JsonSerializer.Serialize(eTLBatchSrcList.Count()));

            return Ok(obj);

        }
    }
}
