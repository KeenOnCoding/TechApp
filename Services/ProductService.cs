using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TechApp

{
    public class ProductService : IProductService
    {
        
        private readonly IWebHostEnvironment _env;
        private readonly ProductSettings _settings;
        private readonly IProductRepository _productReposiory;

        public ProductService(
            IProductRepository productReposiory,
            IOptionsSnapshot<ProductSettings> settings,
            IWebHostEnvironment env)
        {
            _productReposiory = productReposiory ?? throw new ArgumentNullException(nameof(productReposiory)); ;
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings.Value));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _productReposiory.GetAllProductsAsync();

            products.ChangeUriPlaceholder(_settings.PicBaseUrl);

            return products;
        }

        public async Task<Tuple<byte[], string>> GetImageAsync(string pictureId)
        {

            var item = await _productReposiory.GetImageAsync(pictureId);

            var webRoot = _env.WebRootPath;
            var path = Path.Combine(webRoot, item.Name);

            string imageFileExtension = Path.GetExtension(item.Name);
            string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

            var buffer = await File.ReadAllBytesAsync(path);

            return Tuple.Create(buffer, mimetype);
        }

        public async Task<Product> ItemByIdAsync(string productId)
        {
            var product = await _productReposiory.ItemByIdAsync(productId);
            product.FillProductUrl(_settings.PicBaseUrl);
            return product;
        }

        private string GetImageMimeTypeFromImageFileExtension(string extension)
        {
            string mimetype;

            switch (extension)
            {
                case ".png":
                    mimetype = "image/png";
                    break;
                case ".gif":
                    mimetype = "image/gif";
                    break;
                case ".jpg":
                case ".jpeg":
                    mimetype = "image/jpeg";
                    break;
                case ".bmp":
                    mimetype = "image/bmp";
                    break;
                case ".tiff":
                    mimetype = "image/tiff";
                    break;
                case ".wmf":
                    mimetype = "image/wmf";
                    break;
                case ".jp2":
                    mimetype = "image/jp2";
                    break;
                case ".svg":
                    mimetype = "image/svg+xml";
                    break;
                default:
                    mimetype = "application/octet-stream";
                    break;
            }

            return mimetype;
        }
    }
}
