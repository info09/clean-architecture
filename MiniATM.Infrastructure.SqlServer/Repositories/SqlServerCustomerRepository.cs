using MiniATM.Entities;
using MiniATM.UseCase.Repositories;

namespace MiniATM.Infrastructure.SqlServer.Repositories
{
    public class SqlServerCustomerRepository : ICustomerRepository
    {
        public Task<Customer?> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
