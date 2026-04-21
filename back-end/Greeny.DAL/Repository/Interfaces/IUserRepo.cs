

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetByIdAsync(string id);

        Task<bool> CreateAsync(User user);

        Task<bool> UpdateAsync(User user);

        Task<bool> DeleteAsync(string id);

        Task<IEnumerable<User>> GetAllActiveAsync();
        Task<IEnumerable<User>> GetAllDeletedAsync();

    }
}
