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
    public class ETLBatchSrcStepCfgController : ControllerBase
    {
        private readonly ETLBatchSrcStepCfgService _eTLBatchSrcStepCfgService;
        private readonly ILogger<ETLBatchSrcStepCfgController> _logger;

        public ETLBatchSrcStepCfgController(ETLBatchSrcStepCfgService service, ILogger<ETLBatchSrcStepCfgController> logger)
        {
            _eTLBatchSrcStepCfgService = service;
            _logger = logger;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/ETLBatchSrcStepCfg/GetETLBatchSrcStepCfgList")]
        public async Task<IActionResult> GetETLBatchSrcStepCfgList()
        {
            var eTLBatchSrcStepCfgList = await _eTLBatchSrcStepCfgService.GetETLBatchSrcStepCfgAsync();
            APIResponce obj = new APIResponce();
            if (eTLBatchSrcStepCfgList == null)
            {             
                obj.StatusCode = StatusCodes.Status404NotFound;
                obj.Message = PEIConstants.NO_DATA_AVAIL;
                obj.Result = "";

                _logger.LogWarning(PEIConstants.NO_DATA_WITH_ONE_PRMTR_LOG, nameof(GetETLBatchSrcStepCfgList));


                return NotFound(obj);
            }

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = PEIConstants.DATA_AVAIL;
            obj.Result = eTLBatchSrcStepCfgList;

            _logger.LogInformation(PEIConstants.DATA_AVAIL_TWO_PRMTR_LOG, nameof(GetETLBatchSrcStepCfgList), JsonSerializer.Serialize(eTLBatchSrcStepCfgList.Count()));

            return Ok(obj);

        }


        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="productDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ETLBatchSrcStepCfg/CreateETLBatchSrcStepCfg")]
        public async Task<IActionResult> CreateETLBatchSrcStepCfg(ETLBatchSrcStepCfgDTO eTLBatchSrcStepCfg)
        {
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(CreateETLBatchSrcStepCfg), JsonSerializer.Serialize(eTLBatchSrcStepCfg));


            var isETLBatchSrcStepCfgCreated = await _eTLBatchSrcStepCfgService.InsertAsync(eTLBatchSrcStepCfg);
            await _eTLBatchSrcStepCfgService.CompletedAsync();
            APIResponce obj = new APIResponce();
            if (isETLBatchSrcStepCfgCreated)
            {
                //return Ok(isETLBatchSrcStepCfgCreated);

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
        /// Update ETL batch SrcStepCfg
        /// </summary>
        /// <param name="eTLBatchSrcStepCfgDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/ETLBatchSrcStepCfg/UpdateETLBatchSrcStepCfg")]
        public async Task<IActionResult> UpdateETLBatchSrcStepCfg(ETLBatchSrcStepCfgDTO eTLBatchSrcStepCfgDTO)
        {
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(UpdateETLBatchSrcStepCfg), JsonSerializer.Serialize(eTLBatchSrcStepCfgDTO));


            APIResponce obj = new APIResponce();
            if (eTLBatchSrcStepCfgDTO != null)
            {
                var iseTLBatchSrcStepCfgUpdated = await _eTLBatchSrcStepCfgService.UpdateETLBatchSrcStepCfg(eTLBatchSrcStepCfgDTO);
                await _eTLBatchSrcStepCfgService.CompletedAsync();
                

                if (iseTLBatchSrcStepCfgUpdated)
                {
                    //return Ok(iseTLBatchSrcStepCfgUpdated);
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
        [Route("api/ETLBatchSrcStepCfg/DeleteETLBatchSrcStepCfg")]
        public async Task<IActionResult> DeleteETLBatchSrcStepCfg(int Id)
        {
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(DeleteETLBatchSrcStepCfg), JsonSerializer.Serialize(Id));

            APIResponce obj = new APIResponce();
            if (Id != 0)
            {
                var iseTLBatchSrcStepCfgUpdated = await _eTLBatchSrcStepCfgService.DeleteETLBatchSrcStepCfg(Id);
                await _eTLBatchSrcStepCfgService.CompletedAsync();

                if (iseTLBatchSrcStepCfgUpdated)
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
        [Route("api/ETLBatchSrcStepCfg/GetETLBatchSrcStepCfgListFilter")]
        public async Task<IActionResult> GetETLBatchSrcStepCfgListFilter(string batchName, string batchStep)
        {
            var eTLBatchSrcStepCfgList = await _eTLBatchSrcStepCfgService.GetETLBatchSrcStepCfgFilterAsync(batchName, batchStep);
            APIResponce obj = new APIResponce();
            if (eTLBatchSrcStepCfgList == null)
            {
                obj.StatusCode = StatusCodes.Status404NotFound;
                obj.Message = PEIConstants.NO_DATA_AVAIL;
                obj.Result = "";

                _logger.LogWarning(PEIConstants.NO_DATA_WITH_ONE_PRMTR_LOG, nameof(GetETLBatchSrcStepCfgListFilter));

                return NotFound(obj);
            }

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = PEIConstants.DATA_AVAIL;
            obj.Result = eTLBatchSrcStepCfgList;

            _logger.LogInformation(PEIConstants.DATA_AVAIL_TWO_PRMTR_LOG, nameof(GetETLBatchSrcStepCfgListFilter), JsonSerializer.Serialize(eTLBatchSrcStepCfgList.Count()));

            return Ok(obj);

        }
    }
}
