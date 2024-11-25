using MiniATM.UseCase.Repositories;

namespace MiniATM.UseCase.UnitOfWork
{
    public interface ITransactionUnitOfWork
    {
        ITransactionRepository TransactionRepository { get; }
        IBankAccountRepository BankAccountRepository { get; }

        Task BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CancelTransactionAsync();
    }
}
