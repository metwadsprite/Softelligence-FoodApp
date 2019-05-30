using BusinessLogic;
using EF.DataAccess;
using BusinessLogic.Abstractions;

namespace Logic.Implementations
{
    public class UserService
    {
        public User user;
        private readonly IPersistenceContext dataContext;
        private ApplicationDbContext dbContext;

        public UserService(IPersistenceContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void SelectCurrentUser(string email)
        {
            this.user = dataContext.GetUsersRepository().GetByEmail(email);
        }

        private Session GetActiveSession()
        {
            return dataContext.GetSessionsRepository().GetActiveSession();
        }

        public void PlaceOrder(Store store, MenuItem menuItem, string userEmail)
        {
            Session currentSession = GetActiveSession();

            Order newOrder = new Order();
            newOrder.Store = store;
            newOrder.Price = menuItem.Price;
            newOrder.Details = menuItem.Details;
            newOrder.IsActive = true;
            this.SelectCurrentUser(userEmail);
            newOrder.User = this.user;

            currentSession.AddOrder(newOrder);
            dataContext.GetSessionsRepository().Update(currentSession);

        }
        //order info to change
        public void ChangeOrder(Store store, MenuItem menuItem, int orderId)
        {
            Session currentSession = GetActiveSession();

            Order newOrder = new Order();
            newOrder.Store = store;
            newOrder.Price = menuItem.Price;
            newOrder.Details = menuItem.Details;
            newOrder.User = this.user;

            //currentSession.ChangeOrder(newOrder);
            currentSession.UpdateOrder(orderId, newOrder);
            dataContext.GetSessionsRepository().Update(currentSession);
        }


        public void LoadOrder(Order loadedOrder)
        {
            user.LoadOrder(loadedOrder);
        }

        public void CancelOrder(Order orderToCancel)
        {
            Session currentSession = GetActiveSession();

            user.CancelOrder();
            dataContext.GetSessionsRepository().DeleteOrder(orderToCancel);
        }
        public Order GetCurrentOrder()
        {
            return user.GetCurrentOrder();
        }
    }
}
