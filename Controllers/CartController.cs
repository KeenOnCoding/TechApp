
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace TechApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;

        private readonly ApplicationDbContext _catalogContext;
        public CartController(ICartRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
            _catalogContext = context;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerCart>> GetCartByIdAsync(string id)
        {
            var basket = await _repository.GetCartAsync(id);

            return Ok(basket ?? new CustomerCart(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerCart>> UpdateCartAsync([FromBody] CustomerCart value)
        {
            return Ok(await _repository.UpdateCartAsync(value));
        }


        [Route("checkout")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> CheckoutAsync([FromBody] CartCheckout basketCheckout)
        {
            _catalogContext.Orders.Add(new Order
            {
                FirstName = basketCheckout.FirstName,
                LastName = basketCheckout.LastName,
                Email = basketCheckout.Email,
                Subject = basketCheckout.Subject,
                Town = basketCheckout.Town,
                State = basketCheckout.State,
                PostalCode = basketCheckout.PostalCode,
                Phone = basketCheckout.Phone,
                Content = basketCheckout.Content,
            });

            await _catalogContext.SaveChangesAsync();

            return Accepted();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteCartByIdAsync(string id)
        {
            await _repository.DeleteCartAsync(id);
        }
    }
}
