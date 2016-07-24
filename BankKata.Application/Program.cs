namespace BankKata.Application
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var clock = new Clock();
			var transactionRepository = new TransactionRepository(clock);
			var console = new Console();
			var statementPrinter = new StatementPrinter(console);
			var account = new Account(transactionRepository, statementPrinter);

			account.Deposit(1000);
			account.Withdraw(100);
			account.Deposit(500);

			account.PrintStatement();
		}
	}
}