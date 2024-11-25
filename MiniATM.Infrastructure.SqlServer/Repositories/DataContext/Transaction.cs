using MiniATM.Entities;
using System.ComponentModel.DataAnnotations;

namespace MiniATM.Infrastructure.SqlServer.Repositories.DataContext
{
    public class Transaction
    {
        public required Guid Id { get; set; }
        public required TransactionTypes TransactionTypes { get; set; }
        [MaxLength(50)]
        public required string AccountId { get; set; }
        public required double Amount { get; set; }
        public DateTime DateTimeUTC { get; set; }
        [MaxLength(200)]
        public required string Notes { get; set; }
    }
}
