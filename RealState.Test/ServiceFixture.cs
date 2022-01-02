using Microsoft.Extensions.DependencyInjection;
using RealState.Common.Interfaces;
using RealState.Common.Interfaces.Data;
using RealState.Common.Interfaces.Services;
using RealState.Common.Models;
using RealState.Data.Contexts;
using RealState.Data.Repositories;
using RealState.Services;

namespace RealState.Test
{
    public class ServiceFixture
    {
        public ServiceFixture()
        {
            ServiceCollection serviceCollection = new();
            serviceCollection.AddDbContext<SqlServerContext>();
            serviceCollection.AddTransient<IResponse, Response>();
            serviceCollection.AddTransient(typeof(IGenericSqlServerRepository<>), typeof(GenericSqlServerRepository<>));
            serviceCollection.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}
