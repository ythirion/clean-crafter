namespace SRP;

public interface ITransactionRepository
{
    void Add(Transaction transaction);
    Transaction[] All();
}