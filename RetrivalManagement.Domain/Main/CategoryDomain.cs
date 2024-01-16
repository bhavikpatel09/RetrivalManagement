using RetrivalManagement.Models.DbEntities.Main;
using RetrivalManagement.Models.Enums;
using RetrivalManagement.Models;
using RxWeb.Core.Data;
using RetrivalManagement.UnitOfWork.Main;

namespace RetrivalManagement.Domain.Main
{
    public class CategoryDomain : ICategoryDomain
    {
        public CategoryDomain(IMainUow mainUow, IDbContextManager<RetrivalManagementDbContext> dbContextManager)
        {
            MainUow = mainUow;
            ValidationMessages = new HashSet<string>();
            DbContextManager = dbContextManager;
        }
        public List<Category> GetAll()
        {
            return MainUow.Repository<Category>().All().ToList();
        }

        public Category Get(int id) => MainUow.Repository<Category>().SingleOrDefault(t => t.CategoryId == id);

        public HashSet<string> AddValidation(Category category)
        {
            CommonValidation(category);
            if (string.IsNullOrEmpty(category.Name))
            {
                ValidationMessages.Add("Please enter Name");
            }
            return ValidationMessages;
        }

        public Category Add(Category category)
        {
            MainUow.RegisterNewAsync<Category>(category);
            MainUow.CommitAsync();

            return category;
        }

        public HashSet<string> UpdateValidation(Category category)
        {
            return ValidationMessages;
        }

        public Category Update(Category category)
        {
            var categoryObject = MainUow.Repository<Category>().SingleOrDefault(t => t.CategoryId == category.CategoryId && t.StatusId != Status.Deleted);

            if (categoryObject != null)
            {
                categoryObject.Name = category.Name;
                MainUow.RegisterDirtyAsync<Category>(categoryObject);
                MainUow.CommitAsync();
                return categoryObject;
            }
            return category;
        }

        public HashSet<string> DeleteValidation(int id)
        {

            return ValidationMessages;
        }

        public void Delete(int id)
        {
            var user = MainUow.Repository<Category>().FindByKey(id);
            if (user != null)
            {
                user.StatusId = Status.Deleted;
                MainUow.RegisterDirtyAsync<Category>(user);
                MainUow.CommitAsync();
            }

        }

        private void CommonValidation(Category user)
        {
        }
        private IMainUow MainUow { get; set; }

        private HashSet<string> ValidationMessages { get; set; }

        private IDbContextManager<RetrivalManagementDbContext> DbContextManager { get; set; }

    }
    public interface ICategoryDomain
    {
        List<Category> GetAll();
        //string Get(string orderByColumn, string sortOrder, int pageIndex, int rowCount, string searchQuery, int categoryId);
        Category Get(int id);

        HashSet<string> AddValidation(Category category);
        HashSet<string> UpdateValidation(Category category);

        Category Add(Category category);
        Category Update(Category category);
        HashSet<string> DeleteValidation(int id);
        void Delete(int id);

    }
}
