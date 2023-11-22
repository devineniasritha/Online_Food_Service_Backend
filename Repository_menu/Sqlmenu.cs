using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodService.Models;
using OnlineFoodService.Repository_order;

namespace OnlineFoodService.Repository_menu
{
    public class Sqlmenu:IMenu
    {
        private readonly onlinefoodservicecontext _dbcontext;
        private readonly IMapper _mapper;


        public Sqlmenu(onlinefoodservicecontext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            return await _dbcontext.Menu.ToListAsync();
        }

        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {
            _dbcontext.Menu.Add(menu);
            await _dbcontext.SaveChangesAsync();

            return menu;
        }

        public async Task<ActionResult<Menu>> DeleteMenu(int itemId)
        {
            var menu = await _dbcontext.Menu.FindAsync(itemId);
            var cart = await _dbcontext.Carts
            .Where(c => c.ItemId == itemId)
            .FirstOrDefaultAsync();
            var order = await _dbcontext.OrderDetails
            .Where(c => c.Cart.ItemId == itemId)
            .FirstOrDefaultAsync();
            if (menu == null)
            {
                throw new NullReferenceException("Sorry, item not found.");
            }
            else
            {
                if (cart != null)
                {
                    if(order != null)
                    {
                        _dbcontext.OrderDetails.Remove(order);
                        _dbcontext.Carts.Remove(cart);
                        _dbcontext.Menu.Remove(menu);
                        await _dbcontext.SaveChangesAsync();
                    }
                    else
                    {
                        _dbcontext.Carts.Remove(cart);
                        _dbcontext.Menu.Remove(menu);
                        await _dbcontext.SaveChangesAsync();
                    }
                }

                else
                {
                    _dbcontext.Menu.Remove(menu);
                    await _dbcontext.SaveChangesAsync();
                }

                return menu;
            }
        }


        public async Task<ActionResult<Menu>> GetMenu(int itemId)
        {
            var menu = await _dbcontext.Menu.FindAsync(itemId);

            if (menu == null)
            {
                throw new NullReferenceException("Sorry, User not found.");
            }

            return menu;
        }

        public async Task<ActionResult<Menu>> EditMenu(int itemId,Menu menu)
        {
            _dbcontext.Entry(menu).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return menu;
        }

        public async Task<ActionResult<int>> GetItemCount()
        {
            return await _dbcontext.Menu.CountAsync();

        }
    }
}
