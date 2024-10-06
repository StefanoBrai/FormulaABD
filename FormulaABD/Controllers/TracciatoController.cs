using FormulaABD.Interfaces;
using FormulaABD.Models;
using FormulaABD.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FormulaABD.Controllers
{
    public class TracciatoController : Controller
    {
        private readonly ITracciatoRepository _repository;

        public TracciatoController(ITracciatoRepository repository)
        {
            _repository = repository;
        }

        #region GET : Tracciato
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tracciati = await _repository.GetAllAsync();
            return View(tracciati);
        }
        #endregion

        #region GET : Tracciato/{guid}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var tracciato = await _repository.GetByGuidAsync(id);
            if (tracciato == null) 
            {
                return NotFound();
            }
            return View(tracciato);
        }
        #endregion

        #region POST : Tracciato/Create/{guid}
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTracciatoVM createTracciatoVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createTracciatoVM);
            }

            var tracciato = new Tracciato
            {
                Name = createTracciatoVM.Name
            };

            await _repository.CreateAsync(tracciato);
            return RedirectToAction("Index");
        }
        #endregion

        #region PUT : Tracciato/Edit/{guid}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tracciato = await _repository.GetByGuidAsync(id);

            if (tracciato == null) return View("Error");

            return View(tracciato);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditPilotaVM editPilotaVM)
        {
            if (!ModelState.IsValid) 
            {
                ModelState.AddModelError("", "Failed to edit Tracciato");
                return View("Edit", editPilotaVM);
            }

            var tracciatoInDb = await _repository.GetByGuidAsync(id);
            
            if (tracciatoInDb == null) return View("Error");

            tracciatoInDb.Name = editPilotaVM.Name;

            await _repository.UpdateAsync(tracciatoInDb);

            return RedirectToAction("Index");
        }
        #endregion

        #region DELETE : Tracciato/Delete/{guid}
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tracciato = await _repository.GetByGuidAsync(id);
            if (tracciato == null) return NotFound();
            return View(tracciato);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteTracciato(Guid id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
