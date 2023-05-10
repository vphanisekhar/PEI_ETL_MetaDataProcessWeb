using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEI_ETL.Core.Entities;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Interfaces;
using PEI_ETL.Services.Service;

namespace PEI_ETL_MetaDataProcess_APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ETLBatchController : ControllerBase
    {
        private readonly ETLBatchService _eTLBatchService;

        public ETLBatchController(ETLBatchService service)
        {
            _eTLBatchService = service;
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

                return NotFound(obj);
            }
            //return Ok(eTLBatchSrcList);

            // return StatusCode(StatusCodes.Status200OK, eTLBatchList);

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = "Data retrieved successfully!";
            obj.Result = eTLBatchList;

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
                //  return StatusCode(StatusCodes.Status400BadRequest, "Issue while updating the record in the database table!");
                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = "Issue while updating the record in the database table!";
                obj.Result = "";

                return BadRequest(obj);
            }
            else
            {
                // return StatusCode(StatusCodes.Status400BadRequest, "Invalid data!");

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
                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = "Issue while deleting the record in the database table!";
                obj.Result = "";

                return BadRequest(obj);
            }
            else
            {
                //    return StatusCode(StatusCodes.Status400BadRequest, "Invalid data!");

                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = "Invalid data!";
                obj.Result = "";

                return BadRequest(obj);
            }
        }       
    }
}
