using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodService.Models;

namespace OnlineFoodService.Repository_cart
{
    public class Sqlcart:ICart
    {
        private readonly onlinefoodservicecontext _dbcontext;
        private readonly IMapper _mapper;


        public Sqlcart(onlinefoodservicecontext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            var CartItems = _dbcontext.Carts
                .Include(Cart => Cart.Item)
                .Include(Cart => Cart.User)
                .ToList();
            return CartItems;
        }

        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            var menu = await _dbcontext.Menu.FindAsync(cart.ItemId);
            var user = await _dbcontext.UserDetails.FindAsync(cart.UserId);
            if (menu == null || user == null)
            {
                throw new NullReferenceException("Item  not found");
            }
            var cartItemToAdd = new Cart
            {
                UserId = cart.UserId,
                ItemId = cart.ItemId,
                Quantity = cart.Quantity,
                Status = cart.Status
            };

            _dbcontext.Carts.Add(cartItemToAdd);
            await _dbcontext.SaveChangesAsync();

            return cart;
        }

        public async Task<ActionResult<Cart>> DeleteCart(int cartId)
        {
            var cart = await _dbcontext.Carts.FindAsync(cartId);
            var order = await _dbcontext.OrderDetails
            .Where(c => c.CartId == cartId)
            .FirstOrDefaultAsync();
            if (cart == null)
            {
                throw new NullReferenceException("Sorry, item not found.");
            }
            else
            {
                if (order != null)
                {
                    _dbcontext.OrderDetails.Remove(order);
                    _dbcontext.Carts.Remove(cart);
                    await _dbcontext.SaveChangesAsync();
                }

                else
                {
                    _dbcontext.Carts.Remove(cart);
                    await _dbcontext.SaveChangesAsync();
                }

                return cart;
            }
        }

        public async Task<ActionResult<Cart>> GetCart(int cartId)
        {
            var cart = await _dbcontext.Carts
                .Include(c => c.Item)  // Include the related Items
                .Include(c => c.User)   // Include the related Menu
                .FirstOrDefaultAsync(c => c.CartId == cartId);

            if (cart == null)
            {
                throw new NullReferenceException("Sorry, User not found.");
            }

            

            return cart;
        }

        public async Task<ActionResult<Cart>> EditCart(int cartId,Cart cart)
        {
            _dbcontext.Entry(cart).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return cart;
        }
    }
}
