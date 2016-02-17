using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyManager.UnitTest
{
    [TestClass]
    public class ExchangeRateTest
    {
        private const string CURRENCY_NAME = "CUR";
        private const string CURRENCY_NAME_1 = "CUR1";
        private const string CURRENCY_NAME_2 = "CUR2";
        private const string CURRENCY_NAME_3 = "CUR3";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNullFromCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeRate.Create(null, null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithEmptyFromCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeRate.Create(string.Empty, null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithOnlyWhiteSpaceFromCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeRate.Create(" ", null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNullToCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeRate.Create(CURRENCY_NAME, null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithEmptyToCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeRate.Create(CURRENCY_NAME, string.Empty, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithOnlyWhiteSpaceToCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeRate.Create(CURRENCY_NAME, " ", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_WithSameFromAndToCurrencyName_ShouldThrowArgumentException()
        {
            ExchangeRate.Create(CURRENCY_NAME, CURRENCY_NAME, 0);
        }

        [TestMethod]
        public void Change_From550With1Dot2053Rate_ShouldReturn662Dot9150()
        {
            var exchangeRate = CreateWithRate(1.2053);
            
            var exchangeValue = exchangeRate.Change(550);

            Assert.AreEqual(662.9150, exchangeValue);
        }

        [TestMethod]
        public void InvertRate_WhenRateEqual0Dot9661_InvertRateShouldEqual1Dot0351()
        {
            var exchangeRate = CreateWithRate(0.9661);
            Assert.AreEqual(1.0351, exchangeRate.InvertRate);
        }

        [TestMethod]
        public void ChangeInvert_From662Dot9150With0Dot9661Rate_ShouldReturn686Dot1833()
        {
            var exchangeRate = CreateWithRate(0.9661);

            var exchangeValue = exchangeRate.ChangeInvert(662.9150);

            Assert.AreEqual(686.1833, exchangeValue);
        }

        [TestMethod]
        public void Equals_WithNull_ShouldReturnFalse()
        {
            var exchangeRate1 = CreateWithRate(0);

            Assert.IsFalse(exchangeRate1.Equals(null));
        }

        [TestMethod]
        public void Equals_WithSameCurrencyFromAndTo_ShouldReturnTrue()
        {
            var exchangeRate1 = CreateWithRate(0);
            var exchangeRate2 = CreateWithRate(0);

            Assert.IsTrue(exchangeRate1.Equals(exchangeRate2));
            Assert.IsTrue(exchangeRate2.Equals(exchangeRate1));
        }

        [TestMethod]
        public void Equals_WithSameCurrencyFromButNotTo_ShouldReturnFalse()
        {
            var exchangeRate1 = ExchangeRate.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);
            var exchangeRate2 = ExchangeRate.Create(CURRENCY_NAME_1, CURRENCY_NAME_3, 0);

            Assert.IsFalse(exchangeRate1.Equals(exchangeRate2));
            Assert.IsFalse(exchangeRate2.Equals(exchangeRate1));
        }

        [TestMethod]
        public void Equals_WithSameCurrencyToButNotFrom_ShouldReturnFalse()
        {
            var exchangeRate1 = ExchangeRate.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);
            var exchangeRate2 = ExchangeRate.Create(CURRENCY_NAME_3, CURRENCY_NAME_2, 0);

            Assert.IsFalse(exchangeRate1.Equals(exchangeRate2));
            Assert.IsFalse(exchangeRate2.Equals(exchangeRate1));
        }

        [TestMethod]
        public void CanChangeFrom_WithNotManagedCurrency_ShouldReturnFalse()
        {
            var exchangeRate = ExchangeRate.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            Assert.IsFalse(exchangeRate.IsManagedInitialeCurrency(CURRENCY_NAME_2));
        }

        [TestMethod]
        public void CanChangeFrom_WithManagedCurrency_ShouldReturnTrue()
        {
            var exchangeRate = ExchangeRate.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            Assert.IsTrue(exchangeRate.IsManagedInitialeCurrency(CURRENCY_NAME_1));
        }

        [TestMethod]
        public void CanChangeTo_WithNotManagedCurrency_ShouldReturnFalse()
        {
            var exchangeRate = ExchangeRate.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            Assert.IsFalse(exchangeRate.IsManagedTargetCurrency(CURRENCY_NAME_1));
        }

        [TestMethod]
        public void CanChangeTo_WithManagedCurrency_ShouldReturnTrue()
        {
            var exchangeRate = ExchangeRate.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            Assert.IsTrue(exchangeRate.IsManagedTargetCurrency(CURRENCY_NAME_2));
        }

        private static IExchangeRate CreateWithRate(double rate)
        {
            return ExchangeRate.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, rate);
        }
    }
}
