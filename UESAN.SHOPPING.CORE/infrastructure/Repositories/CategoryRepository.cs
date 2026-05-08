using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UESAN.SHOPPING.CORE.core.Entities;

namespace UESAN.SHOPPING.CORE.infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly logisticaBDContext _context;

        public CategoryRepository(logisticaBDContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        //UpdateCategory: Este método actualiza una categoría existente en la base de datos. Primero, busca la categoría por su ID. Si la categoría existe, actualiza su descripción y guarda los cambios en la base de datos.
        public async Task UpdateCategory(Category category)
        {
            var existingCategory = await _context.Categories.Where(c => c.Id == category.Id).FirstOrDefaultAsync();
            if (existingCategory != null)
            {

                existingCategory.Description = category.Description;
                await _context.SaveChangesAsync();
            }
        }


        //DeleteCategory: Este método marca una categoría como inactiva en lugar de eliminarla físicamente de la base de datos. Busca la categoría por su ID y, si la encuentra, establece su propiedad IsActive en false y guarda los cambios.

        public async Task DeleteCategory(int id)
        {
            {
                var existingcategory = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (existingcategory != null)
                {
                    existingcategory.IsActive = false;
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
