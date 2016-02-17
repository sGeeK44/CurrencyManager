using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyManager.UnitTest
{
    [TestClass]
    public class ExchangeRateTest
    {
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
            ExchangeRate.Create("CUR", null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithEmptyToCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeRate.Create("CUR", string.Empty, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithOnlyWhiteSpaceToCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeRate.Create("CUR", " ", 0);
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
            var exchangeRate1 = ExchangeRate.Create("CUR1", "CUR2", 0);
            var exchangeRate2 = ExchangeRate.Create("CUR1", "CUR3", 0);

            Assert.IsFalse(exchangeRate1.Equals(exchangeRate2));
            Assert.IsFalse(exchangeRate2.Equals(exchangeRate1));
        }

        [TestMethod]
        public void Equals_WithSameCurrencyToButNotFrom_ShouldReturnFalse()
        {
            var exchangeRate1 = ExchangeRate.Create("CUR1", "CUR2", 0);
            var exchangeRate2 = ExchangeRate.Create("CUR3", "CUR2", 0);

            Assert.IsFalse(exchangeRate1.Equals(exchangeRate2));
            Assert.IsFalse(exchangeRate2.Equals(exchangeRate1));
        }

        private static IExchangeRate CreateWithRate(double rate)
        {
            return ExchangeRate.Create("CUR1", "CUR2", rate);
        }
    }
}
