using HotChocolate.Types.Pagination;
using NodaTime;
using RetrivalManagement.Domain.Main;
using RetrivalManagement.GraphQL.Mutations;
using RetrivalManagement.GraphQL.Queries;
using RetrivalManagement.Models;

namespace RetrivalManagement.GraphQL.Bootstrap
{
    public static class AddGraphQL
    {
        public static void AddGraphQLService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddGraphQLServer()
                .InitializeOnStartup()

                .RegisterService<ICategoryDomain>()

                .SetPagingOptions(new PagingOptions()
                {
                    DefaultPageSize = 25,
                    MaxPageSize = 1000,
                    IncludeTotalCount = true
                })
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>();
        }
    }
}
