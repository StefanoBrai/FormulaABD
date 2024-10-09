using FormulaABD.DTOs.Risultato;
using FormulaABD.Helpers;
using FormulaABD.Models;

namespace FormulaABD.Interfaces
{
    public interface IRisultatoRepository
    {
        Task<IEnumerable<Risultato>> GetAllAsync(QueryObject query);
        Task<Risultato> GetByGuidAsync(Guid guid);
        Task<List<Risultato>> GetAllByTracciatoGuid(Guid guidTracciato);
        Task<List<Risultato>> GetAllByTracciatoPilota(Guid guidTracciato, Guid guidPilota);
        Task<Risultato> CreateAsync(Risultato risultato);
        Task<Risultato> UpdateAsync(Risultato risultato);
        Task<Risultato> DeleteAsync(Guid guid);
    }
}
