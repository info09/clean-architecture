using AutoMapper;
using MiniATM.Infrastructure.SqlServer.Repositories.DataContext;
using MiniATM.UseCase.Repositories;

namespace MiniATM.Infrastructure.SqlServer.Repositories
{
    public class SqlServerTransactionRepository : ITransactionRepository
    {
        private readonly IMapper _mapper;
        private readonly MiniATMDbContext _dbContext;

        public SqlServerTransactionRepository(MiniATMDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task Add(Entities.Transaction transaction)
        {
            var dbtransaction = _mapper.Map<DataContext.Transaction>(transaction);

            _dbContext.Transactions.Add(dbtransaction);
            await _dbContext.SaveChangesAsync();
        }
    }
}
