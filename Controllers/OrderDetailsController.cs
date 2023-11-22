using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodService.Models;
using OnlineFoodService.Repository_order;

namespace OnlineFoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrder _repository;

        public OrderDetailsController(IOrder repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrders()
        {
            return await _repository.GetOrders();
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<OrderDetail>> PostOrder(OrderDetail order)
        {

            await _repository.PostOrder(order);
            //var createdPost = await _repository.PostRegisterUser(employee);

            //return CreatedAtAction("GetEmployer", new { id = employee.Id }, createdPost);
            return Ok();

        }

        [HttpDelete("{orderId}")]
        [Authorize]
        public async Task<ActionResult<OrderDetail>> DeleteOrder(int orderId)
        {
            try
            {
                return await _repository.DeleteOrder(orderId);

            }

            catch (Exception ex)
            {
                return NotFound();
            }

        }

        [HttpGet("{orderId}")]
        [Authorize]
        public async Task<ActionResult<OrderDetail>> GetOrder(int orderId)
        {
            try
            {
                return await _repository.GetOrder(orderId);
            }

            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut("{orderId}")]
        [Authorize]
        public async Task<ActionResult<OrderDetail>> EditOrder(int orderId,OrderDetail order)
        {
            if (orderId !=order.OrderId)
            {
                return BadRequest();
            }

            try
            {
                return await _repository.EditOrder(orderId,order);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (orderId != order.OrderId)
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

        [HttpGet("totalordercount")]
        public async Task<ActionResult<int>> GetOrderCount()
        {
            var totalOrderCount = await _repository.GetOrderCount();
            return totalOrderCount;
        }
    }
}
