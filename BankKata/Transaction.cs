namespace BankKata
{
	public class Transaction
	{
		private readonly int _amount;
		private readonly string _date;

		public Transaction(string date, int amount)
		{
			_date = date;
			_amount = amount;
		}

		protected bool Equals(Transaction other)
		{
			return _amount == other._amount && string.Equals(_date, other._date);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((Transaction) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (_amount*397) ^ (_date?.GetHashCode() ?? 0);
			}
		}

		public string Date()
		{
			return _date;
		}

		public int Amount()
		{
			return _amount;
		}
	}
}