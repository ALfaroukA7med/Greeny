using Greeny.BLL.Abstraction;
using Greeny.BLL.Errors;
using Greeny.BLL.Helper;
using Greeny.BLL.ModelVM.ReferencePlanet;
using Greeny.BLL.Services.Interfaces;


namespace Greeny.BLL.Services.Implementation
{
    public class ReferencePlanetService : IReferencePlanetService
    {
        private readonly IReferencePlanetRepo _referencePlanetRepo;
        private readonly IMapper _mapper;

        public ReferencePlanetService(IReferencePlanetRepo referencePlanetRepo, IMapper mapper)
        {
            _referencePlanetRepo = referencePlanetRepo;
            _mapper = mapper;
        }

        public async Task<Response<bool>> CreateAsync(CreateRefPlanetVM vm)
        {
            if (vm == null)
                return Response<bool>.Fail(RefPlanetError.InvalidData);

            if (vm.UploadImage != null)
            {
                vm.Image = Upload.UploadFile("Files", vm.UploadImage);
            }

            var entity = _mapper.Map<ReferencePlanet>(vm);

            await _referencePlanetRepo.CreateAsync(entity);

            return Response<bool>.Success(true);
        }



        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var refPlanet = await _referencePlanetRepo.GetByIdAsync(id); 

            if (refPlanet == null)
            {
                return Response<bool>.Fail(RefPlanetError.NotFound);
            }
            await _referencePlanetRepo.DeleteAsync(id);

            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> ExistsByIdAsync(int id)
        {
           var Exists = await _referencePlanetRepo.ExistsByIdAsync(id);
            return Response<bool>.Success(Exists);
        }

        public async Task<Response<IEnumerable<DetailsRefPlanetVM>>> GetAll()
        {
            var Query = _referencePlanetRepo.GetAll();
            var RefPlanets = await Query.ToListAsync();

            if (RefPlanets == null || !RefPlanets.Any())
            {
                return Response<IEnumerable<DetailsRefPlanetVM>>.Fail(RefPlanetError.NotFound);
            }

            var data = _mapper.Map<IEnumerable<DetailsRefPlanetVM>>(RefPlanets);

            return Response<IEnumerable<DetailsRefPlanetVM>>.Success(data);
        }

        public async Task<Response<DetailsRefPlanetVM>> GetByCommonNameAsync(string commonName)
        {
            var result = await _referencePlanetRepo.GetByCommonNameAsync(commonName);
            if(result == null || result.IsDeleted)
            {
                return Response<DetailsRefPlanetVM>.Fail(RefPlanetError.NotFound);
            }

            var data = _mapper.Map<DetailsRefPlanetVM>(result);
            return Response<DetailsRefPlanetVM>.Success(data);
        }

        public async Task<Response<DetailsRefPlanetVM>> GetByIdAsync(int id)
        {
            var result = await _referencePlanetRepo.GetByIdAsync(id);
            if (result == null || result.IsDeleted)
            {
                return Response<DetailsRefPlanetVM>.Fail(RefPlanetError.NotFound);
            }

            var data = _mapper.Map<DetailsRefPlanetVM>(result);
            return Response<DetailsRefPlanetVM>.Success(data);
        }

        public async Task<Response<DetailsRefPlanetVM>> GetBySciNameAsync(string sciName)
        {
            var result = await _referencePlanetRepo.GetBySciNameAsync(sciName);
            if (result == null || result.IsDeleted)
            {
                return Response<DetailsRefPlanetVM>.Fail(RefPlanetError.NotFound);
            }

            var data = _mapper.Map<DetailsRefPlanetVM>(result);
            return Response<DetailsRefPlanetVM>.Success(data);
        }

        public async Task<Response<bool>> UpdateAsync(UpdateRefPlanetVM vm)
        {
            var refPlanet = await _referencePlanetRepo.GetByIdAsync(vm.Id);

            if (refPlanet == null || refPlanet.IsDeleted)
                return Response<bool>.Fail(RefPlanetError.NotFound);

            if (vm.UploadImage != null)
            {
                vm.Image = Upload.UploadFile("Files", vm.UploadImage);
            }

            _mapper.Map(vm, refPlanet);

            await _referencePlanetRepo.UpdateAsync(refPlanet);

            return Response<bool>.Success(true);
        }

    }
}
