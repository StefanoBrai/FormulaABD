using FormulaABD.DTOs.Tracciato;
using FormulaABD.Models;

namespace FormulaABD.Mappers
{
    public static class TracciatoMappers
    {
        public static TracciatoDto ToTracciatoDto(this Tracciato tracciato)
        {
            return new TracciatoDto
            {
                Name = tracciato.Name
            };
        }
    }
}
