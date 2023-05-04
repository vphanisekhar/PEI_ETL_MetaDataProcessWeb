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
    [Route("api/[controller]")]
    [ApiController]
    public class ETLBatchSrcController : ControllerBase
    {
        //public readonly IProductService _productService;
        //public ProductController(IProductService productService)
        //{
        //    _productService = productService;
        //}


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
        public async Task<IActionResult> GetETLBatchSrcList()
        {
            var eTLBatchSrcList = await _eTLBatchSrcService.GetETLBatchSrcAsync();
            if (eTLBatchSrcList == null)
            {
                return NotFound();
            }
            return Ok(eTLBatchSrcList);
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        //[HttpGet("{productId}")]
        //public async Task<IActionResult> GetProductById(int productId)
        //{
        //    var productDetails = await _productService.GetProductById(productId);

        //    if (productDetails != null)
        //    {
        //        return Ok(productDetails);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="productDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateETLBatchSrc(ETLBatchSrcDTO eTLBatchSrc)
        {
            var isETLBatchSrcCreated = await _eTLBatchSrcService.InsertAsync(eTLBatchSrc);
            await _eTLBatchSrcService.CompletedAsync();

            if (isETLBatchSrcCreated)
            {
                return Ok(isETLBatchSrcCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update ETL batch src
        /// </summary>
        /// <param name="eTLBatchSrc"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateETLBatchSrc(ETLBatchSrc eTLBatchSrc)
        {
            if (eTLBatchSrc != null)
            {
                var iseTLBatchSrcUpdated = await _eTLBatchSrcService.UpdateETLBatchSrc(eTLBatchSrc);
                await _eTLBatchSrcService.CompletedAsync();

                if (iseTLBatchSrcUpdated)
                {
                    return Ok(iseTLBatchSrcUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        ///// <summary>
        ///// Delete product by id
        ///// </summary>
        ///// <param name="productId"></param>
        ///// <returns></returns>
        //[HttpDelete("{productId}")]
        //public async Task<IActionResult> DeleteProduct(int productId)
        //{
        //    var isProductCreated = await _productService.DeleteProduct(productId);

        //    if (isProductCreated)
        //    {
        //        return Ok(isProductCreated);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
