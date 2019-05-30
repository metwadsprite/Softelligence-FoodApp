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
            newOrder.User = user;
            newOrder.Price = menuItem.Price;
            newOrder.Details = menuItem.Details;
            newOrder.IsActive = true;
            this.SelectCurrentUser(userEmail);
            newOrder.User = this.user;

            currentSession.AddOrder(newOrder);
            dataContext.GetSessionsRepository().Update(currentSession);

        }
        //order info to change
        public void ChangeOrder(Store store, MenuItem menuItem)
        {
            Order newOrder = new Order();
            newOrder.Store = store;
            newOrder.User = user;
            newOrder.Price = menuItem.Price;
            newOrder.Details = menuItem.Details;

            user.ChangeOrder(newOrder);
        }
        public void CancelOrder()
        {
            user.CancelOrder();
        }
        public Order GetCurrentOrder()
        {
            return user.GetCurrentOrder();
        }
    }
}
