namespace FormulaABD.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRisultatoRepository RisultatoRepository { get; }
        IPilotaRepository PilotaRepository { get; }
        ITracciatoRepository TracciatoRepository { get; }

        Task<int> CompleteAsync();
        Task BeginTransictionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
