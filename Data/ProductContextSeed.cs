using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TechApp
{
    public class ProductContextSeed
    {
        public async Task SeedAsync(ApplicationDbContext context, IWebHostEnvironment env, IOptions<ProductSettings> settings, ILogger<ProductContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(ProductContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var contentRootPath = env.ContentRootPath;
                var picturePath = env.WebRootPath;

                if (!context.Products.Any())
                {
                    await context.Products.AddRangeAsync(GetPreconfiguredProducts());

                    await context.SaveChangesAsync();
                }
            });
        }
        private AsyncRetryPolicy CreatePolicy(ILogger<ProductContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<Exception>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
        private IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new Product[] {
                new Product() {
                Name = "ONTEC E Headset",
                Price = 175M,
                SalePrice= 160,
                Discount = 50,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 5,
                Brand = "Apple",
                Category = "Headphones",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("8c9e15d2-0f8b-43ea-9c8e-4d8123532651"),
                        Name = "8c9e15d2-0f8b-43ea-9c8e-4d8123532651.png"
                    },
                    new Picture() {
                        Id = new Guid("af13e2f3-fe4f-42eb-b388-c8a478ce0e66"),
                        Name = "af13e2f3-fe4f-42eb-b388-c8a478ce0e66.png"
                    },
                    new Picture() {
                        Id = new Guid("c0815e5d-aef0-4f74-8e44-90fab8c66bea"),
                        Name = "c0815e5d-aef0-4f74-8e44-90fab8c66bea.png"
                    },

                },
                Tags = new Tag[] {
                    new Tag(){ Name = "nike"},
                    new Tag(){ Name = "caprese"},
                    new Tag(){ Name = "lifestyle"}
                },
                Colors = new Color[]{
                    new Color(){ Name = "Black"},
                    new Color(){ Name = "Red"},
                    new Color(){ Name = "White"},
                }
            },
                new Product() {
                Name = "Solo Headset",
                Price = 235M,
                SalePrice= 200,
                Discount = 50,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 10,
                Brand = "Samsung",
                Category = "Headphones",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("5f4d93ce-8cc1-4066-969f-658505799e7c"),
                        Name = "5f4d93ce-8cc1-4066-969f-658505799e7c.png"
                    },
                    new Picture() {
                        Id = new Guid("68c144d9-3d20-44a1-9d79-6783e83318c9"),
                        Name = "68c144d9-3d20-44a1-9d79-6783e83318c9.png"
                    },
                    new Picture() {
                        Id = new Guid("ca776f3b-c5b3-426a-a36e-012d18ec0ed6"),
                        Name = "ca776f3b-c5b3-426a-a36e-012d18ec0ed6.png"
                    },

                },
                Tags = new Tag[] {
                    new Tag(){ Name = "nike"},
                    new Tag(){ Name = "caprese"},
                    new Tag(){ Name = "lifestyle"}
                },
                Colors = new Color[]{
                    new Color(){ Name = "Black"},
                    new Color(){ Name = "Red"},
                    new Color(){ Name = "White"},
                }
            },
                new Product() {
                Name = "Ultra Whireless",
                Price = 275M,
                SalePrice= 260,
                Discount = 20,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 9,
                Brand = "Sony",
                Category = "Headphones",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("1bf17d67-2f9d-4069-9f40-4abab04851ed"),
                        Name = "1bf17d67-2f9d-4069-9f40-4abab04851ed.png"
                    },
                    new Picture() {
                        Id = new Guid("ebd717fa-e770-45ae-b016-73ae9719a033"),
                        Name = "ebd717fa-e770-45ae-b016-73ae9719a033.png"
                    },
                    new Picture() {
                        Id = new Guid("e0a82fd5-aef9-4d59-bf31-18de99f2e1bd"),
                        Name = "e0a82fd5-aef9-4d59-bf31-18de99f2e1bd.png"
                    },

                },
                Tags = new Tag[] {
                    new Tag(){ Name = "nike"},
                    new Tag(){ Name = "caprese"},
                    new Tag(){ Name = "lifestyle"}
                },
                Colors = new Color[]{
                    new Color(){ Name = "Black"},
                    new Color(){ Name = "Red"},
                    new Color(){ Name = "White"},
                }
            },
            };
        }
    }
}
