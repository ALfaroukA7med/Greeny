


namespace Greeny.DAL.Repository.Implementation
{
    public class UserRepo : IUserRepo
    {
        private readonly GreenyDbContext _context;

        public UserRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        //public IQueryable<User> GetAll()
        //{
        //    return _context.Users.Where(u=> !u.IsDeleted).AsNoTracking();
        //}

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task UpdateAsync(User newUser)
        {
             await _context.Users.Where(u => u.Id == newUser.Id && !u.IsDeleted).ExecuteUpdateAsync(setter => setter
            .SetProperty(u => u.FirstName , newUser.FirstName)
            .SetProperty(u => u.LastName , newUser.LastName)
            .SetProperty(u => u.Address , newUser.Address)
            .SetProperty(u => u.ProfilePicture , newUser.ProfilePicture)
            );
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users.AsNoTracking();
        }
    }
}
