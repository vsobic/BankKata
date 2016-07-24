using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankKata.Tests.Account
{
	[TestClass]
	public class AccountShould
	{
		private BankKata.Account _account;
		private Mock<BankKata.Clock> _clock;
		private Mock<Console> _console;
		private Mock<StatementPrinter> _statementPrinter;
		private Mock<TransactionRepository> _transactionRepository;

		[TestInitialize]
		public void TestInitialize()
		{
			_clock = new Mock<BankKata.Clock>();
			_console = new Mock<Console>();
			_transactionRepository = new Mock<TransactionRepository>(_clock.Object);
			_statementPrinter = new Mock<StatementPrinter>(_console.Object);
			_account = new BankKata.Account(_transactionRepository.Object, _statementPrinter.Object);
		}

		[TestMethod]
		public void StoreADepositTransaction()
		{
			_account.Deposit(100);
			_transactionRepository.Verify(r => r.AddDeposit(100));
		}

		[TestMethod]
		public void StoreAWithdrawalTransaction()
		{
			_account.Withdraw(100);
			_transactionRepository.Verify(r => r.AddWithdrawal(100));
		}

		[TestMethod]
		public void PrintAStatement()
		{
			var transactions = new List<Transaction> {new Transaction("12/05/2015", 100)};
			_transactionRepository.Setup(t => t.AllTransactions()).Returns(transactions);

			_account.PrintStatement();

			_statementPrinter.Verify(s => s.Print(transactions));
		}
	}
}