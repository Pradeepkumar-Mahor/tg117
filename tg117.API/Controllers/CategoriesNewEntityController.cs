using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tg117.Domain;
using tg117.Domain.DbContext;
using tg117.Domain.Repos.Interface;
using tg117.Domain.Repos.Repository;
using static tg117.API.Classes.GenericClass;

namespace tg117.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesNewEntityController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesNewEntityController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        #region With Generic Entity Repo

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Category> employees = _categoryRepository.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(Guid id)
        {
            Category category = _categoryRepository.FindOne(x => x.Guid == id);
            if (category == null)
            {
                return NotFound("The Category record couldn't be found.");
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Category is null.");
            }

            category.Guid = Guid.NewGuid();
            _categoryRepository.Add(category);
            return CreatedAtRoute(
                  "Get",
                  new { Id = category.Guid }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Category is null.");
            }
            Category categoryToUpdate = _categoryRepository.FindOne(x => category.Guid == id);
            if (categoryToUpdate == null)
            {
                return NotFound("The Category record couldn't be found.");
            }

            categoryToUpdate.DisplayOrder = category.DisplayOrder;
            categoryToUpdate.Name = category.Name;

            _categoryRepository.Update(categoryToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Category category = _categoryRepository.FindOne(x => x.Guid == id);
            if (category == null)
            {
                return NotFound("The category record couldn't be found.");
            }
            _categoryRepository.Delete(category);
            return NoContent();
        }

        #endregion With Generic Entity Repo
    }
}