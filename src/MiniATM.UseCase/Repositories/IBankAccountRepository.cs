using MiniATM.Entities;

namespace MiniATM.UseCase.Repositories
{
    public interface IBankAccountRepository
    {
        Task<BankAccount?> FindByIdAsync(string accountId);
        Task<IEnumerable<BankAccount>> FindByCustomerId(Guid customerId);
        Task UpdateAsync(BankAccount fromAccount);
    }
}
