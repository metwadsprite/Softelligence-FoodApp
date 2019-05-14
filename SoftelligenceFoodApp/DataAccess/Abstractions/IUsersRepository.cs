using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions
{
    public interface IUsersRepository
    {
        User GetById();
        String GetEmail();
        String GetName();
        ICollection<Order> GetHistory();
        void ViewActiveSession();
        void PlaceOrder();
        void ChangeOrder();
        void CancelOrder();
        void ViewOrder();

    }
}
