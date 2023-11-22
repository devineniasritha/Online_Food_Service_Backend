using Microsoft.AspNetCore.Mvc;
using OnlineFoodService.Models;

namespace OnlineFoodService.Repository_cart
{
    public interface ICart
    {
        Task<ActionResult<IEnumerable<Cart>>> GetCarts();
        Task<ActionResult<Cart>> GetCart(int cartId);
        Task<ActionResult<Cart>> PostCart(Cart cart);
        Task<ActionResult<Cart>> DeleteCart(int cartId);
        Task<ActionResult<Cart>> EditCart(int cartId,Cart cart);
    }
}
