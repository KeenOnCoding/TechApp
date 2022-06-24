using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
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
                new Product() {
                Name = "Wireless Mondo",
                Price = 266M,
                SalePrice= 200,
                Discount = 20,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 3,
                Brand = "Kenwood",
                Category = "Headphones",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("e0fbfd9b-f1e5-4f2c-a39c-c36ed6e6ad31"),
                        Name = "e0fbfd9b-f1e5-4f2c-a39c-c36ed6e6ad31.png"
                    },
                    new Picture() {
                        Id = new Guid("aab170ec-dc7f-4699-9b84-ae938a9626d9"),
                        Name = "aab170ec-dc7f-4699-9b84-ae938a9626d9.png"
                    },
                    new Picture() {
                        Id = new Guid("e3b6be25-9449-444b-8a62-2c66c3815bce"),
                        Name = "e3b6be25-9449-444b-8a62-2c66c3815bce.png"
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
                Name = "Wireless Mondo",
                Price = 80M,
                SalePrice= 70,
                Discount = 20,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 7,
                Brand = "Aiwa",
                Category = "Headphones",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("3f57b14c-ce0a-4505-9fee-418f4ef43b5c"),
                        Name = "3f57b14c-ce0a-4505-9fee-418f4ef43b5c.png"
                    },
                    new Picture() {
                        Id = new Guid("3470cf3b-a3c1-440a-8f77-017b097f69c4"),
                        Name = "3470cf3b-a3c1-440a-8f77-017b097f69c4.png"
                    },
                    new Picture() {
                        Id = new Guid("70fdd499-5771-4839-ab7c-fb012399cb15"),
                        Name = "70fdd499-5771-4839-ab7c-fb012399cb15.png"
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
                Name = "Dono Purple Wireless",
                Price = 100,
                SalePrice= 200,
                Discount = 50,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 12,
                Brand = "Philips",
                Category = "Headphones",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("d68ab434-4cfb-48cd-962f-6c3fea91b32f"),
                        Name = "d68ab434-4cfb-48cd-962f-6c3fea91b32f.png"
                    },
                    new Picture() {
                        Id = new Guid("b70e6be5-bfbb-417b-a254-60351c809e52"),
                        Name = "b70e6be5-bfbb-417b-a254-60351c809e52.png"
                    },
                    new Picture() {
                        Id = new Guid("69876b2e-3aef-42a8-b276-2c23898b116e"),
                        Name = "69876b2e-3aef-42a8-b276-2c23898b116e.png"
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
                Name = "Tablet  8500U 2TB",
                Price = 1000,
                SalePrice= 900,
                Discount = 50,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 9,
                Brand = "DELL",
                Category = "Laptops",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("7a4ca061-0259-4b46-beec-18e1d845df31"),
                        Name = "7a4ca061-0259-4b46-beec-18e1d845df31.png"
                    },
                    new Picture() {
                        Id = new Guid("f4394555-5863-4f0d-b0c6-d3effe212176"),
                        Name = "f4394555-5863-4f0d-b0c6-d3effe212176.png"
                    },
                    new Picture() {
                        Id = new Guid("a69b32bc-104e-42f6-8a59-ac93f5cb7a62"),
                        Name = "a69b32bc-104e-42f6-8a59-ac93f5cb7a62.png"
                    },

                },
                Tags = new Tag[] {
                    new Tag(){ Name = "lifestyle"}
                },
                Colors = new Color[]{
                    new Color(){ Name = "Grey"}
                }
            },
                new Product() {
                Name = "Pro Book 15",
                Price = 2000,
                SalePrice= 1900,
                Discount = 150,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 4,
                Brand = "Microsoft",
                Category = "Laptops",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("ad316b7b-bd90-439c-8114-26d7fc822cfa"),
                        Name = "ad316b7b-bd90-439c-8114-26d7fc822cfa.png"
                    },
                    new Picture() {
                        Id = new Guid("d02fe142-00e1-4a33-8a4e-4a699fb0dd9d"),
                        Name = "d02fe142-00e1-4a33-8a4e-4a699fb0dd9d.png"
                    },
                    new Picture() {
                        Id = new Guid("9664cf06-4107-40b0-b333-5f786ac0654c"),
                        Name = "9664cf06-4107-40b0-b333-5f786ac0654c.png"
                    },

                },
                Tags = new Tag[] {
                    new Tag(){ Name = "Modern"}
                },
                Colors = new Color[]{
                    new Color(){ Name = "Grey"}
                }
            },
                new Product() {
                Name = "Tablet VX3000 Extra Light",
                Price = 3000,
                SalePrice= 2900,
                Discount = 160,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 10,
                Brand = "Samsung",
                Category = "Laptops",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("f78efd78-e429-4872-b502-564698dccde7"),
                        Name = "f78efd78-e429-4872-b502-564698dccde7.png"
                    },
                    new Picture() {
                        Id = new Guid("bf7f1ec7-05ef-407d-ba11-e95801af4860"),
                        Name = "bf7f1ec7-05ef-407d-ba11-e95801af4860.png"
                    },
                    new Picture() {
                        Id = new Guid("d916e56a-bdba-4010-959a-182c2e460452"),
                        Name = "d916e56a-bdba-4010-959a-182c2e460452.png"
                    },

                },
                Tags = new Tag[] {
                    new Tag(){ Name = "Modern"}
                },
                Colors = new Color[]{
                    new Color(){ Name = "Black"}
                }
            },
                new Product() {
                Name = "Tablet VX4000 8500 3TB",
                Price = 2000,
                SalePrice= 1800,
                Discount = 200,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 10,
                Brand = "Nokia",
                Category = "Laptops",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("99c4a5bc-ee40-4bc4-a83a-05084a6b4198"),
                        Name = "99c4a5bc-ee40-4bc4-a83a-05084a6b4198.png"
                    },
                    new Picture() {
                        Id = new Guid("c840b829-ea29-47a0-9fe7-137b93d31905"),
                        Name = "c840b829-ea29-47a0-9fe7-137b93d31905.png"
                    },
                    new Picture() {
                        Id = new Guid("c90fb106-1514-4457-b126-2d8b5097e33d"),
                        Name = "c90fb106-1514-4457-b126-2d8b5097e33d.png"
                    },

                },
                Tags = new Tag[] {
                    new Tag(){ Name = "Modern"}
                },
                Colors = new Color[]{
                    new Color(){ Name = "Black"}
                }
            },
                new Product() {
                Name = "Laptop Sens 7200L",
                Price = 700,
                SalePrice= 650,
                Discount = 100,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 10,
                Brand = "Dell",
                Category = "Laptops",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("420e3398-6e5e-45a8-8f18-15910025f313"),
                        Name = "420e3398-6e5e-45a8-8f18-15910025f313.png"
                    },
                    new Picture() {
                        Id = new Guid("d0acda9c-5fb3-448b-b910-a495b59fb24a"),
                        Name = "d0acda9c-5fb3-448b-b910-a495b59fb24a.png"
                    },
                    new Picture() {
                        Id = new Guid("c57d9938-e7cf-4ad9-801a-1750c1793a25"),
                        Name = "c57d9938-e7cf-4ad9-801a-1750c1793a25.png"
                    },

                },
                Tags = new Tag[] {
                    new Tag(){ Name = "Modern"}
                },
                Colors = new Color[]{
                    new Color(){ Name = "Black"}
                }
            },
                new Product() {
                Name = "Laptop XS3000 WiFi Smart",
                Price = 900,
                SalePrice= 850,
                Discount = 50,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 20,
                Brand = "Asus",
                Category = "Laptops",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("908fc02e-9aac-49ed-9cde-894a26ef4c7a"),
                        Name = "908fc02e-9aac-49ed-9cde-894a26ef4c7a.png"
                    },
                    new Picture() {
                        Id = new Guid("f7087710-cd44-4e2c-bb15-0d2aff21c7c5"),
                        Name = "f7087710-cd44-4e2c-bb15-0d2aff21c7c5.png"
                    },
                    new Picture() {
                        Id = new Guid("d6139407-1be4-4fa0-aa3a-1a0e7bb6d7b0"),
                        Name = "d6139407-1be4-4fa0-aa3a-1a0e7bb6d7b0.png"
                    },

                },
                Tags = new Tag[] {
                    new Tag(){ Name = "Modern"}
                },
                Colors = new Color[]{
                    new Color(){ Name = "Black"}
                }
            },
                new Product() {
                Name = "Smartphone Tablet Spring2000",
                Price = 400,
                SalePrice= 390,
                Discount = 50,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 20,
                Brand = "Samsung",
                Category = "Smartphones",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("9d2631e6-4871-49b5-b6d9-a891de74fec7"),
                        Name = "9d2631e6-4871-49b5-b6d9-a891de74fec7.png"
                    },
                    new Picture() {
                        Id = new Guid("00e828bb-e229-4461-be32-c859a145cab82"),
                        Name = "00e828bb-e229-4461-be32-c859a145cab82.png"
                    },
                    new Picture() {
                        Id = new Guid("09f1562c-3182-4d7c-ba3e-fb07b69c2960"),
                        Name = "09f1562c-3182-4d7c-ba3e-fb07b69c2960.png"
                    },

                },
                Tags = new Tag[] {
                    new Tag(){ Name = "Modern"}
                },
                Colors = new Color[]{
                    new Color(){ Name = "Black"}
                }
            },
                new Product() {
                Name = "Extra Thin Elitte",
                Price = 400,
                SalePrice= 390,
                Discount = 50,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 20,
                Brand = "Samsung",
                Category = "Smartphones",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("3edaf113-0345-4e02-85fd-761a74f4073f"),
                        Name = "3edaf113-0345-4e02-85fd-761a74f4073f.png"
                    },
                    new Picture() {
                        Id = new Guid("6666f613-28d9-4aac-b4cc-1800e9979e0a"),
                        Name = "6666f613-28d9-4aac-b4cc-1800e9979e0a.png"
                    },
                    new Picture() {
                        Id = new Guid("d1f6305f-0414-4e64-a831-d090db1f5109"),
                        Name = "d1f6305f-0414-4e64-a831-d090db1f5109.png"
                    },

                },
                Tags = new Tag[] {
                    new Tag(){ Name = "Modern"}
                },
                Colors = new Color[]{
                    new Color(){ Name = "Black"}
                }
            },
                new Product() {
                Name = "Notebook Polo 4000",
                Price = 500,
                SalePrice= 490,
                Discount = 70,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 10,
                Brand = "Asus",
                Category = "Smartphones",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("76f031ce-b64e-43fc-a2e6-d3337cd05e8d"),
                        Name = "76f031ce-b64e-43fc-a2e6-d3337cd05e8d.png"
                    },
                    new Picture() {
                        Id = new Guid("3e87cdf2-6a4b-4522-9130-65d2eb7056a1"),
                        Name = "3e87cdf2-6a4b-4522-9130-65d2eb7056a1.png"
                    },
                    new Picture() {
                        Id = new Guid("22095343-3ef2-4010-bf14-b5ca7f866c48"),
                        Name = "22095343-3ef2-4010-bf14-b5ca7f866c48.png"
                    },

                },
                Tags = new Tag[] {
                    new Tag(){ Name = "Modern"}
                },
                Colors = new Color[]{
                    new Color(){ Name = "Black"}
                }
            },
                new Product() {
                Name = "Smartphone Elitte Pro",
                Price = 699,
                SalePrice= 590,
                Discount = 20,
                ShortDetails = "Sed ut perspiciatis, unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam eaque ipsa, quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt, explicabo. Nemo enim ipsam voluptatem,",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                Stock = 10,
                Brand = "Brand-1",
                Category = "Smartphones",
                NewPro =  true,
                Sale =  true,
                Pictures = new Picture[]{
                    new Picture() {
                        Id = new Guid("1c1da291-6f7d-40e2-bc7b-902e4c9dc3a0"),
                        Name = "1c1da291-6f7d-40e2-bc7b-902e4c9dc3a0.png"
                    },
                    new Picture() {
                        Id = new Guid("e741d0f3-4aa3-49d1-96f1-449c6e158f5d"),
                        Name = "e741d0f3-4aa3-49d1-96f1-449c6e158f5d.png"
                    },
                    new Picture() {
                        Id = new Guid("b1541681-9d9f-4f5f-b4ac-8466b9588b79"),
                        Name = "b1541681-9d9f-4f5f-b4ac-8466b9588b79.png"
                    },

                },
                Tags = new Tag[] {
                    new Tag(){ Name = "Modern"}
                },
                Colors = new Color[]{
                    new Color(){ Name = "Black"}
                }
            }
            };
        }
    }
}
