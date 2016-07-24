using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BankKata
{
	public class StatementPrinter
	{
		private const string StatementHeader = "DATE | AMOUNT | BALANCE";
		private readonly Console _console;

		public StatementPrinter(Console console)
		{
			_console = console;
		}

		public virtual void Print(List<Transaction> transactions)
		{
			_console.WriteLine(StatementHeader);
			var runningBalance = new RunningBalance {Balance = 0};
			transactions
				.Select(t => StatementLine(t, runningBalance))
				.Reverse()
				.ToList()
				.ForEach(x => _console.WriteLine(x));
		}

		private static string StatementLine(Transaction transaction, RunningBalance runningBalance)
		{
			runningBalance.Balance += transaction.Amount();
			return transaction.Date()
			       + " | "
			       + ToDecimalFormat(transaction.Amount())
			       + " | "
			       + ToDecimalFormat(runningBalance.Balance);
		}

		private static string ToDecimalFormat(int amount)
		{
			decimal decimalAmount = amount;
			return decimalAmount.ToString("F", CultureInfo.InvariantCulture.NumberFormat);
		}

		public class RunningBalance
		{
			public int Balance { get; set; }
		}
	}
}