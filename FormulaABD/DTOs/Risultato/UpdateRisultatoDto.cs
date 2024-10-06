namespace FormulaABD.DTOs.Risultato
{
    public class UpdateRisultatoDto
    {
        public Guid TracciatoId { get; set; }
        public Guid PilotaId { get; set; }
        public string TempoGiro { get; set; }
    }
}
