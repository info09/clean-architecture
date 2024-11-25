using System.ComponentModel.DataAnnotations;

namespace MiniATM.Infrastructure.SqlServer.Repositories.DataContext
{
    public class Customer
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
    }
}
