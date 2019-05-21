using BusinessLogic.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BusinessLogic
{
    public class Session
    {
        private bool isActive;

        public int Id { get; set; }
        public ICollection<Store> Stores { get; set; }
        public ICollection<Order> Orders { get; set; }
        public DateTime StartTime { get; set; }

        public Session()
        {
            isActive = true;
        }

        public void AddStore(Store newStore)
        {
            Stores.Add(newStore);
        }
        public void RemoveStore(int storeId)
        {
            Stores.Remove(Stores.Where(store => store.Id == storeId).SingleOrDefault());
        }
        public void AddOrder(Order orderToAdd)
        {
            Orders.Add(orderToAdd);
        }
        public void UpdateOrder(int orderId, Order updatedOrder)
        {
            var orderToUpdate = Orders.Where(order => order.Id == orderId).SingleOrDefault();
            orderToUpdate = updatedOrder;
        }
        public void CancelOrder(int orderId)
        {
            Orders.Remove(Orders.Where(order => order.Id == orderId).SingleOrDefault());
        }
        public void Finalize()
        {
            isActive = false;
        }
    }
}
