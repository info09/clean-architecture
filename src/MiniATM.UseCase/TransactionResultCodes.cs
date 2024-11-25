namespace MiniATM.UseCase
{
    public enum TransactionResultCodes
    {
        Success,
        SourceNotFound,
        DestinationNotFound,
        BalanceTooLow,
        CashNotAvailable, // not enough cash in storage
        CashWithdrawalError, // customer account's balance has been debited, but failed to take cash from storage 
        Error
    }
}
