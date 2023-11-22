using Microsoft.AspNetCore.Mvc;
using OnlineFoodService.Models;

namespace OnlineFoodService.Repository_order
{
    public interface IOrder
    {
        Task<ActionResult<IEnumerable<OrderDetail>>> GetOrders();
        Task<ActionResult<OrderDetail>> GetOrder(int orderId);
        Task<ActionResult<OrderDetail>> PostOrder(OrderDetail order);
        Task<ActionResult<OrderDetail>> DeleteOrder(int orderId);
        Task<ActionResult<OrderDetail>> EditOrder(int orderId,OrderDetail order);
        Task<ActionResult<int>> GetOrderCount();
    }
}
