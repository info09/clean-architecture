using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniATM.Infrastructure.SqlServer.Repositories.DataContext;
using MiniATM.UseCase.Repositories;

namespace MiniATM.Infrastructure.SqlServer.Repositories
{
    public class SqlServerCustomerRepository : ICustomerRepository
    {
        private readonly MiniATMDbContext _context;
        private readonly IMapper _mapper;

        public SqlServerCustomerRepository(MiniATMDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Entities.Customer?> FindByIdAsync(Guid id)
        {
            var customer = await _context.Customers.Where(i => i.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<Entities.Customer>(customer);
        }
    }
}
