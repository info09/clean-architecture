using AutoMapper;
using MiniATM.Infrastructure.SqlServer.Repositories.DataContext;
using MiniATM.UseCase.Repositories;
using MiniATM.UseCase.UnitOfWork;

namespace MiniATM.Infrastructure.SqlServer.Repositories
{
    public class SqlServerTransactionUnitOfWork : ITransactionUnitOfWork
    {
        private readonly MiniATMDbContext _dbContext;
        private readonly IMapper _mapper;

        public SqlServerTransactionUnitOfWork(MiniATMDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

            TransactionRepository = new SqlServerTransactionRepository(_dbContext, _mapper);
            BankAccountRepository = new SqlServerBankAccountRepository(_dbContext, _mapper);
        }

        public ITransactionRepository TransactionRepository { get; }

        public IBankAccountRepository BankAccountRepository { get; }

        public Task BeginTransactionAsync()
        {
            return Task.CompletedTask;
        }

        public Task CancelTransactionAsync()
        {
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
