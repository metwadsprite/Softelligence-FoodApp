using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Abstractions
{
    public interface IPersistenceContext
    {
        void Initialize(IServiceCollection services, string connectionString);
        ISessionsRepository GetSessionsRepository();
        IUsersRepository GetUsersRepository();
        IStoresRepository GetStoresRepository();
    }
}
