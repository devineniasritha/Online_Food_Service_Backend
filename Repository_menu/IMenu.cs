using Microsoft.AspNetCore.Mvc;
using OnlineFoodService.Models;

namespace OnlineFoodService.Repository_menu
{
    public interface IMenu
    {
        Task<ActionResult<IEnumerable<Menu>>> GetMenus();
        Task<ActionResult<Menu>> GetMenu(int itemId);
        Task<ActionResult<Menu>> PostMenu(Menu menu);
        Task<ActionResult<Menu>> DeleteMenu(int itemId);
        Task<ActionResult<Menu>> EditMenu(int itemId,Menu menu);
        Task<ActionResult<int>> GetItemCount();
    }
}
