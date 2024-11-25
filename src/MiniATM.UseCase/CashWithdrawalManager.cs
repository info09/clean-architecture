using MiniATM.UseCase.UnitOfWork;

namespace MiniATM.UseCase
{
    public class CashWithdrawalManager : ICashWithdrawalManager
    {
        private readonly ITransactionUnitOfWork _transactionUnitOfWork;
        private readonly ICashStorage _cashStorage;
        private bool _useSafeCashWithdrawal;

        public CashWithdrawalManager(ITransactionUnitOfWork transactionUnitOfWork, ICashStorage cashStorage, bool useSafeCashWithdrawal = true)
        {
            _transactionUnitOfWork = transactionUnitOfWork ?? throw new ArgumentNullException(nameof(transactionUnitOfWork));
            _cashStorage = cashStorage;
            _useSafeCashWithdrawal = useSafeCashWithdrawal;
        }

        public async Task<TransactionResult> WithdrawAsync(string accountId, double amount)
        {
            try
            {
                await _transactionUnitOfWork.BeginTransactionAsync();

                var fromAccount = await _transactionUnitOfWork.BankAccountRepository.FindByIdAsync(accountId);
                if (fromAccount == null || fromAccount.IsLocked)
                    return TransactionResult.SourceNotFound;

                var remainingAmount = fromAccount.Balance - amount;
                if (remainingAmount < 0)
                    return TransactionResult.BalanceTooLow;

                if (!_cashStorage.IsCashAmountAvailable(amount))
                    return TransactionResult.CashNotAvailable;

                if (_useSafeCashWithdrawal)
                {
                    fromAccount.Balance = fromAccount.Balance - amount;
                    await _transactionUnitOfWork.BankAccountRepository.UpdateAsync(fromAccount);
                    if (!_cashStorage.Withdraw(amount))
                    {
                        return TransactionResult.CashWithdrawalError;
                    }
                }
                else
                {
                    fromAccount.Balance -= amount;
                    await _transactionUnitOfWork.BankAccountRepository.UpdateAsync(fromAccount);
                    if (_cashStorage.Withdraw(amount))
                    {
                        await _transactionUnitOfWork.SaveChangesAsync();
                    }
                    else
                    {
                        await _transactionUnitOfWork.CancelTransactionAsync();
                    }
                }

                return TransactionResult.Success;
            }
            catch (Exception ex)
            {

                return new TransactionResult(TransactionResultCodes.Error, ex.Message);
            }
        }
    }
}
