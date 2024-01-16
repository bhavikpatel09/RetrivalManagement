using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RetrivalManagement.Domain.Main;
using RetrivalManagement.Models.DbEntities.Main;

namespace RetrivalManagement.GraphQL.Mutations
{
    public partial class Mutation
    {
        public Category? CreateCategory(ICategoryDomain categoryDomain, Category category)
        {
            HashSet<string> validations = categoryDomain.AddValidation(category);
            if (validations.Count == 0)
            {
                Category result = categoryDomain.Add(category);
                return result;
            }
            return null;
        }

        public Category? UpdateCategory(ICategoryDomain categoryDomain, Category category)
        {
            HashSet<string> validations = categoryDomain.UpdateValidation(category);
            if (validations.Count == 0)
            {
                Category result = categoryDomain.Update(category);
                return result;
            }
            return null;
        }

        public bool DeleteCategory(ICategoryDomain categoryDomain, int id)
        {
            HashSet<string> validations = categoryDomain.DeleteValidation(id);
            if (validations.Count == 0)
            {
                categoryDomain.Delete(id);
                return true;
            }
            return false;
        }
    }
}
