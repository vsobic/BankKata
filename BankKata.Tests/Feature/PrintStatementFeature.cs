﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankKata.Tests.Feature
{
	[TestClass]
	public class PrintStatementFeature
	{
		private Mock<Console> _console;
		private BankKata.Account _account;
		private TransactionRepository _transactionRepository;
		private StatementPrinter _statementPrinter;
		private Mock<BankKata.Clock> _clock;

		[TestInitialize]
		public void TestInitialize()
		{
			_console = new Mock<Console>();
			_clock = new Mock<BankKata.Clock>();
			_transactionRepository = new TransactionRepository(_clock.Object);
			_statementPrinter = new StatementPrinter(_console.Object);
			_account =  new BankKata.Account(_transactionRepository, _statementPrinter);
		}

		[TestMethod]
		public void PrintStatementContainingAllTransactions()
		{
			var results = new Queue<string>(new[] { "01/04/2014", "02/04/2014", "10/04/2014" });
			_clock.Setup(c => c.TodayAsString()).Returns(() => results.Dequeue());
			var callOrder = "";
			_account.Deposit(1000);
			_account.Withdraw(100);
			_account.Deposit(500);
			_console.Setup(x => x.WriteLine("DATE | AMOUNT | BALANCE")).Callback(() => callOrder += "1");
			_console.Setup(x => x.WriteLine("10/04/2014 | 500.00 | 1400.00")).Callback(() => callOrder += "2");
			_console.Setup(x => x.WriteLine("02/04/2014 | -100.00 | 900.00")).Callback(() => callOrder += "3");
			_console.Setup(x => x.WriteLine("01/04/2014 | 1000.00 | 1000.00")).Callback(() => callOrder += "4");

			_account.PrintStatement();

			_console.Verify(c => c.WriteLine("DATE | AMOUNT | BALANCE"));
			_console.Verify(c => c.WriteLine("10/04/2014 | 500.00 | 1400.00"));
			_console.Verify(c => c.WriteLine("02/04/2014 | -100.00 | 900.00"));
			_console.Verify(c => c.WriteLine("01/04/2014 | 1000.00 | 1000.00"));
			Assert.AreEqual("1234", callOrder);
		}
	}
}