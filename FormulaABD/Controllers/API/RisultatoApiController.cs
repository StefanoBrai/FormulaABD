using FormulaABD.DTOs.Risultato;
using FormulaABD.Helpers;
using FormulaABD.Interfaces;
using FormulaABD.Mappers;
using FormulaABD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace FormulaABD.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RisultatoApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RisultatoApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region GET: api/Risultato
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var risultati = await _unitOfWork.RisultatoRepository.GetAllAsync(query);

                var risultatiDto = risultati.Select(r => r.ToRisultatoDto()).ToList();

                return Ok(risultatiDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Errore durante la get dei risultati: {ex.Message}");
            }
        }
        #endregion

        #region GET: api/Risultato/{guid}
        [HttpGet]
        [Route("{guid:Guid}")]
        public async Task<IActionResult> GetByGuid([FromRoute] Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var risultato = await _unitOfWork.RisultatoRepository.GetByGuidAsync(guid);

                if (risultato == null)
                {
                    return NotFound();
                }

                //var risultatiDto = risultati.Select(r => r.ToRisultatoDto());

                return Ok(risultato.ToRisultatoDto());
            }
            catch (Exception ex)
            {
                return BadRequest($"Errore durante la get del risultato: {ex.Message}");
            }
        }
        #endregion

        #region GET : api/Risultato/Tracciato/{guid}
        [HttpGet]
        [Route("Tracciato/{guid:Guid}")]
        public async Task<IActionResult> GetAllByTracciato([FromRoute] Guid guid)
        {
            try
            {
                var risultati = await _unitOfWork.RisultatoRepository.GetAllByTracciatoGuid(guid);

                if (risultati == null || risultati.Count() == 0)
                {
                    return NotFound();
                }

                return Ok(risultati.Select(r => r.ToRisultatoDto()));
            }
            catch (Exception ex)
            {
                return BadRequest($"Errore durante la get del risultato: {ex.Message}");
            }
        }
        #endregion

        #region GET : api/Risultato/Tracciato/{guidTracciato}/Pilota/{guidPilota}
        [HttpGet]
        [Route("Tracciato/{guidTracciato:Guid}/Pilota/{guidPilota:Guid}")]
        public async Task<IActionResult> GetAllByTracciatoPilota([FromRoute] Guid guidTracciato, [FromRoute] Guid guidPilota)
        {
            try
            {
                var risultati = await _unitOfWork.RisultatoRepository.GetAllByTracciatoPilota(guidTracciato, guidPilota);

                if (risultati == null || risultati.Count() == 0)
                {
                    NotFound();
                }

                return Ok(risultati.Select(r => r.ToRisultatoDto()));
            }
            catch (Exception ex)
            {
                return BadRequest($"Errore durante la get del risultato: {ex.Message}");
            }
        }
        #endregion

        #region PUT : api/Risultato/{guid}
        [HttpPut]
        [Route("{guid:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid guid, [FromBody] UpdateRisultatoDto updateRisultatoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _unitOfWork.BeginTransictionAsync();

                var editedRisultato = new Risultato
                {
                    Id = guid,
                    PilotaId = updateRisultatoDto.PilotaId,
                    TracciatoId = updateRisultatoDto.TracciatoId,
                    TempoGiro = updateRisultatoDto.TempoGiro!
                };

                var risultato = await _unitOfWork.RisultatoRepository.UpdateAsync(editedRisultato);

                if (risultato == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return NotFound();
                }

                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return Ok(risultato.ToRisultatoDto());
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return BadRequest($"Errore durante l'aggiornamento del risultato: {ex.Message}");
            }
        }
        #endregion

        #region POST : api/Risultato
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRisultatoDto createRisultatoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _unitOfWork.BeginTransictionAsync();

                var createdRisultato = await _unitOfWork.RisultatoRepository.CreateAsync(createRisultatoDto.ToRisultato());
                await _unitOfWork.CompleteAsync();

                if (createdRisultato == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return NotFound(new { message = "Risultato non trovato." });
                }

                // Calcolo Punteggi
                var risultati = Funzioni.AggiornaPosizioniEPunteggi(await _unitOfWork.RisultatoRepository.GetAllByTracciatoGuid(createdRisultato.TracciatoId));

                if (risultati == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return StatusCode(500, new { message = "Errore nel calcolo dei punteggi." });
                }

                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return CreatedAtAction(nameof(GetAll), new { id = createdRisultato.Id }, createdRisultato);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return BadRequest($"Errore durante la creazione del risultato: {ex.Message}");
            }
        }
        #endregion

        #region DELETE : api/Risultato/{guid}
        [HttpDelete]
        [Route("{guid:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid guid)
        {
            try
            {
                await _unitOfWork.BeginTransictionAsync();
                var deletedRisultato = await _unitOfWork.RisultatoRepository.DeleteAsync(guid);

                if (deletedRisultato == null)
                {
                    return NotFound();
                }

                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}