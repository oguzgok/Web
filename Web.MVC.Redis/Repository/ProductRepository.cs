

using Microsoft.EntityFrameworkCore;
using Web.Library.Cache;
using Web.MVC.Redis.Models;

namespace Web.MVC.Redis.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
      
        public ProductRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
