using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniATM.Infrastructure.SqlServer.Repositories.DataContext;
using MiniATM.UseCase.Repositories;

namespace MiniATM.Infrastructure.SqlServer.Repositories
{
    public class SqlServerBankAccountRepository : IBankAccountRepository
    {
        private readonly MiniATMDbContext _context;
        private readonly IMapper _mapper;

        public SqlServerBankAccountRepository(MiniATMDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Entities.BankAccount>> FindByCustomerId(Guid customerId)
        {
            var bankAccounts = await _context.BankAccounts.Where(i => i.CustomerId == customerId).ToListAsync();
            return _mapper.Map<IEnumerable<Entities.BankAccount>>(bankAccounts);
        }

        public async Task<Entities.BankAccount?> FindByIdAsync(string accountId)
        {
            var bankAccount = await _context.BankAccounts.Where(i => i.Id == accountId).FirstOrDefaultAsync();
            return _mapper.Map<Entities.BankAccount>(bankAccount);
        }

        public async Task UpdateAsync(Entities.BankAccount fromAccount)
        {
            var bankAccount = await _context.BankAccounts.Where(i => i.Id == fromAccount.Id).FirstOrDefaultAsync();
            if (bankAccount != null)
            {
                _mapper.Map(fromAccount, bankAccount);
            }

            await _context.SaveChangesAsync();
        }
    }
}
