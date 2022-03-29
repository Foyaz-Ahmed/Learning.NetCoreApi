using DempApiCore.Model;
using DempApiCore.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DempApiCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("api/products/all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetProductsAsync();

            return Ok(products);
        }

        [HttpGet("api/product/{id}")]
        public async Task<IActionResult> GetSingleProductById([FromRoute] int id)
        {
            var products = await _productRepository.GetProductsByIdAsync(id);

            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost("api/product/create")]
        public async Task<IActionResult> AddProduct([FromBody] ProductsModel product)
        {

            var id = await _productRepository.AddProducts(product);
            return CreatedAtAction(nameof(GetSingleProductById), new { id = id, controller = "product" }, id);
        }

        [HttpPut("api/product/{id}")]
        public async Task<IActionResult> UpdateProduct ([FromBody] ProductsModel product, [FromRoute] int id)
        {

             await _productRepository.UpdateProductAsync(product, id);
             return Ok();

        }

        [HttpPatch("api/product/{id}")]
        public async Task<IActionResult> UpdateProductPatch([FromBody] JsonPatchDocument product, [FromRoute] int id)
        {

            await _productRepository.UpdateProductPatchAsync(product, id);
            return Ok();

        }

        [HttpDelete("api/product/{id}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
        {

            await _productRepository.ProductDeleteAsync(id);
            return Ok();

        }

    }
}
