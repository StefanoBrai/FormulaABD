using FormulaABD.Data;
using FormulaABD.DTOs.Pilota;
using FormulaABD.Interfaces;
using FormulaABD.Mappers;
using FormulaABD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormulaABD.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PilotaApiController : Controller
    {
        private readonly IPilotaRepository _pilotaRepo;
        public PilotaApiController(IPilotaRepository pilotaRepo)
        {
            _pilotaRepo = pilotaRepo;
        }

        #region GET : api/Pilota
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _pilotaRepo.GetAllAsync());
        }
        #endregion

        #region GET : api/Pilota/{guid}
        [HttpGet]
        [Route("{guid:Guid}")]
        public async Task<IActionResult> GetbyGuid([FromRoute] Guid guid)
        {
            var pilota = await _pilotaRepo.GetByGuidAsync(guid);

            if (pilota == null)
            {
                return NotFound();
            }

            return Ok(pilota);
        }
        #endregion

        #region POST : api/Pilota
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePilotaDto createPilotaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newPilota = new Pilota()
            {
                Name = createPilotaDto.Name
            };

            await _pilotaRepo.CreateAsync(newPilota);

            return Ok(newPilota);
        }
        #endregion

        #region PUT : api/Pilota/{guid}
        [HttpPut]
        [Route("{guid:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid guid, [FromBody] UpdatePilotaDto updatePilotaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pilotaUpdate = new Pilota
            {
                Id = guid,
                Name = updatePilotaDto.Name
            };

            var pilota = await _pilotaRepo.UpdateAsync(pilotaUpdate);

            if (pilota == null) 
            {  
                return NotFound(); 
            }

            return Ok(pilota.ToPilotaDto());
        }
        #endregion

        #region DELETE : api/Pilota/{guid}
        [HttpDelete]
        [Route("{guid:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid guid)
        {
            var pilota = await _pilotaRepo.DeleteAsync(guid);

            if (pilota == null)
            {
                return NoContent();
            }

            return Ok(pilota);
        }
        #endregion
    }
}
