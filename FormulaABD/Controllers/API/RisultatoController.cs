using FormulaABD.DTOs.Risultato;
using FormulaABD.Helpers;
using FormulaABD.Interfaces;
using FormulaABD.Mappers;
using FormulaABD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FormulaABD.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RisultatoController : ControllerBase
    {
        private readonly IRisultatoRepository _risultatoRepo;

        public RisultatoController(IRisultatoRepository risultatoRepo)
        {
            _risultatoRepo = risultatoRepo;
        }

        #region GET: api/Risultato
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var risultati = await _risultatoRepo.GetAllAsync(query);

            if (risultati == null || risultati.Count == 0)
            {
                return NotFound();
            }

            var risultatiDto = risultati.Select(r => r.ToRisultatoDto()).ToList();

            return Ok(risultatiDto);
        }
        #endregion

        #region GET: api/Risultato/{guid}
        [HttpGet]
        [Route("{guid:Guid}")]
        public async Task<IActionResult> GetByGuid([FromRoute] Guid guid)
        {
            var risultato = await _risultatoRepo.GetByGuidAsync(guid);

            if (risultato == null)
            {
                return NotFound();
            }

            return Ok(risultato);
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

            var nuovoRisultato = new Risultato
            {
                TracciatoId = createRisultatoDto.TracciatoId,
                PilotaId = createRisultatoDto.PilotaId,
                TempoGiro = createRisultatoDto.TempoGiro
            };

            await _risultatoRepo.CreateAsync(nuovoRisultato);

            // Calcolo Punteggi
            var risultati = await _risultatoRepo.GetAllByTracciatoGuid(createRisultatoDto.TracciatoId);

            Funzioni.AggiornaPosizioniEPunteggi(risultati);

            foreach (var risultato in risultati)
            {
                await _risultatoRepo.UpdateAsync(risultato.Id, risultato);
            }

            return CreatedAtAction(nameof(GetAll), new { id = nuovoRisultato.Id, nuovoRisultato });
        }
        #endregion
    }
}
