using Microsoft.AspNetCore.Mvc;
using RetrivalManagement.Domain.Main;
using RetrivalManagement.Models.DbEntities.Main;
using RetrivalManagement.UnitOfWork.Main;

namespace RetrivalManagement.GraphQL.Controllers.Main
{
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private IMainUow MainUow { get; set; }
        private ICategoryDomain CategoryDomain { get; set; }
        public CategoriesController(IMainUow mainUow, ICategoryDomain categoryDomain)
        {
            MainUow = mainUow;
            CategoryDomain = categoryDomain;
        }

        public IActionResult GetAll()
        {
            var categories = CategoryDomain.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category category = CategoryDomain.Get(id);
            return Ok(category);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            HashSet<string> validations = CategoryDomain.AddValidation(category);
            if (validations.Count == 0)
            {
                Category result = CategoryDomain.Add(category);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category category)
        {
            HashSet<string> validations = CategoryDomain.UpdateValidation(category);
            if (validations.Count == 0)
            {
                Category result = CategoryDomain.Update(category);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Category category)
        {
            Category updateDetails = MainUow.Repository<Category>().FindByKey(id);
            if (updateDetails != null)
            {
                updateDetails.Name = category.Name;
                return Put(category.CategoryId, updateDetails);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            HashSet<string> validations = CategoryDomain.DeleteValidation(id);
            if (validations.Count == 0)
            {
                CategoryDomain.Delete(id);                
                return Ok();
            }
            return BadRequest();
        }
    }
}
