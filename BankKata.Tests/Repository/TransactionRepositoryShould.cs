using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankKata.Tests.Repository
{
	[TestClass]
	public class TransactionRepositoryShould
	{
		private const string Today = "12/05/2015";
		private Mock<BankKata.Clock> _clock;
		private TransactionRepository _transactionRepository;

		[TestInitialize]
		public void TestInitialize()
		{
			_clock = new Mock<BankKata.Clock>();
			_transactionRepository = new TransactionRepository(_clock.Object);
			_clock.Setup(c => c.TodayAsString()).Returns(Today);
		}


		[TestMethod]
		public void CreateAndStoreADepositTransaction()
		{
			_transactionRepository.AddDeposit(100);
			var transactions = _transactionRepository.AllTransactions();

			Assert.AreEqual(1, transactions.Count);
			Assert.AreEqual(transactions[0], GetTransaction(Today, 100));
		}

		[TestMethod]
		public void CreateAndStoreAWithdrawalTransaction()
		{
			_transactionRepository.AddWithdrawal(100);
			var transactions = _transactionRepository.AllTransactions();

			Assert.AreEqual(1, transactions.Count);
			Assert.AreEqual(transactions[0], GetTransaction(Today, -100));
		}

		private static Transaction GetTransaction(string date, int amount)
		{
			return new Transaction(date, amount);
		}
	}
}