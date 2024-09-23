using FormulaABD.Models;

namespace FormulaABD.Interfaces
{
    public interface ITracciatoRepository
    {
        Task<List<Tracciato>> GetAllAsync();
        Task<Tracciato> GetByGuidAsync(Guid guid);
        Task<Tracciato> CreateAsync(Tracciato tracciato);
        Task<Tracciato> RemoveAsync(Guid id);
    }
}
