using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyManager.UnitTest
{
    [TestClass]
    public class BankTest
    {
        [TestMethod]
        public void ConstructorBank_AvailableExchangeRate_ShouldBeNotNull()
        {
            var bank = new Bank();

            Assert.IsNotNull(bank.AvailableExchangeCurrency);
        }

        [TestMethod]
        public void ConstructorBank_AvailableExchangeRate_ShouldHaveZero()
        {
            var bank = new Bank();

            Assert.AreEqual(0, bank.AvailableExchangeCurrency.Count);
        }

        [TestMethod]
        public void AddExchangeRate_NewExchangeRate_ShouldInsertExchangeRate()
        {
            var bank = new Bank();
            
            bank.AddExchangeRate(new ExchangeCurrencyStub());

            Assert.AreEqual(1, bank.AvailableExchangeCurrency.Count);
        }

        [TestMethod]
        public void AddExchangeRate_ExistingExchangeRate_ShouldIgnore()
        {
            var bank = new Bank();
            var exchangeRate = new ExchangeCurrencyStub();
            bank.AddExchangeRate(exchangeRate);
            bank.AddExchangeRate(exchangeRate);

            Assert.AreEqual(1, bank.AvailableExchangeCurrency.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Change_ExchangeRateNotPresent_ShouldThrowNotSuportedException()
        {
            var bank = new Bank();
            bank.Change("", "", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Change_ExchangeRatePresentButComeFromNotCorrespond_ShouldThrowNotSuportedException()
        {
            var bank = new Bank();
            var exchangeRate = new ExchangeCurrencyStub();
            bank.AddExchangeRate(exchangeRate);

            bank.Change(null, null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Change_ExchangeCurrencyPresentCanNotExchangeToTarget_ShouldThrowNotSuportedException()
        {
            var bank = new Bank();
            var exchangeRate = new ExchangeCurrencyStub { CanChangeFromResult = false };
            bank.AddExchangeRate(exchangeRate);

            bank.Change(null, null, 0);
        }

        [TestMethod]
        public void Change_ExchangeRateChainExist_ShouldReturnExchangeRateResultRounded()
        {
            const int expectedChangeResult = 59033;
            var bank = new Bank();
            bank.AddExchangeRate(ExchangeCurrency.Create("AUD", "CHF", 0.9661));
            bank.AddExchangeRate(ExchangeCurrency.Create("JPY", "KRW", 13.1151));
            bank.AddExchangeRate(ExchangeCurrency.Create("EUR", "CHF", 1.2053));
            bank.AddExchangeRate(ExchangeCurrency.Create("AUD", "JPY", 86.0305));
            bank.AddExchangeRate(ExchangeCurrency.Create("EUR", "USD", 1.2989));
            bank.AddExchangeRate(ExchangeCurrency.Create("JPY", "INR", 0.6571));

            var changeResult = bank.Change("EUR", "JPY", 550);

            Assert.AreEqual(expectedChangeResult, changeResult);
        }
    }
}
