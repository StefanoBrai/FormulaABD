using FormulaABD.Models;

namespace FormulaABD.DTOs.Risultato
{
    public class RisultatoDto
    {
        public Guid Id { get; set; }
        public string TracciatoName { get; set; }
        public string PilotaName { get; set; }
        public string TempoGiro { get; set; }

        // Valori calcolati
        public int Posizione { get; set; }
        public int PunteggioPosizione { get; set; }
        public int PunteggioDistacco { get; set; }
        public int TotalePunteggioGara { get; set; }
    }
}
