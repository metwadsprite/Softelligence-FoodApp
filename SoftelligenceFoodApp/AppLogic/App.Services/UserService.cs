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

        public void GetActiveSession() { }
        //+inf order
        public void PlaceOrder() { }
        //order info to change
        public void ChangeOrder() { }
        public void CancelOrder() { }
        public Order GetCurrentOrder()
        {
            user.
            return null;
        }
    }
}
