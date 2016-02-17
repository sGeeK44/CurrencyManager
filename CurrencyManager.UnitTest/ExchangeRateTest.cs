using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyManager.UnitTest
{
    [TestClass]
    public class ExchangeRateTest
    {
        [TestMethod]
        public void Change_From550With1Dot2053Rate_ShouldReturn662Dot9150()
        {
            var exchangeRate = ExchangeRate.Create(null, null, 1.2053);
            
            var exchangeValue = exchangeRate.Change(550);

            Assert.AreEqual(662.9150, exchangeValue);
        }

        [TestMethod]
        public void InvertRate_WhenRateEqual0Dot9661_InvertRateShouldEqual1Dot0351()
        {
            var exchangeRate = ExchangeRate.Create(null, null, 0.9661);
            Assert.AreEqual(1.0351, exchangeRate.InvertRate);
        }

        [TestMethod]
        public void ChangeInvert_From662Dot9150With0Dot9661Rate_ShouldReturn686Dot1833()
        {
            var exchangeRate = ExchangeRate.Create(null, null, 0.9661);

            var exchangeValue = exchangeRate.ChangeInvert(662.9150);

            Assert.AreEqual(686.1833, exchangeValue);
        }
    }
}
