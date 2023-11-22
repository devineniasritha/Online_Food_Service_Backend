using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodService.Models;
using OnlineFoodService.Repository_cart;
using OnlineFoodService.Repository_order;

namespace OnlineFoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICart _repository;

        public CartsController(ICart repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await _repository.GetCarts();
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {

            await _repository.PostCart(cart);
            //var createdPost = await _repository.PostRegisterUser(employee);

            //return CreatedAtAction("GetEmployer", new { id = employee.Id }, createdPost);
            return Ok();

        }

        [HttpDelete("{cartId}")]
        [Authorize]
        public async Task<ActionResult<Cart>> DeleteCart(int cartId)
        {
            try
            {
                return await _repository.DeleteCart(cartId);

            }

            catch (Exception ex)
            {
                return NotFound();
            }

        }

        [HttpGet("{cartId}")]
        [Authorize]
        public async Task<ActionResult<Cart>> GetCart(int cartId)
        {
            try
            {
                return await _repository.GetCart(cartId);
            }

            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut("{cartId}")]
        [Authorize]
        public async Task<ActionResult<Cart>> EditCart(int cartId,Cart cart)
        {
            if (cartId != cart.CartId)
            {
                return BadRequest();
            }

            try
            {
                return await _repository.EditCart(cartId,cart);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (cartId != cart.CartId)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
