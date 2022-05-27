using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechApp
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> ItemByIdAsync(string productId);
        Task<Tuple<byte[], string>> GetImageAsync(string pictureId);

    }
}
