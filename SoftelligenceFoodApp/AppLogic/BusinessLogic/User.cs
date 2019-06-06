using BusinessLogic.BusinessExceptions;

namespace BusinessLogic
{
    public class User
    {
        private Order CurrentOrder { get; set; }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public void PlaceOrder(Order newOrder)
        {
            if (CurrentOrder != null)
            {
                throw new OrderAlreadyInProgressException();
            }
            CurrentOrder = newOrder;

        }
        public void ChangeOrder(Order newOrder)
        {
            if (CurrentOrder.Id == newOrder.Id)
            {
                throw new OrderHistoryAccessException();
            }
            CurrentOrder = newOrder;
        }


        public void LoadOrder(Order loadedOrder)
        {
            CurrentOrder = loadedOrder;
        }

        public void CancelOrder()
        {
            CurrentOrder = null;
        }
        public Order GetCurrentOrder()
        {
            var orderToReturn = CurrentOrder;
            if (orderToReturn == null)
            {
                throw new OrderInvalidIdException();
            }
            return orderToReturn;
        }
    }
}
