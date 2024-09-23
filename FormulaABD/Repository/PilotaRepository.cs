using FormulaABD.Data;
using FormulaABD.Interfaces;
using FormulaABD.Models;
using Microsoft.EntityFrameworkCore;

namespace FormulaABD.Repository
{
    public class PilotaRepository : IPilotaRepository
    {
        private readonly ApplicationDbContext _context;

        public PilotaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pilota> CreateAsync(Pilota pilota)
        {
            await _context.AddAsync(pilota);
            await _context.SaveChangesAsync();

            return pilota;
        }

        public async Task<Pilota?> DeleteAsync(Guid guid)
        {
            var pilota = await _context.Piloti.FirstOrDefaultAsync(p => p.Id == guid);

            if (pilota == null)
            {
                return null;
            }

            _context.Remove(pilota);
            await _context.SaveChangesAsync();

            return pilota;
        }

        public async Task<List<Pilota>> GetAllAsync()
        {
            return await _context.Piloti.ToListAsync();
        }

        public async Task<Pilota?> GetByGuidAsync(Guid guid)
        {
            return await _context.Piloti.FirstOrDefaultAsync(p => p.Id == guid);
        }
    }
}
