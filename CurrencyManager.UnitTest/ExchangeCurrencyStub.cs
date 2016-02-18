namespace CurrencyManager.UnitTest
{
    public class ExchangeCurrencyStub : IExchangeCurrency
    {
        public string InitialCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public IExchangeRate Rate { get; set; }
        public double ChangeResult { get; set; }
        public bool CanChangeResult { get; set; }
        public string CanChangeFromOutParam { get; set; }
        public bool CanChangeFromResult { get; set; }

        public double Change(string initialCurrency, string targetCurrency, double valueToChange)
        {
            return ChangeResult;
        }

        public bool CanChange(string initialCurrency, string targetCurrency)
        {
            return CanChangeResult;
        }

        public bool CanChangeFrom(string currencyToChange, out string changeToCurrency)
        {
            changeToCurrency = CanChangeFromOutParam;
            return CanChangeFromResult;
        }

    }
}
