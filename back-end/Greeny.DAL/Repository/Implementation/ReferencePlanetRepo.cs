using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.DAL.Repository.Implementation
{
    public class ReferencePlanetRepo : IReferencePlanetRepo
    {

        private readonly GreenyDbContext _context;

        public ReferencePlanetRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ReferencePlanet referencePlanet)
        {
            await _context.ReferencePlanets.AddAsync(referencePlanet);
            await _context.SaveChangesAsync();
        }

        public IQueryable<ReferencePlanet> GetAll()
        {
            return _context.ReferencePlanets.Where(rf => !rf.IsDeleted).AsNoTracking();
        }

        public async Task<ReferencePlanet?> GetByIdAsync(int id)
        {
            return await _context.ReferencePlanets.FirstOrDefaultAsync(rf => rf.Id == id && !rf.IsDeleted);
        }

        public async Task UpdateAsync(ReferencePlanet newReferencePlanet)
        {

            await _context.ReferencePlanets.Where(r => r.Id == newReferencePlanet.Id && !r.IsDeleted)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(r => r.TempReq, newReferencePlanet.TempReq)
                .SetProperty(r => r.CommonName, newReferencePlanet.CommonName)
                .SetProperty(r => r.SciName, newReferencePlanet.SciName)
                .SetProperty(r => r.WaterReq, newReferencePlanet.WaterReq)
                .SetProperty(r => r.SunlightReq, newReferencePlanet.SunlightReq)
                .SetProperty(r => r.Description, newReferencePlanet.Description)
                .SetProperty(r => r.ShortDescription, newReferencePlanet.ShortDescription)
                .SetProperty(r => r.SolidType, newReferencePlanet.SolidType)
                .SetProperty(r => r.GrowthSeason, newReferencePlanet.GrowthSeason)
                .SetProperty(r => r.Family, newReferencePlanet.Family)
                .SetProperty(r => r.Image, newReferencePlanet.Image)
                .SetProperty(r => r.PlanetType, newReferencePlanet.PlanetType));
        }

        public async Task DeleteAsync(int id)
        {
            await _context.ReferencePlanets.Where(r => r.Id == id && !r.IsDeleted)
               .ExecuteUpdateAsync(setter => setter
               .SetProperty(r => r.IsDeleted, true));
        }

        public async Task<ReferencePlanet?> GetBySciNameAsync(string sciName)
        {
            return await _context.ReferencePlanets.FirstOrDefaultAsync(rf => EF.Functions.Like(rf.SciName, $"%{sciName}%") && !rf.IsDeleted);
        }

        public async Task<ReferencePlanet?> GetByCommonNameAsync(string commonName)
        {
            return await _context.ReferencePlanets.FirstOrDefaultAsync(rf => EF.Functions.Like(rf.CommonName, $"%{commonName}%") && !rf.IsDeleted);
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.ReferencePlanets
           .AnyAsync(rf => rf.Id == id && !rf.IsDeleted);
        }
    }
}