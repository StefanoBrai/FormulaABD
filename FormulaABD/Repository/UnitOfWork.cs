using FormulaABD.Data;
using FormulaABD.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace FormulaABD.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;
        public IRisultatoRepository RisultatoRepository { get; private set; }
        public IPilotaRepository PilotaRepository { get; private set; }
        public ITracciatoRepository TracciatoRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            RisultatoRepository = new RisultatoRepository(_context);
            PilotaRepository = new PilotaRepository(_context);
            TracciatoRepository = new TracciatoRepository(_context);
        }

        public async Task BeginTransictionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.CommitAsync();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
            _transaction.Dispose();
        }
    }
}
