using Microsoft.AspNetCore.Mvc;
using OnlineFoodService.Models;

namespace OnlineFoodService.Repository
{
    public interface IRegister
    {
        Task<ActionResult<IEnumerable<UserDetail>>> Getusers();
        Task<ActionResult<UserDetail>> GetRegisterUser(int userId);
        Task<ActionResult<UserDetail>> PostRegisterUser(UserDetail register);
        Task<ActionResult<UserDetail>> DeleteRegisterUser(int userId);
        Task<ActionResult<UserDetail>> EditRegisterPassword(int userId, UserDetail register);
        Task<ActionResult<UserDetail>> EditRegisterUser(int userId, UserDetail register);
        Task<ActionResult<int>> GetUserCount();


    }
}
