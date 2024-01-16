using RetrivalManagement.Domain.Main;
using RetrivalManagement.Models.DbEntities.Main;

namespace RetrivalManagement.GraphQL.Queries
{
    public partial class Query
    {
        [UseOffsetPaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public List<Category> GetCategories(ICategoryDomain categoryDomain)
        {
            return categoryDomain.GetAll();
        }
    }
}
