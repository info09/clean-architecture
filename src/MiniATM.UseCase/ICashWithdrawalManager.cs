namespace MiniATM.UseCase
{
    public interface ICashWithdrawalManager
    {
        Task<TransactionResult> WithdrawAsync(string accountId, double amount);
    }
}
