using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;
using System.Security.AccessControl;

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
            return await _context.ReferencePlanets.ToListAsync();
        }

        public async Task<ReferencePlanet?> GetByIdAsync(int id)
        {
            var result = await _context.ReferencePlanets.FirstOrDefaultAsync(rf => rf.Id == id);
            return result;
        }

        public async Task<bool> UpdateAsync(ReferencePlanet newReferencePlanet)
        {
            var result = await _context.ReferencePlanets.FirstOrDefaultAsync(rf => rf.Id == newReferencePlanet.Id);
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
            var result = await _context.ReferencePlanets.FirstOrDefaultAsync(rf => rf.Id == id);
            if (result == null) { return false; }
            _context.ReferencePlanets.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReferencePlanet?> GetBySciNameAsync(string sciName)
        {
            var result = await _context.ReferencePlanets.FirstOrDefaultAsync(rf => rf.SciName == sciName);
            return result;
        }

        public async Task<ReferencePlanet?> GetByCommonNameAsync(string commonName)
        {
            var result = await _context.ReferencePlanets.FirstOrDefaultAsync(rf => rf.CommonName == commonName);
            return result;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            var result = await _context.ReferencePlanets.FirstOrDefaultAsync(rf => rf.Id == id);
            if(result == null) { return false; }
            return true;
        }
    }
}
