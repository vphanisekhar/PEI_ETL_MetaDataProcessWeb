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
                obj.Message = PEIConstants.NO_DATA_AVAIL;
                obj.Result = "";

                _logger.LogWarning(PEIConstants.NO_DATA_WITH_ONE_PRMTR_LOG, nameof(GetETLBatchStepCfgList));


                return NotFound(obj);
            }

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = PEIConstants.DATA_AVAIL;
            obj.Result = eTLBatchStepCfgList;

            _logger.LogInformation(PEIConstants.DATA_AVAIL_TWO_PRMTR_LOG, nameof(GetETLBatchStepCfgList), JsonSerializer.Serialize(eTLBatchStepCfgList.Count()));

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
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(CreateETLBatchStepCfg), JsonSerializer.Serialize(eTLBatchStepCfg));


            var isETLBatchStepCfgCreated = await _eTLBatchStepCfgService.InsertAsync(eTLBatchStepCfg);
            await _eTLBatchStepCfgService.CompletedAsync();
            APIResponce obj = new APIResponce();
            if (isETLBatchStepCfgCreated)
            {
                //return Ok(isETLBatchStepCfgCreated);

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
        /// Update ETL batch StepCfg
        /// </summary>
        /// <param name="eTLBatchStepCfgDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/ETLBatchStepCfg/UpdateETLBatchStepCfg")]
        public async Task<IActionResult> UpdateETLBatchStepCfg(ETLBatchStepCfgDTO eTLBatchStepCfgDTO)
        {
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(UpdateETLBatchStepCfg), JsonSerializer.Serialize(eTLBatchStepCfgDTO));


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
        [Route("api/ETLBatchStepCfg/DeleteETLBatchStepCfg")]
        public async Task<IActionResult> DeleteETLBatchStepCfg(int Id)
        {
            _logger.LogInformation(PEIConstants.INPUT_MUTLI_PRMTR_LOG, nameof(DeleteETLBatchStepCfg), JsonSerializer.Serialize(Id));

            APIResponce obj = new APIResponce();
            if (Id != 0)
            {
                var iseTLBatchStepCfgUpdated = await _eTLBatchStepCfgService.DeleteETLBatchStepCfg(Id);
                await _eTLBatchStepCfgService.CompletedAsync();

                if (iseTLBatchStepCfgUpdated)
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
        [Route("api/ETLBatchStepCfg/GetETLBatchStepCfgListFilter")]
        public async Task<IActionResult> GetETLBatchStepCfgListFilter(string batchName, string batchStep)
        {
            var eTLBatchStepCfgList = await _eTLBatchStepCfgService.GetETLBatchStepCfgFilterAsync(batchName, batchStep);
            APIResponce obj = new APIResponce();
            if (eTLBatchStepCfgList == null)
            {
                obj.StatusCode = StatusCodes.Status404NotFound;
                obj.Message = PEIConstants.NO_DATA_AVAIL;
                obj.Result = "";

                _logger.LogWarning(PEIConstants.NO_DATA_WITH_ONE_PRMTR_LOG, nameof(GetETLBatchStepCfgListFilter));

                return NotFound(obj);
            }

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = PEIConstants.DATA_AVAIL;
            obj.Result = eTLBatchStepCfgList;

            _logger.LogInformation(PEIConstants.DATA_AVAIL_TWO_PRMTR_LOG, nameof(GetETLBatchStepCfgListFilter), JsonSerializer.Serialize(eTLBatchStepCfgList.Count()));

            return Ok(obj);

        }
    }
}
