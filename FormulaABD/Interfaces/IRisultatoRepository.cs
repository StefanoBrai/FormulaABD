using FormulaABD.Helpers;
using FormulaABD.Models;

namespace FormulaABD.Interfaces
{
    public interface IRisultatoRepository
    {
        Task<List<Risultato>> GetAllAsync(QueryObject query);
        Task<Risultato> GetByGuidAsync(Guid guid);
        Task<List<Risultato>> GetAllByTracciatoGuid(Guid guidTracciato);
        Task<Risultato> CreateAsync(Risultato risultato);
        Task<Risultato> UpdateAsync(Guid guid, Risultato risultato);
        Task<Risultato> DeleteAsync(Guid guid);
    }
}
