using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.ReferencePlanet;

namespace Greeny.BLL.Services.Interfaces
{
    public interface IReferencePlanetService
    {
        Task<Response<IEnumerable<DetailsRefPlanetVM>>> GetAll();
        Task<Response<IEnumerable<DetailsForDashboard>>> GetAllForDashboard();
        Task<Response<DetailsRefPlanetVM>> GetByIdAsync(int id);

        Task<Response<bool>> CreateAsync(CreateRefPlanetVM vm);

        Task<Response<bool>> UpdateAsync(UpdateRefPlanetVM vm);

        Task<Response<bool>> DeleteAsync(int id);

        Task<Response<DetailsRefPlanetVM>> GetBySciNameAsync(string sciName);

        Task<Response<DetailsRefPlanetVM>> GetByCommonNameAsync(string commonName);

        Task<Response<bool>> ExistsByIdAsync(int id);
    }
}
