using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreWebApi.Context;
using StoreWebApi.Models;

namespace StoreWebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public ProductsController (StoreContext storeContext)
        {
            _storeContext = storeContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _storeContext.Products.ToListAsync();

            if (products != null)
            {
                return Ok(products);
            }

            return NotFound();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _storeContext.Products.
                FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            _storeContext.Products.Add(product);

            await _storeContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product productUpdated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var product = await (_storeContext.Products.FirstOrDefaultAsync(p => p.Id == id));

            if (product == null)
            {
                return NotFound();
            }

            UpdateProduct(productUpdated, product);

            await _storeContext.SaveChangesAsync();
            return Ok(product);

        }

        private static void UpdateProduct(Product productUpdated, Product product)
        {
            product.BarCode = productUpdated.BarCode;
            product.Description = productUpdated.Description;
            product.Active = productUpdated.Active;
            product.ProductType = productUpdated.ProductType;
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var product = await (_storeContext.Clients.FirstOrDefaultAsync(p => p.Id == id));

            if (product == null)
            {
                return NotFound();
            }
            _storeContext.Remove(product);
            await _storeContext.SaveChangesAsync();

            return Ok(product);
        }
    }
}
