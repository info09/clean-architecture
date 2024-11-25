namespace MiniATM.UseCase
{
    public interface ITransferManager
    {
        Task<TransactionResult> TransferAsync(string fromAccountId, string toAccountId, double amount);
    }
}
