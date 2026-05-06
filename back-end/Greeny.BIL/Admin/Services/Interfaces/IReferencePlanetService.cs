
using Greeny.BLL.Admin.ModelVM.ReferencePlanet;

namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface IReferencePlanetService
    {
        Task<Response<IEnumerable<DetailsRefPlanetVM>>> GetAll();

        Task<Response<DetailsRefPlanetVM>> GetByIdAsync(int id);

        Task<Response<bool>> CreateAsync(CreateRefPlanetVM vm);

        Task<Response<bool>> UpdateAsync(UpdateRefPlanetVM vm);

        Task<Response<bool>> DeleteAsync(int id);

        Task<Response<DetailsRefPlanetVM>> GetBySciNameAsync(string sciName);

        Task<Response<DetailsRefPlanetVM>> GetByCommonNameAsync(string commonName);

        Task<Response<bool>> ExistsByIdAsync(int id);
    }
}
