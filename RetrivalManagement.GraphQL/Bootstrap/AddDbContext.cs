using RetrivalManagement.Models;

namespace RetrivalManagement.GraphQL.Bootstrap
{
    public static class AddDbContextExtension
    {
        public static void AddDbContextService(this IServiceCollection serviceCollection)
        {
            #region SqlDbContext
            serviceCollection.AddDbContext<RetrivalManagementDbContext>();
            #endregion SqlDbContext
        }
    }
}
