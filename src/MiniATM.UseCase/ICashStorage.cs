namespace MiniATM.UseCase
{
    public interface ICashStorage
    {
        bool IsCashAmountAvailable(double amount);
        bool Withdraw(double amount);
    }
}
