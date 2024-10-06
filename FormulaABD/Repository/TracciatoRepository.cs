using FormulaABD.Data;
using FormulaABD.DTOs.Tracciato;
using FormulaABD.Interfaces;
using FormulaABD.Models;
using Microsoft.EntityFrameworkCore;

namespace FormulaABD.Repository
{
    public class TracciatoRepository : ITracciatoRepository
    {
        private readonly ApplicationDbContext _context;

        public TracciatoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tracciato> CreateAsync(Tracciato tracciato)
        {
            _context.Tracciati.Add(tracciato);
            await _context.SaveChangesAsync();

            return tracciato;
        }

        public async Task<IEnumerable<Tracciato>> GetAllAsync()
        {
            return await _context.Tracciati.ToListAsync();
        }

        public async Task<Tracciato> GetByGuidAsync(Guid guid)
        {
            return await _context.Tracciati.FirstOrDefaultAsync(t => t.Id == guid);
        }

        public async Task<Tracciato> DeleteAsync(Guid id)
        {
            var tracciato = await _context.Tracciati.FirstOrDefaultAsync(t => t.Id == id);

            if (tracciato == null)
            {
                return null;
            }

            _context.Remove(tracciato);
            await _context.SaveChangesAsync();

            return tracciato;
        }

        public async Task<Tracciato> UpdateAsync(Tracciato tracciato)
        {
            var existingTracciato = await _context.Tracciati.FirstOrDefaultAsync(x => x.Id == tracciato.Id);

            if (existingTracciato == null)
            {
                return null;
            }

            existingTracciato.Name = tracciato.Name;

            await _context.SaveChangesAsync();

            return existingTracciato;
        }
    }
}
