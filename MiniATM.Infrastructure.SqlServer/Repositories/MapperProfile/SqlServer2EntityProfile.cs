using AutoMapper;

namespace MiniATM.Infrastructure.SqlServer.Repositories.MapperProfile
{
    public class SqlServer2EntityProfile : Profile
    {
        public SqlServer2EntityProfile()
        {
            CreateMap<DataContext.Customer, Entities.Customer>();
            CreateMap<DataContext.BankAccount, Entities.BankAccount>().ReverseMap();

            CreateMap<Entities.Transaction, DataContext.Transaction>().ReverseMap();
        }
    }
}
