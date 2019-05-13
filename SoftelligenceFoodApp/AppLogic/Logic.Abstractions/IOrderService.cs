using System.Collections.Generic;
using BusinessLogic;

namespace Logic.Abstractions
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrders();
    }
}
