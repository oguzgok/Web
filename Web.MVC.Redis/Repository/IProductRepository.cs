

using Web.MVC.Redis.Models;

namespace Web.MVC.Redis.Repository
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<List<Product>> GetAsync();
        Task<Product> GetByIdAsync(int id);
    }
}
