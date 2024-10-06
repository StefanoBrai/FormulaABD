using FormulaABD.DTOs.Risultato;
using FormulaABD.Models;

namespace FormulaABD.Mappers
{
    public static class RisultatoMappers
    {
        public static RisultatoDto ToRisultatoDto(this Risultato risultato)
        {
            return new RisultatoDto
            {
                Id = risultato.Id,
                TracciatoName = risultato.Tracciato.Name,
                PilotaName = risultato.Pilota.Name,
                TempoGiro = risultato.TempoGiro,
                Posizione = risultato.Posizione,
                PunteggioPosizione = risultato.PunteggioPosizione,
                PunteggioDistacco = risultato.PunteggioDistacco,
                TotalePunteggioGara = risultato.TotalePunteggioGara
            };
        }

        public static UpdateRisultatoDto ToUpdateRisultatoDto(this Risultato risultato)
        {
            return new UpdateRisultatoDto
            {
                TracciatoId = risultato.TracciatoId,
                PilotaId = risultato.PilotaId,
                TempoGiro = risultato.TempoGiro
            };
        }
    }
}
