using FormulaABD.Interfaces;
using FormulaABD.Models;
using FormulaABD.Repository;
using FormulaABD.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FormulaABD.Controllers
{
    public class PilotaController : Controller
    {
        private readonly IPilotaRepository _repository;

        public PilotaController(IPilotaRepository pilotaRepository)
        {
            _repository = pilotaRepository;
        }

        #region GET : Pilota
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var piloti = await _repository.GetAllAsync();
            return View(piloti);
        }
        #endregion

        #region GET : Pilota/Details/{guid}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var pilota = await _repository.GetByGuidAsync(id);
            if (pilota == null)
            {
                return NotFound();
            }
            return View(pilota);
        }
        #endregion

        #region POST : Pilota/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreatePilotaVM createPilotaVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createPilotaVM);
            }

            var pilota = new Pilota
            {
                Name = createPilotaVM.Name
            };

            await _repository.CreateAsync(pilota);

            return RedirectToAction("Index");
        }
        #endregion

        #region PUT : Pilota/Edit/{guid}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var pilotaInDb = await _repository.GetByGuidAsync(id);

            if (pilotaInDb == null) return View("Error");

            return View(pilotaInDb);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditPilotaVM editPilotaVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit Pilota");
                return View("Edit", editPilotaVM);
            }

            var pilotaInDb = await _repository.GetByGuidAsync(id);

            if (pilotaInDb == null) return View("Error");

            pilotaInDb.Name = editPilotaVM.Name;

            await _repository.UpdateAsync(pilotaInDb);

            return RedirectToAction("Index");
        }
        #endregion

        #region DELETE : Pilota/Delete/{guid}
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var pilota = await _repository.GetByGuidAsync(id);
            if (pilota == null) return View("Error");
            return View(pilota);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePilota(Guid id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
