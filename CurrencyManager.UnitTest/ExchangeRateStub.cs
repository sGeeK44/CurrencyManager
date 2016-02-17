using System;

namespace CurrencyManager.UnitTest
{
    public class ExchangeRateStub : IExchangeRate
    {
        public string ComeFromCurrency { get; set; }
        public string GoToCurrency { get; set; }
        public double Rate { get; set; }
        public double InvertRate { get; set; }
        public double Change(double valueToChange)
        {
            throw new NotImplementedException();
        }

        public double ChangeInvert(double valueToChange)
        {
            throw new NotImplementedException();
        }

        public bool CanChange(string currency)
        {
            throw new NotImplementedException();
        }

        public bool CanChangeBack(string currency)
        {
            throw new NotImplementedException();
        }
    }
}
