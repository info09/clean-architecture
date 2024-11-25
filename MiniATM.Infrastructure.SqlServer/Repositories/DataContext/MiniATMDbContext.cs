using Microsoft.EntityFrameworkCore;

namespace MiniATM.Infrastructure.SqlServer.Repositories.DataContext
{
    public class MiniATMDbContext : DbContext
    {
        private readonly string _connectionString;

        public MiniATMDbContext()
        {
            _connectionString = @"Server=localhost,1433;Database=MiniATM;User Id=sa;Password=123456aA@;Multipleactiveresultsets=true;TrustServerCertificate=True";
        }

        public MiniATMDbContext(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
