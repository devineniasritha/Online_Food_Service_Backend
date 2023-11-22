using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodService.Models;
using OnlineFoodService.Repository_cart;

namespace OnlineFoodService.Repository_order
{
    public class Sqlorder:IOrder
    {
        private readonly onlinefoodservicecontext _dbcontext;
        private readonly IMapper _mapper;


        public Sqlorder(onlinefoodservicecontext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrders()
        {
            var OrderItems = _dbcontext.OrderDetails
                .Include(OrderDetail => OrderDetail.Cart)
                .Include(OrderDetail => OrderDetail.Cart.Item)
                .Include(OrderDetail => OrderDetail.Cart.User)
                .ToList();
            return OrderItems;
        }

        public async Task<ActionResult<OrderDetail>> PostOrder(OrderDetail order)
        {
            var cart = await _dbcontext.Carts.FindAsync(order.CartId);
            if (cart == null)
            {
                throw new NullReferenceException("Item  not found");
            }
            var orderItemToAdd = new OrderDetail
            {
                CartId = order.CartId,
                OrderTime = order.OrderTime,
                Payment = order.Payment,
                Status = order.Status
            };

            _dbcontext.OrderDetails.Add(orderItemToAdd);
            await _dbcontext.SaveChangesAsync();

            return order;
        }

        public async Task<ActionResult<OrderDetail>> DeleteOrder(int orderId)
        {
            var order = await _dbcontext.OrderDetails.FindAsync(orderId);
            if (order == null)
            {
                throw new NullReferenceException("Sorry, order not found.");
            }
            else
            {
                _dbcontext.OrderDetails.Remove(order);
                await _dbcontext.SaveChangesAsync();

                return order;
            }
        }

        public async Task<ActionResult<OrderDetail>> EditOrder(int orderId,OrderDetail order)
        {
            _dbcontext.Entry(order).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return order;
        }

        public async Task<ActionResult<OrderDetail>> GetOrder(int orderId)
        {
            var order = await _dbcontext.OrderDetails
                .Include(c => c.Cart)
                .Include(c => c.Cart.User)
                .Include(c => c.Cart.Item)
                .FirstOrDefaultAsync(c => c.OrderId == orderId);


            if (order == null)
            {
                throw new NullReferenceException("Sorry, User not found.");
            }

            return order;

        }

        public async Task<ActionResult<int>> GetOrderCount()
        {
            return await _dbcontext.OrderDetails.CountAsync();
        }
    }
}
