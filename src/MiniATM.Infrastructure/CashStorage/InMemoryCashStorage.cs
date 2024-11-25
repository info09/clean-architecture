using MiniATM.UseCase;

namespace MiniATM.Infrastructure.CashStorage
{
    public class InMemoryCashStorage : ICashStorage
    {
        private readonly ILogger<InMemoryCashStorage> _logger;
        private SpinLock _lock;
        private double _avaliableAmount;

        public InMemoryCashStorage(ILogger<InMemoryCashStorage> logger, double avaliableAmount)
        {
            _logger = logger;
            _lock = new SpinLock();
            _avaliableAmount = avaliableAmount;

            _logger.LogInformation($"CashStorage loaded with amount: {avaliableAmount}");
        }

        public bool IsCashAmountAvailable(double amount)
        {
            var avaliable = _avaliableAmount >= amount;
            _logger.LogInformation($"IsCashAmountAvailable({amount}): {avaliable} (current amount: {_avaliableAmount})");
            return avaliable;
        }

        public bool Withdraw(double amount)
        {
            var gotlock = false;
            var success = false;
            try
            {
                _lock.Enter(ref gotlock);
                if (_avaliableAmount >= amount)
                {
                    _avaliableAmount = _avaliableAmount - amount;
                    success = true;
                }
            }
            finally
            {
                if (gotlock) _lock.Exit();
            }

            _logger.LogInformation($"Withdraw({amount}) result: {success} (current amount: {_avaliableAmount})");

            return success;
        }
    }
}
