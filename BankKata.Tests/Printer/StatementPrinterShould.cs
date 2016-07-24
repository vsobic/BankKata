using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankKata.Tests.Printer
{
	[TestClass]
	public class StatementPrinterShould
	{
		private Mock<Console> _console;
		private StatementPrinter _statementPrinter;

		private List<Transaction> NoTransactions { get; } = new List<Transaction>();

		[TestInitialize]
		public void TestInitialize()
		{
			_console = new Mock<Console>();
			_statementPrinter = new StatementPrinter(_console.Object);
		}


		[TestMethod]
		public void AlwaysPrintTheHeader()
		{
			_statementPrinter.Print(NoTransactions);

			_console.Verify(c => c.WriteLine("DATE | AMOUNT | BALANCE"));
		}

		[TestMethod]
		public void PrintTransactionsInReverseChronologicalOrder()
		{
			var callOrder = "";
			_console.Setup(x => x.WriteLine("DATE | AMOUNT | BALANCE")).Callback(() => callOrder += "1");
			_console.Setup(x => x.WriteLine("10/04/2014 | 500.00 | 1400.00")).Callback(() => callOrder += "2");
			_console.Setup(x => x.WriteLine("02/04/2014 | -100.00 | 900.00")).Callback(() => callOrder += "3");
			_console.Setup(x => x.WriteLine("01/04/2014 | 1000.00 | 1000.00")).Callback(() => callOrder += "4");
			var transactions = TransactionsContaining(
				Deposit("01/04/2014", 1000),
				Withdrawal("02/04/2014", 100),
				Deposit("10/04/2014", 500)
				);

			_statementPrinter.Print(transactions);

			_console.Verify(c => c.WriteLine("DATE | AMOUNT | BALANCE"));
			_console.Verify(c => c.WriteLine("10/04/2014 | 500.00 | 1400.00"));
			_console.Verify(c => c.WriteLine("02/04/2014 | -100.00 | 900.00"));
			_console.Verify(c => c.WriteLine("01/04/2014 | 1000.00 | 1000.00"));
			Assert.AreEqual("1234", callOrder);
		}

		private static Transaction Withdrawal(string date, int amount)
		{
			return new Transaction(date, -amount);
		}

		private static Transaction Deposit(string date, int amount)
		{
			return new Transaction(date, amount);
		}

		private static List<Transaction> TransactionsContaining(params Transaction[] transactions)
		{
			return transactions.ToList();
		}
	}
}