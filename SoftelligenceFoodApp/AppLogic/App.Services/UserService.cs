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

        public Session GetActiveSession()
        {
            return dataContext.GetSessionsRepository().GetActiveSession();
        }
        //+inf order
        public void PlaceOrder(Order newOrder)
        {
            user.PlaceOrder(newOrder);
        }
        //order info to change
        public void ChangeOrder(Order newOrder)
        {
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
