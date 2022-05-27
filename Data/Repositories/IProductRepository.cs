using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechApp
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> ItemByIdAsync(string productId);
        Task<Picture> GetImageAsync(string pictureId);
    }
}
