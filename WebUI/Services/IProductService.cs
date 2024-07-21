using WebUI.Models;

namespace WebUI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<StockItem>> GetProducts();
        Task<IEnumerable<StockItem>> GetProductsByCategory(string category);
        Task<StockItem> GetProduct(int productId);
    }
}
