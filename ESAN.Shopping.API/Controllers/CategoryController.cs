using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UESAN.SHOPPING.CORE.infrastructure.Repositories;
using UESAN.SHOPPING.CORE.core.Entities;

namespace ESAN.Shopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (category == null) return BadRequest();

            await _categoryRepository.InsertCategory(category);

            // Devuelve 201 con la ubicación del recurso creado
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (category == null) return BadRequest();
            if (id != category.Id) return BadRequest("El id de la ruta no coincide con el id del body.");

            var existing = await _categoryRepository.GetCategoryById(id);
            if (existing == null) return NotFound();

            await _categoryRepository.UpdateCategory(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existing = await _categoryRepository.GetCategoryById(id);
            if (existing == null) return NotFound();

            await _categoryRepository.DeleteCategory(id);
            return NoContent();
        }
    }
}
