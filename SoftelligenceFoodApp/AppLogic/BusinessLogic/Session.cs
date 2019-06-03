using BusinessLogic.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BusinessLogic
{
    public class Session
    {

        public int Id { get; set; }
        public ICollection<Store> Stores { get; set; }
        public ICollection<Order> Orders { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsActive { get; private set; }


        public Session()
        {
            IsActive = true;
            Stores = new List<Store>();
            Orders = new List<Order>();
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
            orderToUpdate.Price = updatedOrder.Price;
            orderToUpdate.Store = updatedOrder.Store;
            orderToUpdate.User = updatedOrder.User;
            orderToUpdate.IsActive = updatedOrder.IsActive;
            orderToUpdate.Details = updatedOrder.Details;

        }
        public void CancelOrder(int orderId)
        {
            Orders.Remove(Orders.Where(order => order.Id == orderId).FirstOrDefault());

        }
        public void Finalize()
        {
            IsActive = false;
        }
    }
}
