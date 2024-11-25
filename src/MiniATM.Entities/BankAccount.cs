using MiniATM.Entities.Exceptions;

namespace MiniATM.Entities
{
    public class BankAccount
    {
        public required string Id { get; set; }

        public required Guid CustomerId { get; set; }

        public required string Currency { get; set; }

        public bool IsLocked { get; set; }

        public double MinimumRequiredAmount { get; set; }

        private double balance;

        public double Balance
        {
            get => balance;
            set
            {
                if (value < MinimumRequiredAmount) throw new InvalidBalanceException();
                balance = value;
            }
        }
    }
}
