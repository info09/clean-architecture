using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniATM.Infrastructure.CashStorage;
using MiniATM.Infrastructure.Models;
using MiniATM.Infrastructure.SqlServer.Repositories;
using MiniATM.Infrastructure.SqlServer.Repositories.DataContext;
using MiniATM.Infrastructure.SqlServer.Repositories.MapperProfile;
using MiniATM.UseCase;
using MiniATM.UseCase.Repositories;
using MiniATM.UseCase.UnitOfWork;

namespace MiniATM.Infrastructure
{
    public static class ServicesRegister
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var repositoryOptions = configuration.GetSection("RepositoryOptions").Get<RepositoryOptions>() ?? throw new Exception("No RepositoryOptions found");

            if (repositoryOptions.RepositoryType == RepositoryTypes.SqlServer)
            {
                services.AddAutoMapper(typeof(SqlServer2EntityProfile));

                services.AddDbContext<MiniATMDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MiniATMDatabase")));

                services.AddTransient<IBankAccountRepository>(services => new SqlServerBankAccountRepository(services.GetRequiredService<MiniATMDbContext>(), services.GetRequiredService<IMapper>()));
                services.AddTransient<ICustomerRepository>(services => new SqlServerCustomerRepository(services.GetRequiredService<MiniATMDbContext>(), services.GetRequiredService<IMapper>()));
                services.AddTransient<ITransactionRepository>(services => new SqlServerTransactionRepository(services.GetRequiredService<MiniATMDbContext>(), services.GetRequiredService<IMapper>()));
                services.AddTransient<IBankAccountFinder>(services => new BankAccountFinderRepository(services.GetRequiredService<IBankAccountRepository>()));
                services.AddTransient<ITransactionUnitOfWork>(services => new SqlServerTransactionUnitOfWork(services.GetRequiredService<MiniATMDbContext>(), services.GetRequiredService<IMapper>()));

                services.AddTransient<ICashStorage>(services => new InMemoryCashStorage(services.GetRequiredService<ILogger<InMemoryCashStorage>>(), 5000));
                services.AddTransient<ITransferManager>(services => new TransferManager(services.GetRequiredService<ITransactionUnitOfWork>()));
                services.AddTransient<ICashWithdrawalManager>(services => new CashWithdrawalManager(services.GetRequiredService<ITransactionUnitOfWork>(), services.GetRequiredService<ICashStorage>()));
            }
        }
    }
}
