using FormulaABD.DTOs.Tracciato;
using FormulaABD.Interfaces;
using FormulaABD.Mappers;
using FormulaABD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FormulaABD.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracciatoApiController : ControllerBase
    {
        private readonly ITracciatoRepository _tracciatoRepo;

        public TracciatoApiController(ITracciatoRepository tracciatoRepo)
        {
            _tracciatoRepo = tracciatoRepo;
        }

        #region GET : api/Tracciato
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tracciatoRepo.GetAllAsync());
        }
        #endregion

        #region GET : api/Tracciato/{guid}
        [HttpGet]
        [Route("{guid:Guid}")]
        public async Task<IActionResult> GetByGuid([FromRoute] Guid guid)
        {
            var tracciato = await _tracciatoRepo.GetByGuidAsync(guid);

            if (tracciato == null)
            {
                return NotFound();
            }

            return Ok(tracciato);
        }
        #endregion

        #region POST : api/Tracciato
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTracciatoDto createTracciatoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuovoTracciato = new Tracciato()
            {
                Name = createTracciatoDto.Name
            };

            await _tracciatoRepo.CreateAsync(nuovoTracciato);

            return CreatedAtAction(nameof(GetAll), nuovoTracciato);
        }
        #endregion

        #region PUT : api/Tracciato/{guid}
        [HttpPut]
        [Route("{guid:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid guid, [FromBody] UpdateTracciatoDto updateTracciatoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tracciatoUpdate = new Tracciato
            {
                Id = guid,
                Name = updateTracciatoDto.Name
            };

            var tracciato = await _tracciatoRepo.UpdateAsync(tracciatoUpdate);

            if (tracciato == null)
            {
                return NotFound();
            }

            return Ok(tracciato.ToTracciatoDto());
        }
        #endregion

        #region DELETE : api/Tracciato
        [HttpDelete]
        [Route("{guid:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid guid)
        {
            var tracciato = await _tracciatoRepo.DeleteAsync(guid);

            if (tracciato == null)
            {
                return NoContent();
            }

            return Ok(tracciato);
        }
        #endregion
    }
}