using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic;
using Logic.Abstractions;
using Abstractions;

namespace Logic.Implementations
{
    public class OrderService : IOrderService
    {
        private IPersistenceContext DataContext;

        public OrderService(IPersistenceContext dataContext)
        {
            this.DataContext = dataContext;
        }

        public IEnumerable<Order> GetOrders()
        {
            /// TODO :
            return null;
        }
    }
}
