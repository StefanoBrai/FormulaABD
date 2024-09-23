using FormulaABD.Data;
using FormulaABD.Helpers;
using FormulaABD.Interfaces;
using FormulaABD.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FormulaABD.Repository
{
    public class RisultatoRepository : IRisultatoRepository
    {
        private readonly ApplicationDbContext _context;

        public RisultatoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Risultato> CreateAsync(Risultato risultato)
        {
            await _context.Risultati.AddAsync(risultato);
            await _context.SaveChangesAsync();

            return risultato;
        }

        public async Task<Risultato> DeleteAsync(Guid guid)
        {
            var risultato = await _context.Risultati.FirstOrDefaultAsync(r => r.Id == guid);

            if (risultato == null)
            {
                return null;
            }

            _context.Remove(risultato);
            await _context.SaveChangesAsync();

            return risultato;
        }

        public async Task<List<Risultato>> GetAllAsync(QueryObject query)
        {
            var risultati = _context.Risultati.Include(p => p.Pilota).Include(t => t.Tracciato).AsQueryable();

            var tracciati = await _context.Tracciati.ToListAsync();

            // Filtro
            if (!string.IsNullOrWhiteSpace(query.nomeTracciato))
            {
                risultati = risultati.Where(r => r.Tracciato.Name.ToLower() == query.nomeTracciato.ToLower());
            }

            return await risultati.ToListAsync();
        }

        public async Task<Risultato> GetByGuidAsync(Guid guid)
        {
            return await _context.Risultati.Include(p => p.Pilota).Include(t => t.Tracciato).FirstOrDefaultAsync(r => r.Id == guid);
        }

        public async Task<Risultato> UpdateAsync(Guid guid, Risultato risultato)
        {
            var existingRisultato = _context.Risultati.FirstOrDefault(r => r.Id == risultato.Id);

            if (existingRisultato == null)
            {
                return null;
            }

            existingRisultato.TracciatoId = risultato.TracciatoId;
            existingRisultato.PilotaId = risultato.PilotaId;
            existingRisultato.TempoGiro = risultato.TempoGiro;

            await _context.SaveChangesAsync();

            return existingRisultato;
        }

        public async Task<List<Risultato>> GetAllByTracciatoGuid(Guid guidTracciato)
        {
            return await _context.Risultati.Include(p => p.Pilota).Include(t => t.Tracciato).Where(r => r.TracciatoId == guidTracciato).ToListAsync();
        }
    }
}
