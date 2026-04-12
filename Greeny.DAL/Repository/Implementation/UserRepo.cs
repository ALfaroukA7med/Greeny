
using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.DAL.Repository.Implementation
{
    public class UserRepo : IUserRepo
    {
        private readonly GreenyDbContext _context;

        public UserRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return result;
        }

        public async Task<bool> UpdateAsync(User newUser)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == newUser.Id);
            if (result == null)
            {
                return false;
            }
            result.FirstName = newUser.FirstName;
            result.LastName = newUser.LastName;
            result.Address = newUser.Address;
            result.IsDeleted = newUser.IsDeleted;

            if (!string.IsNullOrEmpty(newUser.ProfilePicture))
            {
                result.ProfilePicture = newUser.ProfilePicture;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (result == null) { return false; }
            _context.Users.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
