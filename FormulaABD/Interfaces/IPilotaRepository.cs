using FormulaABD.Models;

namespace FormulaABD.Interfaces
{
    public interface IPilotaRepository
    {
        Task<List<Pilota>> GetAllAsync();
        Task<Pilota?> GetByGuidAsync(Guid guid);
        Task<Pilota> CreateAsync(Pilota pilota);
        Task<Pilota?> DeleteAsync(Guid guid);
    }
}
