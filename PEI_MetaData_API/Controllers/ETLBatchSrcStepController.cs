﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    public class ETLBatchSrcStepController : ControllerBase
    {
        private readonly ETLBatchSrcStepService _eTLBatchStepService;

        public ETLBatchSrcStepController(ETLBatchSrcStepService service)
        {
            _eTLBatchStepService = service;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/ETLBatchStep/GetETLBatchSrcStepList")]
        public async Task<IActionResult> GetETLBatchSrcStepList()
        {
            var eTLBatchStepList = await _eTLBatchStepService.GetETLBatchSrcStepAsync();
            APIResponce obj = new APIResponce();
            if (eTLBatchStepList == null)
            {             
                obj.StatusCode = StatusCodes.Status404NotFound;
                obj.Message = "No data available!";
                obj.Result = "";

                return NotFound(obj);
            }

            obj.StatusCode = StatusCodes.Status200OK;
            obj.Message = "Data retrieved successfully!";
            obj.Result = eTLBatchStepList;

            return Ok(obj);

        }


        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="productDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ETLBatchStep/CreateETLBatchSrcStep")]
        public async Task<IActionResult> CreateETLBatchSrcStep(ETLBatchSrcStepDTO eTLBatchStep)
        {
            var isETLBatchStepCreated = await _eTLBatchStepService.InsertAsync(eTLBatchStep);
            await _eTLBatchStepService.CompletedAsync();
            APIResponce obj = new APIResponce();
            if (isETLBatchStepCreated)
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

                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = "Issue while creating the record in the database table!";
                obj.Result = "";

                return BadRequest(obj);

            }
        }

        /// <summary>
        /// Update ETL batch Step
        /// </summary>
        /// <param name="eTLBatchStepDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/ETLBatchStep/UpdateETLBatchSrcStep")]
        public async Task<IActionResult> UpdateETLBatchSrcStep(ETLBatchSrcStepDTO eTLBatchStepDTO)
        {
            APIResponce obj = new APIResponce();
            if (eTLBatchStepDTO != null)
            {
                var iseTLBatchStepUpdated = await _eTLBatchStepService.UpdateETLBatchSrcStep(eTLBatchStepDTO);
                await _eTLBatchStepService.CompletedAsync();
                

                if (iseTLBatchStepUpdated)
                {
                    //return Ok(iseTLBatchStepUpdated);
                    // return StatusCode(StatusCodes.Status200OK, "Data updated successfully!");

                    obj.StatusCode = StatusCodes.Status200OK;
                    obj.Message = "Data updated successfully!";
                    obj.Result = "";

                    return Ok(obj);
                }
                //   return StatusCode(StatusCodes.Status400BadRequest, "Issue while updating the record in the database table!");

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
        [Route("api/ETLBatchStep/DeleteETLBatchSrcStep")]
        public async Task<IActionResult> DeleteETLBatchSrcStep(int Id)
        {
            APIResponce obj = new APIResponce();
            if (Id != 0)
            {
                var iseTLBatchStepUpdated = await _eTLBatchStepService.DeleteETLBatchSrcStep(Id);
                await _eTLBatchStepService.CompletedAsync();

                if (iseTLBatchStepUpdated)
                {

                    //  return StatusCode(StatusCodes.Status200OK, "Data deleted successfully!");

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
                // return StatusCode(StatusCodes.Status400BadRequest, "Invalid data!");

                obj.StatusCode = StatusCodes.Status400BadRequest;
                obj.Message = "Invalid data!";
                obj.Result = "";

                return BadRequest(obj);
            }
        }
              
    }
}
