using FormulaABD.DTOs.Pilota;
using FormulaABD.Models;

namespace FormulaABD.Interfaces
{
    public interface IPilotaRepository
    {
        Task<IEnumerable<Pilota>> GetAllAsync();
        Task<Pilota?> GetByGuidAsync(Guid guid);
        Task<Pilota> CreateAsync(Pilota pilota);
        Task<Pilota> UpdateAsync(Pilota pilota);
        Task<Pilota?> DeleteAsync(Guid guid);
    }
}
