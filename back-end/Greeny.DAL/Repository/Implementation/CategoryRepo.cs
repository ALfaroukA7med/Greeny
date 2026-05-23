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

        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories
                .Where(c => !c.IsDeleted)
            .AsNoTracking();
        }

        public IQueryable<Category> GetById(int id)
        {
            return _context.Categories
            .Where(c => c.Id == id && !c.IsDeleted)
            .AsNoTracking();
        }

        public async Task UpdateAsync(Category newCategory)
        {
            await _context.Categories
                .Where(c => c.Id == newCategory.Id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(p => p.Icon, newCategory.Icon)
                    .SetProperty(p => p.Name, newCategory.Name)
                    .SetProperty(p => p.Description, newCategory.Description)
                );
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Categories
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(p => p.IsDeleted, true)
                    
                );
        }

        public IQueryable<Category> SearchByName(string name)
        {
            return _context.Categories
              .Where(c => EF.Functions.Like(c.Name, $"%{name}%"))
              .AsNoTracking();
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.Categories
                .Where(c => c.Id == id && !c.IsDeleted)
                .AnyAsync();
        }

        public async Task<int> GetProductsCountByCategoryId(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
                .CountAsync();
        }
        public async Task<string> GetCategoryIconById(int categoryId)
        {
            return await _context.Categories
                .Where(c => c.Id == categoryId && !c.IsDeleted) 
                .Select(c => c.Icon)
                .FirstOrDefaultAsync();
        }
    }
}
