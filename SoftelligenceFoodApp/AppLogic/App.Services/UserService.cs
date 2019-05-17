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
        public void PlaceOrder(Store store, int menuItemId)
        {
            Session currentSession = GetActiveSession();
            Session.addORder
            //1.get current session
            //2.session.addOrder
            //3.save session, sessionRepo.Update(currentSession);
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
