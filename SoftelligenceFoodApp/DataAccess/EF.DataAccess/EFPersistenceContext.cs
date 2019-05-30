using BusinessLogic.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EF.DataAccess
{
    public class EFPersistenceContext : IPersistenceContext
    {
        private EntitiesMapper mapper = null;
        private IStoresRepository storesRepository;
        private ISessionsRepository sessionsRepository;
        private IUsersRepository usersRepository;

        public EFPersistenceContext(EntitiesMapper mapper)
        {
            this.mapper = mapper;
        }

        public ISessionsRepository GetSessionsRepository()
        {

            return sessionsRepository;
        }

        public IStoresRepository GetStoresRepository()
        {

            return storesRepository;
        }

        public IUsersRepository GetUsersRepository()
        {
            return usersRepository;
        }

        public void Initialize(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));
            var dbContext = services.BuildServiceProvider().GetService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
            storesRepository = new StoreRepositoryEF(dbContext, mapper);
            sessionsRepository = new SessionsRepositoryEF(dbContext, mapper);
            usersRepository = new UserRepositoryEF(dbContext, mapper);

        }
    }
}
