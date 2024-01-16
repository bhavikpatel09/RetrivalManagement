using RetrivalManagement.Models.Models;
using RxWeb.Core.Data.Models;

namespace RetrivalManagement.GraphQL.Bootstrap
{
    public static class ConfigurationOptions
    {
        public static void AddConfigurationOptions(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            #region ConfigurationOptions
            serviceCollection.Configure<DatabaseConfig>(configuration.GetSection("Database"));
            serviceCollection.Configure<SecurityConfig>(configuration.GetSection("Security"));
            #endregion ConfigurationOptions
        }
    }
}
