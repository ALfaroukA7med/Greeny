using Greeny.BLL.ModelVM.ProductVM;
using Greeny.BLL.ModelVM.ReferencePlanet;
using Greeny.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.PL.Controllers
{

    public class ReferencePlanetController : Controller
    {
        private readonly IReferencePlanetService _referencePlanetService;

        public ReferencePlanetController(IReferencePlanetService referencePlanetService)
        {
            _referencePlanetService = referencePlanetService;
        }


        // Get All 
        public async Task<IActionResult> Index()
        {
            var result = await _referencePlanetService.GetAll();

            if (!result.IsSuccess)
                return View("Index", Enumerable.Empty<DetailsProductVM>());

            return View("Index", result.Data);
        }

        // Details 
        public async Task<IActionResult> Details(int id)
        {
            var result = await _referencePlanetService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            return View(result.Data);
        }

        // Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRefPlanetVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _referencePlanetService.CreateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed to create Ref Planet");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        // Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _referencePlanetService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            var vm = new UpdateRefPlanetVM
            {
                Id = result.Data.Id,
                SciName = result.Data.SciName,
                SolidType = result.Data.SolidType,
                TempReq = result.Data.TempReq,
                CommonName = result.Data.CommonName,
                SunlightReq = result.Data.SunlightReq,
                GrowthSeason = result.Data.GrowthSeason,
                Image = result.Data.Image,
                Description = result.Data.Description,
                PlanetType = result.Data.PlanetType,
                Family = result.Data.Family,
                WaterReq = result.Data.WaterReq,
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateRefPlanetVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _referencePlanetService.UpdateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed To Edit");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        // Delete 
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _referencePlanetService.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> GetBySciNameAsync(string name)
        {
            var result = await _referencePlanetService.GetBySciNameAsync(name);

            if (!result.IsSuccess)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> GetByCommonNameAsync(string name)
        {
            var result = await _referencePlanetService.GetByCommonNameAsync(name);

            if (!result.IsSuccess)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}