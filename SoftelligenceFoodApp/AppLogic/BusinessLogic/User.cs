using System.Collections.Generic;
using System.Linq;
using BusinessLogic.BusinessExceptions;

namespace BusinessLogic
{
    public class User
    {
        private List<Order> OrderHistory = new List<Order>();
        private Order CurrentOrder;

        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public void PlaceOrder(Order newOrder)
        {
            CurrentOrder = newOrder;
        }
        public void ChangeOrder(Order newOrder)
        {
            CurrentOrder = newOrder;
        }
        public void RemoveOrder()
        {
            CurrentOrder = null;
        }
        public Order GetOrder(int orderId)
        {
            if (orderId == CurrentOrder.Id)
            {
                return CurrentOrder;
            }

            var orderToView = OrderHistory.Where(order => order.Id == orderId).SingleOrDefault();
            if (orderToView == null)
            {
                throw new OrderNotFoundException();
            }
            return orderToView;
        }
        public void FinalizeOrder()
        {
            OrderHistory.Add(CurrentOrder);
            CurrentOrder = null;
        }
    }
}
