using MiniATM.Entities;
using MiniATM.UseCase.Repositories;

namespace MiniATM.UseCase
{
    public class BankAccountFinderRepository : IBankAccountFinder
    {
        private readonly IBankAccountRepository _repository;

        public BankAccountFinderRepository(IBankAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BankAccount>> FindByCustomerId(Guid customerId)
        {
            return await _repository.FindByCustomerId(customerId);
        }
    }
}
