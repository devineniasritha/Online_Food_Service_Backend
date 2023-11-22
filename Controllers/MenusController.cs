using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodService.Models;
using OnlineFoodService.Repository_menu;
using OnlineFoodService.Repository_order;

namespace OnlineFoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenu _repository;

        public MenusController(IMenu repository)
        {
            _repository = repository;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            return await _repository.GetMenus();
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {

            await _repository.PostMenu(menu);
            //var createdPost = await _repository.PostRegisterUser(employee);

            //return CreatedAtAction("GetEmployer", new { id = employee.Id }, createdPost);
            return Ok();

        }

        [HttpDelete("{itemId}")]
        [Authorize]
        public async Task<ActionResult<Menu>> DeleteMenu(int itemId)
        {
            try
            {
                return await _repository.DeleteMenu(itemId);

            }

            catch (Exception ex)
            {
                return NotFound();
            }

        }

        [HttpGet("{itemId}")]
        [Authorize]
        public async Task<ActionResult<Menu>> GetMenu(int itemId)
        {
            try
            {
                return await _repository.GetMenu(itemId);
            }

            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut("{itemId}")]
        [Authorize]
        public async Task<ActionResult<Menu>> EditMenu(int itemId,Menu menu)
        {
            if (itemId != menu.ItemId)
            {
                return BadRequest();
            }

            try
            {
                return await _repository.EditMenu(itemId,menu);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (itemId != menu.ItemId)
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

        [HttpGet("totalitemcount")]
        public async Task<ActionResult<int>> GeItemCount()
        {
            var totalItemCount = await _repository.GetItemCount();
            return totalItemCount;
        }
    }
}
