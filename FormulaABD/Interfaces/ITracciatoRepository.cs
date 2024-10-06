using FormulaABD.DTOs.Tracciato;
using FormulaABD.Models;

namespace FormulaABD.Interfaces
{
    public interface ITracciatoRepository
    {
        Task<IEnumerable<Tracciato>> GetAllAsync();
        Task<Tracciato> GetByGuidAsync(Guid guid);
        Task<Tracciato> CreateAsync(Tracciato tracciato);
        Task<Tracciato> UpdateAsync(Tracciato tracciato);
        Task<Tracciato> DeleteAsync(Guid id);
    }
}
