using RealState.Common.Enumerations;
using RealState.Data.Contexts;

namespace RealState.Data.Initializers
{
    public static class SqlServerInitializer
    {
        public static void Initialize(SqlServerContext context)
        {
            context._sqlConnectionString = ConfigurationEnum.SqlServerConnectionString;
            context.Database.EnsureCreated();
        }
    }
}
