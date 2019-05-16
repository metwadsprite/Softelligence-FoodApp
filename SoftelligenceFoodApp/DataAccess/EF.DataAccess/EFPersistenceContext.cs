using BusinessLogic.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EF.DataAccess
{
    public class EFPersistenceContext : IPersistenceContext
    {
        public ISessionsRepository GetSessionsRepository()
        {   

            throw new System.NotImplementedException();
        }

        public IStoresRepository GetStoresRepository()
        {   

            throw new System.NotImplementedException();
        }

        public IUsersRepository GetUsersRepository()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));
        }
    }
}
