using System;

namespace CurrencyManager.UnitTest
{
    public class ExchangeRateStub : IExchangeRate
    {
        public double Rate { get; set; }
        public double InvertRate { get; set; }
        public double ChangeResult { get; set; }
        public double ChangeBackResult { get; set; }

        public double Change(double valueToChange)
        {
            return ChangeResult;
        }

        public double ChangeBack(double valueToChange)
        {
            return ChangeBackResult;
        }
    }
}
