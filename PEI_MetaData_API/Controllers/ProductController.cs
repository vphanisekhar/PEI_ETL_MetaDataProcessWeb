using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEI_ETL.Services.Interfaces;
using UnitOfWorkDemo.Core.Models;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace PEI_ETL_MetaDataProcess_APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,  Roles = "uma_protection")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //public readonly IProductService _productService;
        //public ProductController(IProductService productService)
        //{
        //    _productService = productService;
        //}


        private readonly ProductService _productService;

        public ProductController(ProductService service)
        {
            _productService = service;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productDetailsList = await _productService.GetProductAsync();
            if (productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList);
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
        public async Task<IActionResult> CreateProduct(ProductDetailsDTO productDetails)
        {
            var isProductCreated = await _productService.InsertAsync(productDetails);
            await _productService.CompletedAsync();

            if (isProductCreated)
            {
                return Ok(isProductCreated);
            }
            else
            {
                return BadRequest();
            }       
        }

        /// <summary>
        /// Update the product
        /// </summary>
        /// <param name="productDetails"></param>
        ///// <returns></returns>
        //[HttpPut]
        //public async Task<IActionResult> UpdateProduct(ProductDetails productDetails)
        //{
        //    if (productDetails != null)
        //    {
        //        var isProductCreated = await _productService.UpdateProduct(productDetails);
        //        if (isProductCreated)
        //        {
        //            return Ok(isProductCreated);
        //        }
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

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
