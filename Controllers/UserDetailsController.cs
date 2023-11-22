using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodService.Models;
using OnlineFoodService.Repository;

namespace OnlineFoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IRegister _repository;
        private readonly onlinefoodservicecontext _dbcontext;

        public UserDetailsController(IRegister repository, onlinefoodservicecontext dbcontext)
        {
            _repository = repository;
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<UserDetail>>> Getusers()
        {
            return await _repository.Getusers();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult<UserDetail>> PostRegisterUser(UserDetail register)
        {

            await _repository.PostRegisterUser(register);
            //var createdPost = await _repository.PostRegisterUser(employee);

            //return CreatedAtAction("GetEmployer", new { id = employee.Id }, createdPost);
            return Ok();

        }

        [HttpDelete("{userId}")]
        [Authorize]
        public async Task<ActionResult<UserDetail>> DeleteRegisterUser(int userId)
        {
            try
            {
                return await _repository.DeleteRegisterUser(userId);

            }

            catch (Exception ex)
            {
                return NotFound();
            }

        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<ActionResult<UserDetail>> GetRegisterUser(int userId)
        {
            try
            {
                return await _repository.GetRegisterUser(userId);
            }

            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut("change-password/{userId}")]
        [Authorize]
        public async Task<ActionResult<UserDetail>> EditRegisterPassword(int userId, UserDetail register)
        {
            if (userId != register.UserId)
            {
                return BadRequest();
            }

            try
            {
                return await _repository.EditRegisterPassword(userId, register);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (userId != register.UserId)
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

        [HttpPut("{userId}")]
        [Authorize]
        public async Task<ActionResult<UserDetail>> EditRegisterUser(int userId, UserDetail register)
        {
            if (userId != register.UserId)
            {
                return BadRequest();
            }

            try
            {
                return await _repository.EditRegisterUser(userId, register);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (userId != register.UserId)
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

        [HttpGet("totalusercount")]
        public async Task<ActionResult<int>> GetUserCount()
        {
            var totalUserCount = await _repository.GetUserCount();
            return totalUserCount;
        }

        [HttpGet("Check-Email&password")]
        public async Task<IActionResult> CheckEmailExists([FromQuery] string email)
        {
            var emailExists = await _dbcontext.UserDetails.AnyAsync(x => x.EmailId == email);
            return Ok(new { emailExists });
        }
    }
}
