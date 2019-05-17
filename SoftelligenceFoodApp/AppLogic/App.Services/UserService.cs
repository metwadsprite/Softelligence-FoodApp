using BusinessLogic;
using BusinessLogic.Abstractions;

namespace Logic.Implementations
{
    public class UserService
    {
        private User user;
        private readonly IPersistenceContext dataContext;

        public UserService(IPersistenceContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void SelectCurrentUser(int userId)
        {
            this.user = dataContext.GetUsersRepository().GetById(userId);
        }

        private Session GetActiveSession()
        {
            return dataContext.GetSessionsRepository().GetActiveSession();
        }
        //+inf order , store and menuitem
        public void PlaceOrder(Store store, MenuItem menuItem)
        {
            Session currentSession = GetActiveSession();

            Order newOrder = new Order();
            newOrder.Store = store;
            newOrder.RecipientName = user.Name;
            newOrder.Price = menuItem.Price;
            newOrder.Details = menuItem.Details;

            currentSession.AddOrder(newOrder);
            //dataContext.GetSessionsRepository.Update(currentSession);
            //1.get current session
            //2.session.addOrder
            //3.save session, sessionRepo.Update(currentSession);
        }
        //order info to change
        public void ChangeOrder(Store store, MenuItem menuItem)
        {
            Order newOrder = new Order();
            newOrder.Store = store;
            newOrder.RecipientName = user.Name;
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
