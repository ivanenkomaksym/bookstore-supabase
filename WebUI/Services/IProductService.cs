using WebUI.Models;

namespace WebUI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByCategory(string category);
        Task<Product> GetProduct(Guid productId);
    }
}
