using System;
using Moq;
using Xunit;

namespace SRP.Tests;

public class AccountServiceShould
{
    private const int PositiveAmount = 100;
    private const int NegativeAmount = -PositiveAmount;

    private readonly DateOnly _today = new(2017, 9, 6);

    private readonly Transaction[] _transactions =
    {
        new(new DateOnly(2014, 4, 1), 1000),
        new(new DateOnly(2014, 4, 2), -100),
        new(new DateOnly(2014, 4, 10), 500)
    };

    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
    private readonly Mock<IClock> _clockMock;
    private readonly Mock<IConsole> _consoleMock;

    private readonly AccountService _accountService;

    public AccountServiceShould()
    {
        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        _clockMock = new Mock<IClock>();
        _clockMock.Setup(c => c.Today()).Returns(_today);
        _consoleMock = new Mock<IConsole>();

        _accountService = new AccountService(
            _transactionRepositoryMock.Object,
            _clockMock.Object,
            _consoleMock.Object);
    }

    [Fact]
    public void Deposit_Amount_Into_The_Account()
    {
        _accountService.Deposit(PositiveAmount);
        AssertTransactionHasBeenCreatedInRepository(PositiveAmount);
    }

    [Fact]
    public void Withdraw_Amount_From_The_Account()
    {
        _accountService.Withdraw(PositiveAmount);
        AssertTransactionHasBeenCreatedInRepository(NegativeAmount);
    }

    [Fact]
    public void Print_Statement()
    {
        _transactionRepositoryMock
            .Setup(_ => _.All())
            .Returns(_transactions);

        _accountService.PrintStatement();

        _consoleMock.Verify(c => c.WriteLine("DATE | AMOUNT | BALANCE"));
        _consoleMock.Verify(c => c.WriteLine("10/04/2014 | 500.00 | 1400.00"));
        _consoleMock.Verify(c => c.WriteLine("02/04/2014 | -100.00 | 900.00"));
        _consoleMock.Verify(c => c.WriteLine("01/04/2014 | 1000.00 | 1000.00"));
    }

    private void AssertTransactionHasBeenCreatedInRepository(int amount)
    {
        _transactionRepositoryMock
            .Verify(repo =>
                    repo.Add(It.Is<Transaction>(transaction => transaction == new Transaction(_today, amount))),
                Times.Once()
            );
    }
}