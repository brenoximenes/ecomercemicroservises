using ECommerceAPI.Products.Db.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProdustsProvider produstsProvider;

        public ProductsController(IProdustsProvider produstsProvider)
        {
            this.produstsProvider = produstsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await produstsProvider.GetProductsAsync();
            if (result.IsSucces)
            {
                return Ok(result.Products);
            
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await produstsProvider.GetProductAsync(id);
            if (result.IsSucces)
            {
                return Ok(result.Product);
            }
            return NotFound();

        }
    }
}
