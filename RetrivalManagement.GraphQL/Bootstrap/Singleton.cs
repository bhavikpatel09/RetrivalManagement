using RetrivalManagement.Infrastructure.Singleton;
using RxWeb.Core.Data;

namespace RetrivalManagement.GraphQL.Bootstrap
{
    public static class Singleton
    {
        public static void AddSingletonService(this IServiceCollection serviceCollection)
        {
            #region Singleton
            serviceCollection.AddSingleton(typeof(UserAccessConfigInfo));
            #endregion Singleton
        }

    }
}
