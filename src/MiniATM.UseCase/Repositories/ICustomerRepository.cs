using MiniATM.Entities;

namespace MiniATM.UseCase.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> FindByIdAsync(Guid id);
    }
}
