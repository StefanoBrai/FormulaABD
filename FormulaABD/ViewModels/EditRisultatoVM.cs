using FormulaABD.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormulaABD.ViewModels
{
    public class EditRisultatoVM
    {
        public Guid Id { get; set; }
        public Guid TracciatoId { get; set; }
        public Guid PilotaId { get; set; }
        public string? TempoGiro { get; set; }
        public List<SelectListItem>? Piloti { get; set; }
        public List<SelectListItem>? Tracciati { get; set; }
    }
}
