using FormulaABD.DTOs.Pilota;
using FormulaABD.Models;

namespace FormulaABD.Mappers
{
    public static class PilotaMappers
    {
        public static PilotaDto ToPilotaDto(this Pilota pilota)
        {
            return new PilotaDto
            {
                Name = pilota.Name
            };
        }
    }
}
