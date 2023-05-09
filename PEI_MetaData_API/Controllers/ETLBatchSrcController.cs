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
    public class ETLBatchSrcController : ControllerBase
    {
        private readonly ETLBatchSrcService _eTLBatchSrcService;

        public ETLBatchSrcController(ETLBatchSrcService service)
        {
            _eTLBatchSrcService = service;
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
            if (eTLBatchSrcList == null)
            {
               // return NotFound();
                return StatusCode(StatusCodes.Status404NotFound, "No data available!");
            }
            //return Ok(eTLBatchSrcList);

            return StatusCode(StatusCodes.Status200OK, eTLBatchSrcList);
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
            var isETLBatchSrcCreated = await _eTLBatchSrcService.InsertAsync(eTLBatchSrc);
            await _eTLBatchSrcService.CompletedAsync();

            if (isETLBatchSrcCreated)
            {
                //return Ok(isETLBatchSrcCreated);

                return StatusCode(StatusCodes.Status200OK, "Data created successfully!");
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Issue while creating the record in the database table!");
            }
        }

        /// <summary>
        /// Update ETL batch src
        /// </summary>
        /// <param name="eTLBatchSrc"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/ETLBatchSrc/UpdateETLBatchSrc")]
        public async Task<IActionResult> UpdateETLBatchSrc(ETLBatchSrc eTLBatchSrc)
        {
            if (eTLBatchSrc != null)
            {
                var iseTLBatchSrcUpdated = await _eTLBatchSrcService.UpdateETLBatchSrc(eTLBatchSrc);
                await _eTLBatchSrcService.CompletedAsync();

                if (iseTLBatchSrcUpdated)
                {
                    //return Ok(iseTLBatchSrcUpdated);
                    return StatusCode(StatusCodes.Status200OK, "Data updated successfully!");
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Issue while updating the record in the database table!");

            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid data!");

            }
        }


        [HttpPut]
        [Route("api/ETLBatchSrc/DeleteETLBatchSrc")]
        public async Task<IActionResult> DeleteETLBatchSrc(int Id)
        {
            if (Id != 0)
            {
                var iseTLBatchSrcUpdated = await _eTLBatchSrcService.DeleteETLBatchSrc(Id);
                await _eTLBatchSrcService.CompletedAsync();

                if (iseTLBatchSrcUpdated)
                {

                    return StatusCode(StatusCodes.Status200OK, "Data deleted successfully!");
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Issue while deleting the record in the database table!");

            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid data!");
            }
        }       
    }
}
