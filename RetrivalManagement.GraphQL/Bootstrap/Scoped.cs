using RetrivalManagement.Domain.Main;
using RetrivalManagement.Domain.UserManagement;
using RetrivalManagement.Infrastructure.Security;
using RetrivalManagement.Models;
using RetrivalManagement.UnitOfWork.Login;
using RetrivalManagement.UnitOfWork.Main;
using RetrivalManagement.UnitOfWork.UserManagement;
using RxWeb.Core.Annotations;
using RxWeb.Core.Data;
using RxWeb.Core.Security;

namespace RetrivalManagement.GraphQL.Bootstrap
{
    public static class ScopedExtension
    {

        public static void AddScopedService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRepositoryProvider, RepositoryProvider>();
            serviceCollection.AddScoped<ITokenAuthorizer, TokenAuthorizer>();
            serviceCollection.AddScoped<IModelValidation, ModelValidation>();
            serviceCollection.AddScoped<IApplicationTokenProvider, ApplicationTokenProvider>();
            serviceCollection.AddScoped(typeof(IDbContextManager<>), typeof(DbContextManager<>));

            #region ContextService

            serviceCollection.AddScoped<IRetrivalManagementDbContext, RetrivalManagementDbContext>();
            serviceCollection.AddScoped<ILoginUow, LoginUow>();
            serviceCollection.AddScoped<IUserUow, UserUow>();
            serviceCollection.AddScoped<IMainUow, MainUow>();
            #endregion ContextService



            #region DomainService
            serviceCollection.AddScoped<IUserDomain, UserDomain>();
            serviceCollection.AddScoped<ICategoryDomain, CategoryDomain>();
            #endregion DomainService
        }
    }
}
