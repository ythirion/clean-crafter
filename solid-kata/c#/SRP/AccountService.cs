namespace SRP;

public class AccountService
{
    private const string StatementHeader = "DATE | AMOUNT | BALANCE";
    private const string DateFormat = "dd/MM/yyyy";
    private const string AmountFormat = "#.00";

    private readonly ITransactionRepository _transactionRepository;
    private readonly IClock _clock;
    private readonly IConsole _console;

    public AccountService(ITransactionRepository transactionRepository, IClock clock, IConsole console)
    {
        _transactionRepository = transactionRepository;
        _clock = clock;
        _console = console;
    }

    public void Deposit(double amount) => _transactionRepository.Add(TransactionWith(amount));

    public void Withdraw(double amount) => _transactionRepository.Add(TransactionWith(-amount));

    public void PrintStatement()
    {
        PrintHeader();
        PrintTransactions();
    }

    private void PrintHeader() => PrintLine(StatementHeader);

    private void PrintTransactions()
    {
        var balance = 0d;
        _transactionRepository.All()
            .Select(transaction =>
            {
                balance += transaction.Amount;
                return StatementLine(transaction, balance);
            })
            .ToList()
            .ForEach(PrintLine);
    }


    private Transaction TransactionWith(double amount) => new(_clock.Today(), amount);


    private string StatementLine(Transaction transaction, double balance)
        => $"{FormatDate(transaction.Date)} | {FormatNumber(transaction.Amount)} | {FormatNumber(balance)}";

    private string FormatDate(DateOnly date) => date.ToString(DateFormat);

    private string FormatNumber(double amount) => amount.ToString(AmountFormat);

    private void PrintLine(string line)
        => _console.WriteLine(line);
}