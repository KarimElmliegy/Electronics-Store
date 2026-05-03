using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Core.Entities;
using E_commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using E_commerce.Core.Interfaces;
using E_commerce.Core.Specification;
namespace E_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> _GproductRepository) : ControllerBase
    {
        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Product>>> GetProducts(string? brand , string? type, string? Sort)
        {
            var spec = new ProductFilterSpecification(brand, type,Sort);

            var products = await _GproductRepository.ListWithSpecificationAsynch(spec);
            return Ok(products);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product?>> GetProduct(int id)
        {
            Product? result = await _GproductRepository.GetByIdAsynch(id);
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
            _GproductRepository.Add(product);
            if(await _GproductRepository.SaveAsynch())
            {
                return Ok(product); 
            }
            return BadRequest("Not Added");
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product =  await _GproductRepository.GetByIdAsynch(id);
            if(product == null)
            {
                return NotFound("Product Not Exist");
            }
            _GproductRepository.Delete(product);
            if (await _GproductRepository.SaveAsynch())
            {
                return NoContent();
            }
            return BadRequest("Can not delete Product"); 
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id , Product product)
        {

            if (id != product.Id || !ProductsExist(id))
            {
                return BadRequest("Can Not Update This Product"); 
            }
            _GproductRepository.Update(product);
            if (await _GproductRepository.SaveAsynch())
                return NoContent() ;

            return BadRequest("Can Not Update");
        }

        private bool ProductsExist(int id)
        {
            return _GproductRepository.Exists(id);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyCollection<Product>>> GetAllBtands()
        {
            var Spec = new BrandListSpecification();
            return Ok(await _GproductRepository.ListAsync(Spec));
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyCollection<Product>>> GetAllTypes()
        {
            var Spec = new TypeListSpecification();
            return Ok(await _GproductRepository.ListAsync(Spec));
        }
    }

}
