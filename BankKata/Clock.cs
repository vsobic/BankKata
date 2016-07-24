using System;
using System.Globalization;

namespace BankKata
{
	public class Clock
	{
		public virtual string TodayAsString()
		{
			return Today().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
		}

		protected virtual DateTime Today()
		{
			return DateTime.UtcNow;
		}
	}
}