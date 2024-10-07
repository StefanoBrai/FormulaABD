using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormulaABD.Models
{
    [Table("Risultati")]
    public class Risultato
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TracciatoId { get; set; }
        public Guid PilotaId { get; set; }

        [Display(Name = "Tempo")]
        public string TempoGiro { get; set; }

        // Navigations property
        public Pilota Pilota { get; set; }
        public Tracciato Tracciato { get; set; }

        // Valori calcolati
        public int Posizione { get; set; }

        [Display(Name = "Punteggio Posizione")]
        public int PunteggioPosizione { get; set; }

        [Display(Name = "Punteggio Distacco")]
        public int PunteggioDistacco { get; set; }

        [Display(Name = "Totale")]
        public int TotalePunteggioGara { get; set; }
    }
}
