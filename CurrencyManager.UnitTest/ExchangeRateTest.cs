using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyManager.UnitTest
{
    [TestClass]
    public class ExchangeRateTest
    {
        [TestMethod]
        public void Create_With1Dot2053Rate_RateShouldBeInitTo1Dot2053()
        {
            const double expectedRate = 1.2053;
            var exchangeRate = ExchangeRate.Create(expectedRate);

            Assert.AreEqual(expectedRate, exchangeRate.Rate);
        }

        [TestMethod]
        public void Create_With1Dot2053Rate_InvertRateShouldEqual1Dot0351()
        {
            var exchangeRate = ExchangeRate.Create(0.9661);
            Assert.AreEqual(1.0351, exchangeRate.InvertRate);
        }

        [TestMethod]
        public void Change_From550With1Dot2053Rate_ShouldReturn662Dot9150()
        {
            var exchangeRate = ExchangeRate.Create(1.2053);
            
            var exchangeValue = exchangeRate.Change(550);

            Assert.AreEqual(662.9150, exchangeValue);
        }

        [TestMethod]
        public void ChangeInvert_From662Dot9150With0Dot9661Rate_ShouldReturn686Dot1833()
        {
            var exchangeRate = ExchangeRate.Create(0.9661);

            var exchangeValue = exchangeRate.ChangeBack(662.9150);

            Assert.AreEqual(686.1833, exchangeValue);
        }
    }
}
