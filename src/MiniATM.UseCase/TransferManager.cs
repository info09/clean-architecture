using MiniATM.UseCase.UnitOfWork;

namespace MiniATM.UseCase
{
    public class TransferManager : ITransferManager
    {
        private readonly ITransactionUnitOfWork _transactionUnitOfWork;

        public TransferManager(ITransactionUnitOfWork transactionUnitOfWork)
        {
            _transactionUnitOfWork = transactionUnitOfWork;
        }

        public async Task<TransactionResult> TransferAsync(string fromAccountId, string toAccountId, double amount)
        {
            try
            {
                await _transactionUnitOfWork.BeginTransactionAsync();
                var fromAccount = await _transactionUnitOfWork.BankAccountRepository.FindByIdAsync(fromAccountId);
                if (fromAccount == null || fromAccount.IsLocked)
                    return TransactionResult.SourceNotFound;

                var remainingAmount = fromAccount.Balance - amount;
                if (remainingAmount < fromAccount.MinimumRequiredAmount)
                    return TransactionResult.BalanceTooLow;

                var toAccount = await _transactionUnitOfWork.BankAccountRepository.FindByIdAsync(fromAccountId);
                if (toAccount == null || toAccount.IsLocked)
                    return TransactionResult.DestinationNotFound;

                fromAccount.Balance = fromAccount.Balance - amount;
                await _transactionUnitOfWork.BankAccountRepository.UpdateAsync(fromAccount);

                var now = DateTime.UtcNow;
                await _transactionUnitOfWork.TransactionRepository.Add(new Entities.Transaction()
                {
                    Id = Guid.NewGuid(),
                    Amount = amount,
                    AccountId = fromAccountId,
                    DateTimeUTC = now,
                    TransactionTypes = Entities.TransactionTypes.Deposit,
                    Notes = $"Transfer to {fromAccountId}"
                });

                toAccount.Balance = toAccount.Balance + amount;
                await _transactionUnitOfWork.BankAccountRepository.UpdateAsync(toAccount);

                await _transactionUnitOfWork.TransactionRepository.Add(new Entities.Transaction()
                {
                    Id = Guid.NewGuid(),
                    Amount = amount,
                    AccountId = toAccountId,
                    DateTimeUTC = now,
                    TransactionTypes = Entities.TransactionTypes.Deposit,
                    Notes = $"Transfer from {toAccountId}"
                });

                await _transactionUnitOfWork.SaveChangesAsync();
                return TransactionResult.Success;
            }
            catch (Exception ex)
            {
                return new TransactionResult(TransactionResultCodes.Error, ex.Message);
            }
        }
    }
}
