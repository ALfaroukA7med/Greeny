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

        public async Task<bool> CreateAsync(Product product)
        {
              await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
                return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
                var result =  await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                return result;
        }

        public async Task<bool> UpdateAsync(Product newProduct)
        {
                var result =await _context.Products.FirstOrDefaultAsync(p => p.Id == newProduct.Id);

            if (result == null)
            { return false; }

                result.Name = newProduct.Name;
                result.Price = newProduct.Price;
                result.Description = newProduct.Description;
                result.Quantity = newProduct.Quantity;
                result.IsDeleted = newProduct.IsDeleted;


            if (!string.IsNullOrEmpty(newProduct.Image))
            {
                result.Image = newProduct.Image;
            }


            await _context.SaveChangesAsync();
                return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
                var result = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (result == null) { return false; }

                _context.Products.Remove(result);
                await _context.SaveChangesAsync();
                return true;
        }

    }
}
