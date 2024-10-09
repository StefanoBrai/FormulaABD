using FormulaABD.DTOs.Risultato;
using FormulaABD.Models;
using FormulaABD.ViewModels;

namespace FormulaABD.Mappers
{
    public static class RisultatoMappers
    {
        public static Risultato ToRisultato(this CreateRisultatoVM createRisultatoVM)
        {
            return new Risultato
            {
                TracciatoId = createRisultatoVM.TracciatoId,
                PilotaId = createRisultatoVM.PilotaId,
                TempoGiro = createRisultatoVM.TempoGiro
            };
        }

        public static Risultato ToRisultato(this CreateRisultatoDto createRisultatoDto)
        {
            return new Risultato
            {
                TracciatoId = createRisultatoDto.TracciatoId,
                PilotaId = createRisultatoDto.PilotaId,
                TempoGiro = createRisultatoDto.TempoGiro
            };
        }

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
