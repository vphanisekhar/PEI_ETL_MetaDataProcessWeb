using Microsoft.AspNetCore.Mvc;
using PEI_ETL.Services.DTO;
using PEI_ETL.Services.Service;
using UnitOfWorkDemo.Core.Models;

namespace PEI_ETL_MetaDataProcessWeb.Controllers
{

    public class ProductController : Controller
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var Products = await _service.GetProductAsync();
            return View(Products);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(ProductDetailsDTO Product)
        {
            try
            {
                await _service.InsertAsync(Product);
                await _service.CompletedAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
