using MiniATM.Entities;

namespace MiniATM.UseCase
{
    public interface IBankAccountFinder
    {
        Task<IEnumerable<BankAccount>> FindByCustomerId(Guid customerId);
    }
}
