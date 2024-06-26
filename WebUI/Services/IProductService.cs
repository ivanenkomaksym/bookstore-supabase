﻿using WebUI.Models;

namespace WebUI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductWithStock>> GetProducts();
        Task<IEnumerable<ProductWithStock>> GetProductsByCategory(string category);
        Task<ProductWithStock> GetProduct(int productId);
    }
}
