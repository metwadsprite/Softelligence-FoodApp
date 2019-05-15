
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Abstractions;
using BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using EF.DataAccess.DataModel;

namespace EF.DataAccess
{
    public class UserRepositoryEF : IUsersRepository
    {

        private readonly ApplicationDbContext dbContext;
        public UserRepositoryEF(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User GetById(int id)
        {
            //var user = await dbContext.Users.FirstOrDefaultAsync();
            throw new System.NotImplementedException();
        }

        public string GetEmail()
        {   

            throw new System.NotImplementedException();
        }

        public ICollection<Order> GetHistory()
        {
            throw new System.NotImplementedException();
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }

        public void PlaceOrder()
        {
            throw new System.NotImplementedException();
        }

        public void CancelOrder()
        {
            throw new System.NotImplementedException();
        }

        public void ChangeOrder()
        {
            throw new System.NotImplementedException();
        }

        public void ViewActiveSession()
        {
            throw new System.NotImplementedException();
        }

        public void ViewOrder()
        {
            throw new System.NotImplementedException();
        }
    }
}
