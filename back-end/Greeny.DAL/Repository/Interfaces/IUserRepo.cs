

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IUserRepo
    {
        IQueryable<User> GetAll();

        Task<User> GetByIdAsync(string id);

        Task CreateAsync(User user);

        Task UpdateAsync(User user);

        Task DeleteAsync(string id);

        IQueryable<User> GetAllActive();
        IQueryable<User> GetAllDeleted();

    }
}
