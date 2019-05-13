using Microsoft.Extensions.DependencyInjection;

namespace Abstractions
{
    public interface IPersistenceContext
    {
        void Initialize(IServiceCollection services, string connectionString);
    }
}
