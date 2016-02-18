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
        [ExpectedException(typeof(NotSupportedException))]
        public void Change_ExchangeRatePresentButComeFromNotCorrespond_ShouldThrowNotSuportedException()
        {
            var bank = new Bank();
            var exchangeRate = new ExchangeCurrencyStub();
            bank.AddExchangeRate(exchangeRate);

            bank.Change(null, null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Change_ExchangeCurrencyPresentCanNotExchangeToTarget_ShouldThrowNotSuportedException()
        {
            var bank = new Bank();
            var exchangeRate = new ExchangeCurrencyStub { CanChangeFromResult = false };
            bank.AddExchangeRate(exchangeRate);

            bank.Change(null, null, 0);
        }

        [TestMethod]
        public void Change_ExchangeRatePresent_ShouldReturnExchangeRateResultRounded()
        {
            const int expectedChangeResult = 59033;
            var bank = new Bank();
            var exchangeRate = new ExchangeCurrencyStub
            {
                CanChangeResult = true,
                ChangeResult = 686.1833 * 86.0305
            };
            bank.AddExchangeRate(exchangeRate);

            var changeResult = bank.Change(null, null, 0);

            Assert.AreEqual(expectedChangeResult, changeResult);
        }
    }
}
