

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IReferencePlanetRepo
    {
        Task<IEnumerable<ReferencePlanet>> GetAllAsync();

        Task<ReferencePlanet> GetByIdAsync(string id);

        Task<bool> CreateAsync(ReferencePlanet referencePlanet);

        Task<bool> UpdateAsync(ReferencePlanet referencePlanet);

        Task<bool> DeleteAsync(string id);
    }
}
