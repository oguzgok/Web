using Microsoft.AspNetCore.Mvc;
using Web.MVC.Redis.Models;
using Web.MVC.Redis.Repository;

namespace Web.MVC.Redis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productRepository.GetAsync());
        }
        // www.api.com/products/10
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productRepository.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Product product)
        {
            return Created(string.Empty, await _productRepository.CreateAsync(product));
        }

    }
}
