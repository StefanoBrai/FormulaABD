using FormulaABD.DTOs.Risultato;
using FormulaABD.Helpers;
using FormulaABD.Interfaces;
using FormulaABD.Mappers;
using FormulaABD.Models;
using FormulaABD.Repository;
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

        #region GET : Risultato
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
        #endregion

        #region GET : Risultato/Details/{guid}
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
        #endregion

        #region POST : Risultato/Create
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

                var createdRisultato = await _unitOfWork.RisultatoRepository.CreateAsync(createRisultatoVM.ToRisultato());
                await _unitOfWork.CompleteAsync();

                if (createdRisultato == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return View("NotFound");
                }

                // Calcolo Punteggi
                var risultati = Funzioni.AggiornaPosizioniEPunteggi(await _unitOfWork.RisultatoRepository.GetAllByTracciatoGuid(createdRisultato.TracciatoId));

                if (risultati == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return View("Error");
                }

                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante la creazione del risultato: {ex.Message}");
                return View(createRisultatoVM);
            }
        }
        #endregion

        #region PUT : Risultato/Edit/{guid}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                await _unitOfWork.BeginTransictionAsync();

                var risulttatoInDb = await _unitOfWork.RisultatoRepository.GetByGuidAsync(id);
                var piloti = await _unitOfWork.PilotaRepository.GetAllAsync();
                var tracciati = await _unitOfWork.TracciatoRepository.GetAllAsync();

                var viewModel = new EditRisultatoVM
                {
                    Id = id,
                    PilotaId = risulttatoInDb.PilotaId,
                    TracciatoId = risulttatoInDb.TracciatoId,
                    TempoGiro = risulttatoInDb.TempoGiro,

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
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Si è verificato un errore durante la modifica del risultato: {ex.Message}");
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRisultatoVM editRisultatoVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit Pilota");
                return View("Edit", editRisultatoVM);
            }

            try
            {
                await _unitOfWork.BeginTransictionAsync();

                var ris = new Risultato
                {
                    Id = editRisultatoVM.Id,
                    PilotaId = editRisultatoVM.PilotaId,
                    TracciatoId = editRisultatoVM.TracciatoId,
                    TempoGiro = editRisultatoVM.TempoGiro!
                };

                // Calcolo Punteggi
                var risultato = await _unitOfWork.RisultatoRepository.UpdateAsync(ris);

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
                ModelState.AddModelError("", $"Si è verificato un errore durante la modifica del risultato: {ex.Message}");
                return View(editRisultatoVM);
            }
        }
        #endregion

        #region DELETE : Risultato/Delete/{guid}
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
        #endregion
    }
}