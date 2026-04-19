using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.DAL.Repository.Implementation
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly GreenyDbContext _context;

        public CategoryRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Category category)
        {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return true;
        }
         
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
                return await _context.Categories.Where(c=>!c.IsDeleted).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
                return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id&& !c.IsDeleted);
        }

        public async Task<bool> UpdateAsync(Category newCategory)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(c => c.Id == newCategory.Id);

            if (result == null) {return false;}

                result.Name = newCategory.Name;
                result.Description = newCategory.Description;

            if (!string.IsNullOrEmpty(newCategory.Icon))
            {
                result.Icon = newCategory.Icon;
            }

            await _context.SaveChangesAsync();
                return true;
        }
         
        public async Task<bool> DeleteAsync(int id)
        {
                var result = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
                if (result == null) { return false; }

                result.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<IEnumerable<Category>> SearchByNameAsync(string name)
        {
            return await _context.Categories
              .Where(c => EF.Functions.Like(c.Name, $"%{name}%") && !c.IsDeleted).ToListAsync();
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id&& !c.IsDeleted);
        }
    }
}
