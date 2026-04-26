

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IReferencePlanetRepo
    {
        IQueryable<ReferencePlanet> GetAll();

        Task<ReferencePlanet> GetByIdAsync(int id);

        Task CreateAsync(ReferencePlanet referencePlanet);

        Task UpdateAsync(ReferencePlanet referencePlanet);

        Task DeleteAsync(int id);

        Task<ReferencePlanet> GetBySciNameAsync(string sciName);

        Task<ReferencePlanet> GetByCommonNameAsync(string commonName);

        Task<bool> ExistsByIdAsync(int id);



    }
}
