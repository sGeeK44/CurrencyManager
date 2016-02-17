using System;

namespace CurrencyManager.UnitTest
{
    public class ExchangeRateStub : IExchangeRate
    {
        public string ComeFromCurrency { get; set; }
        public string GoToCurrency { get; set; }
        public double Rate { get; set; }
        public double InvertRate { get; set; }
        public bool IsManagedInitialeCurrencyResult { get; set; }
        public bool IsManagedTargetCurrencyResult { get; set; }

        public double Change(double valueToChange)
        {
            throw new NotImplementedException();
        }

        public double ChangeInvert(double valueToChange)
        {
            throw new NotImplementedException();
        }

        public bool IsManagedInitialeCurrency(string initialCurrency)
        {
            return IsManagedInitialeCurrencyResult;
        }

        public bool IsManagedTargetCurrency(string targetCurrency)
        {
            return IsManagedTargetCurrencyResult;
        }
    }
}
