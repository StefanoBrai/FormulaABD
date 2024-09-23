using FormulaABD.Models;

namespace FormulaABD.Helpers
{
    public static class Funzioni
    {
        public static void AggiornaPosizioniEPunteggi(List<Risultato> risultati)
        {
            risultati = risultati.OrderBy(r => r.TempoGiro).ToList();

            for (int i = 0; i < risultati.Count; i++)
            {
                risultati[i].Posizione = i + 1;
                risultati[i].PunteggioPosizione = CalcolaPunteggioPosizione(risultati[i].Posizione);
                risultati[i].PunteggioDistacco = 30 - CalcolaPunteggioDistacco(risultati[0].TempoGiro, risultati[i].TempoGiro);
                risultati[i].TotalePunteggioGara = risultati[i].PunteggioPosizione + risultati[i].PunteggioDistacco;
            }
        }

        public static int CalcolaPunteggioPosizione(int posizione)
        {
            switch (posizione)
            {
                case 1: return 25;
                case 2: return 18;
                case 3: return 15;
                case 4: return 12;
                default: return 0;
            }
        }

        public static int CalcolaPunteggioDistacco(string tempoPrimo, string tempoAttuale) 
        {
            var primoTempo = ParseCustomTimeSpan(tempoPrimo);
            var attualeTempo = ParseCustomTimeSpan(tempoAttuale);
            var distacco = attualeTempo - primoTempo;

            return distacco.Seconds;
        }

        public static TimeSpan ParseCustomTimeSpan(string timeString)
        {
            string[] parts = timeString.Split(':', '.');

            if (parts.Length != 3)
            {
                throw new FormatException("Invalid time format. Expected mm:ss.fff");
            }

            if (!int.TryParse(parts[0], out int minutes) ||
                !int.TryParse(parts[1], out int seconds) ||
                !int.TryParse(parts[2], out int milliseconds))
            {
                throw new FormatException("Invalid time format. Unable to parse numbers.");
            }

            return new TimeSpan(0, 0, minutes, seconds, milliseconds);
        }
    }
}
