using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Core.Entities;
using E_commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
namespace E_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<Product>>> GetProducts()
        {
            var result = await _context.Products.ToListAsync();
            return Ok(result);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product?>> GetProduct(int id)
        {
            Product? result = await _context.Products.FindAsync(id);
            if (result == null)
            {
                return NotFound("this Product Not Exist in Store");
            }
            else
                return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id); 
            if(product == null)
            {
                return NotFound("Product Not Found"); 
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id , Product product)
        {

            if (id != product.Id || !ProductsExist(id))
            {
                return BadRequest("Can Not Update This Product"); 
            }
            _context.Entry(product).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent() ;
        }

        private bool ProductsExist(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }

}
