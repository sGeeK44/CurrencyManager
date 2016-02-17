using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyManager.UnitTest
{
    /// <summary>
    /// Summary description for CurrencyExchangerTest
    /// </summary>
    [TestClass]
    public class ExchangeRateTest
    {
        [TestMethod]
        public void Change_From550With1Dot2053Rate_ShouldReturn662Dot9150()
        {
            var bank = new ExchangeRate(1.2053);
            
            var exchangeValue = bank.Change(550);

            Assert.AreEqual(662.9150, exchangeValue);
        }
    }
}
