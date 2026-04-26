using Greeny.DAL.Repository.Interfaces;
using Greeny.DAL.Database;


namespace Greeny.DAL.Repository.Implementation
{
    public class ProductRepo : IProductRepo
    {
        private readonly GreenyDbContext _context;

        public ProductRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product product)
        {
              await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
        }

        public IQueryable<Product> GetAll()
        {
            return _context.Products
            .Where(p => !p.IsDeleted)
            .AsNoTracking();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id&& !p.IsDeleted);
        }

        public async Task UpdateAsync(Product newProduct)
        {
            await _context.Products.Where(p => p.Id == newProduct.Id && !p.IsDeleted)
               .ExecuteUpdateAsync(setter => setter
               .SetProperty(p=> p.Name, newProduct.Name)
               .SetProperty(p => p.Price, newProduct.Price)
               .SetProperty(p => p.Description, newProduct.Description)
               .SetProperty(p => p.Image, newProduct.Image)
               .SetProperty(p => p.Quantity, newProduct.Quantity));
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Products.Where(p => p.Id == id && !p.IsDeleted)
          .ExecuteUpdateAsync(setter => setter
          .SetProperty(p => p.IsDeleted, true));
        }


        public IQueryable<Product> SearchByName(string name)
        {
            return _context.Products
                .Where(p => EF.Functions.Like(p.Name, $"%{name}%") && !p.IsDeleted)
                .AsNoTracking();
        }

        public IQueryable<Product> GetInStock()
        {
            return _context.Products
                .Where(p => p.Quantity > 0 && !p.IsDeleted)
                .AsNoTracking();
        }

        public IQueryable<Product> GetOutStock()
        {
            return _context.Products
                .Where(p => p.Quantity == 0 && !p.IsDeleted)
                .AsNoTracking();
        }

        public IQueryable<Product> GetMostExpensive()
        {
            return _context.Products
                .OrderByDescending(p => p.Price)
                .Take(10)
                .AsNoTracking();
        }

        public IQueryable<Product> GetLestExpensive()
        {
            return _context.Products
                .OrderBy(p => p.Price)
                .Take(10)
                .AsNoTracking();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Products
                .AnyAsync(p => EF.Functions.Like(p.Name, $"%{name}%") && !p.IsDeleted);
        }

        public async Task<bool> ExistsByIdAsync(int Id)
        {
            return await _context.Products
                .AnyAsync(p => p.Id == Id && !p.IsDeleted);
        }

        public IQueryable<Product> GetAllByCategoryId(int categoryId)
        {
            return _context.Products.Where(p=>p.CategoryId== categoryId && !p.IsDeleted).AsNoTracking();
        }
    }
}
