using FormulaABD.DTOs.Risultato;
using FormulaABD.Helpers;
using FormulaABD.Interfaces;
using FormulaABD.Mappers;
using FormulaABD.Models;
using FormulaABD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

namespace FormulaABD.Controllers
{
    public class RisultatoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RisultatoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Error");
            }

            try
            {
                var risultati = await _unitOfWork.RisultatoRepository.GetAllAsync(query);

                return View(risultati);
            }
            catch (Exception ex)
            {
                return BadRequest($"Errore durante la get dei risultati: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var risultato = await _unitOfWork.RisultatoRepository.GetByGuidAsync(id);

                return View(risultato);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante il recupero del risultato: {ex.Message}");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var piloti = await _unitOfWork.PilotaRepository.GetAllAsync();
            var tracciati = await _unitOfWork.TracciatoRepository.GetAllAsync();

            var viewModel = new CreateRisultatoVM
            {
                Piloti = piloti.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList(),

                Tracciati = tracciati.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRisultatoVM createRisultatoVM)
        {
            if (!ModelState.IsValid)
            {
                return View(createRisultatoVM);
            }

            try
            {
                await _unitOfWork.BeginTransictionAsync();

                var nuovoRisultato = new Risultato
                {
                    TracciatoId = createRisultatoVM.TracciatoId,
                    PilotaId = createRisultatoVM.PilotaId,
                    TempoGiro = createRisultatoVM.TempoGiro
                };

                await _unitOfWork.RisultatoRepository.CreateAsync(nuovoRisultato);
                await _unitOfWork.CompleteAsync();

                // Calcolo Punteggi
                var risultato = await _unitOfWork.RisultatoRepository.UpdateAsync(nuovoRisultato.Id, nuovoRisultato.ToUpdateRisultatoDto());

                if (risultato == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return NotFound();
                }

                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante la creazione del risultato: {ex.Message}");
                return View(createRisultatoVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var risultato = await _unitOfWork.RisultatoRepository.GetByGuidAsync(id);
            if (risultato == null) return View("Error");
            return View(risultato);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRisultato(Guid id)
        {
            await _unitOfWork.RisultatoRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
