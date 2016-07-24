using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankKata.Tests.Clock
{
	[TestClass]
	public class ClockShould
	{
		[TestMethod]
		public void ReturnTodaysDateInddMMyyyyFormat()
		{
			var clock = new TestableClock();

			var date = clock.TodayAsString();

			Assert.AreEqual("24/04/2015", date);
		}

		public class TestableClock : BankKata.Clock
		{
			protected override DateTime Today()
			{
				return new DateTime(2015,4,24);
			}
		}
	}
}