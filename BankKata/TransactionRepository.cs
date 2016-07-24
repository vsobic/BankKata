using System.Collections.Generic;

namespace BankKata
{
	public class TransactionRepository
	{
		private readonly Clock _clock;
		private readonly List<Transaction> _transactions = new List<Transaction>();

		public TransactionRepository(Clock clock)
		{
			_clock = clock;
		}

		public virtual void AddDeposit(int amount)
		{
			var deposit = new Transaction(_clock.TodayAsString(), amount);
			_transactions.Add(deposit);
		}

		public virtual void AddWithdrawal(int amount)
		{
			var withdrawal = new Transaction(_clock.TodayAsString(), -amount);
			_transactions.Add(withdrawal);
		}

		public virtual List<Transaction> AllTransactions()
		{
			return new List<Transaction>(_transactions.AsReadOnly());
		}
	}
}