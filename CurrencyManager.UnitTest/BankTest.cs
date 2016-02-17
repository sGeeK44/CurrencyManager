using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyManager.UnitTest
{
    [TestClass]
    public class BankTest
    {
        private const string CURRENCY_NAME_1 = "CUR1";
        private const string CURRENCY_NAME_2 = "CUR2";
        private const string CURRENCY_NAME_3 = "CUR3";

        [TestMethod]
        public void ConstructorBank_AvailableExchangeRate_ShouldBeNotNull()
        {
            var bank = new Bank();

            Assert.IsNotNull(bank.AvailableExchangeRate);
        }

        [TestMethod]
        public void ConstructorBank_AvailableExchangeRate_ShouldHaveZero()
        {
            var bank = new Bank();

            Assert.AreEqual(0, bank.AvailableExchangeRate.Count);
        }

        [TestMethod]
        public void AddExchangeRate_NewExchangeRate_ShouldInsertExchangeRate()
        {
            var bank = new Bank();
            
            bank.AddExchangeRate(new ExchangeRateStub());

            Assert.AreEqual(1, bank.AvailableExchangeRate.Count);
        }

        [TestMethod]
        public void AddExchangeRate_ExistingExchangeRate_ShouldIgnore()
        {
            var bank = new Bank();
            var exchangeRate = new ExchangeRateStub();
            bank.AddExchangeRate(exchangeRate);
            bank.AddExchangeRate(exchangeRate);

            Assert.AreEqual(1, bank.AvailableExchangeRate.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Change_ExchangeRateNotPresent_ShouldThrowNotSuportedException()
        {
            var bank = new Bank();
            bank.Change("", "", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Change_ExchangeRatePresentButComeFromNotCorrespond_ShouldThrowNotSuportedException()
        {
            var bank = new Bank();
            var exchangeRate = new ExchangeRateStub();
            bank.AddExchangeRate(exchangeRate);

            bank.Change(null, null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Change_ExchangeRatePresentButGoToNotCorrespond_ShouldThrowNotSuportedException()
        {
            var bank = new Bank();
            var exchangeRate = new ExchangeRateStub { IsManagedInitialeCurrencyResult = true };
            bank.AddExchangeRate(exchangeRate);

            bank.Change(null, null, 0);
        }

        [TestMethod]
        public void Change_ExchangeRatePresent_ShouldReturnExchangeRateResultRounded()
        {
            const int expectedChangeResult = 59033;
            var bank = new Bank();
            var exchangeRate = new ExchangeRateStub
            {
                IsManagedInitialeCurrencyResult = true,
                IsManagedTargetCurrencyResult = true,
                ChangeResult = 686.1833 * 86.0305
            };
            bank.AddExchangeRate(exchangeRate);

            var changeResult = bank.Change(null, null, 0);

            Assert.AreEqual(expectedChangeResult, changeResult);
        }
    }
}
