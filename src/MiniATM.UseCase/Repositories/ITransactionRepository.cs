using MiniATM.Entities;

namespace MiniATM.UseCase.Repositories
{
    public interface ITransactionRepository
    {
        Task Add(Transaction transaction);
    }
}
