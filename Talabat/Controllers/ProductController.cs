using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.IRepository.Contract;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductController(IGenericRepository<Product> ProductRepo)
        {
            _productRepo = ProductRepo;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            var products = await _productRepo.GetAllAsync();
            if (products.Count() == 0) 
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetAsync(int id)
        {
            var product= await _productRepo.GetAsync(id);
            if (product == null) 
                return NotFound();
            return Ok(product);
        }
    }
}
