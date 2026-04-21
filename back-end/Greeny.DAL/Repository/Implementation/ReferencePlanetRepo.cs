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

        public async Task<bool> CreateAsync(ReferencePlanet referencePlanet)
        {
            await _context.ReferencePlanets.AddAsync(referencePlanet);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ReferencePlanet>> GetAllAsync()
        {
            return await _context.ReferencePlanets.Where(rf=> !rf.IsDeleted).ToListAsync();
        }

        public async Task<ReferencePlanet?> GetByIdAsync(int id)
        {
            return await _context.ReferencePlanets.FirstOrDefaultAsync(rf => rf.Id == id && !rf.IsDeleted);
        }

        public async Task<bool> UpdateAsync(ReferencePlanet newReferencePlanet)
        {
            var result = await _context.ReferencePlanets.FirstOrDefaultAsync(rf => rf.Id == newReferencePlanet.Id && !rf.IsDeleted);
            if (result == null)
            {
                return false;
            }
            result.TempReq = newReferencePlanet.TempReq;
            result.CommonName = newReferencePlanet.CommonName;
            result.SciName = newReferencePlanet.SciName;
            result.WaterReq = newReferencePlanet.WaterReq;
            result.SunlightReq = newReferencePlanet.SunlightReq;
            result.Description = newReferencePlanet.Description;
            result.SolidType = newReferencePlanet.SolidType;
            result.GrowthSeason = newReferencePlanet.GrowthSeason;
            result.Family = newReferencePlanet.Family;
            result.PlanetType = newReferencePlanet.PlanetType;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.ReferencePlanets.FirstOrDefaultAsync(rf => rf.Id == id && !rf.IsDeleted);
            if (result == null) { return false; }

            result.IsDeleted= true;
            await _context.SaveChangesAsync();
            return true;
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
