using System.ComponentModel.DataAnnotations.Schema;

namespace FormulaABD.Models
{
    [Table("Risultati")]
    public class Risultato
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TracciatoId { get; set; }
        public Guid PilotaId { get; set; }
        public string TempoGiro { get; set; }

        // Navigations property
        public Pilota Pilota { get; set; }
        public Tracciato Tracciato { get; set; }

        // Valori calcolati
        public int Posizione { get; set; }
        public int PunteggioPosizione { get; set; }
        public int PunteggioDistacco { get; set; }
        public int TotalePunteggioGara { get; set; }
    }
}
