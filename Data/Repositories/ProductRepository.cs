using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TechApp
{
    public class ProductRepository: GenericRepository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _productContext;
        private readonly IWebHostEnvironment _env;
        private readonly ProductSettings _settings;

        public ProductRepository(ApplicationDbContext context,
            IOptionsSnapshot<ProductSettings> settings,
            IWebHostEnvironment env) : base(context)
        {
            _productContext = context;
            _env = env;
            _settings = settings.Value;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products=  await Get()
                .Include(picture => picture.Pictures)
                .Include(picture => picture.Tags)
                .Include(picture => picture.Colors)
                .AsNoTracking()
                .ToListAsync();

            return products;
        }

        public async Task<Picture> GetImageAsync(string pictureId)
        {
            var item = await _productContext.Pictures
                .AsNoTracking()
                .SingleOrDefaultAsync(ci => ci.Id == Guid.Parse(pictureId));


            return item;
        }

        public async Task<Product> ItemByIdAsync(string productId)
        {
            var product =  await Get()
                .Include(picture => picture.Pictures)
                .Include(picture => picture.Tags)
                .Include(picture => picture.Colors)
                .AsNoTracking()
                .SingleOrDefaultAsync(ci => ci.Id == Guid.Parse(productId));

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
