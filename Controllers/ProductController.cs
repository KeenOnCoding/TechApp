using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TechApp
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //private readonly ApplicationDbContext _productContext;
        private readonly IWebHostEnvironment _env;
        private readonly ProductSettings _settings;
        public readonly IProductService _productService;

        public ProductController(ApplicationDbContext context, 
            IOptionsSnapshot<ProductSettings> settings,
            IWebHostEnvironment env, 
            IProductService productService)
        {
           // _productContext = context ?? throw new ArgumentNullException(nameof(context));
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings.Value));
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProductsAsync()
        {

            return Ok(await _productService.GetAllProductsAsync());
        }


        [HttpGet]
        [Route("{productId}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ItemByIdAsync(string productId)
        {
            if (!Guid.TryParse(productId, out Guid x))
            {
                return BadRequest();
            }

            var item = await _productService.ItemByIdAsync(productId);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }



        [HttpGet]
        [Route("{pictureId}/pic")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetImageAsync(string pictureId)
        {
            if (!Guid.TryParse(pictureId, out Guid x))
            {
                return BadRequest();
            }
            var item = await _productService.GetImageAsync(pictureId);  
               
            if (item != null)
            {

                return File(item.Item1, item.Item2);
            }
            return NotFound();

        }
    }
}
