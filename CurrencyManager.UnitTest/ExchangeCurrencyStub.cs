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
        public bool CanOnlyMakeIntermediateChangeResult { get; set; }

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

        public bool CanOnlyMakeIntermediateChange(string initialCurrency, string targetCurrency)
        {
            return CanOnlyMakeIntermediateChangeResult;
        }

        public static ExchangeCurrencyStub CreateExchangeWichManageOnlyBothCurrency()
        {
            return new ExchangeCurrencyStub { CanChangeResult = true, CanChangeFromResult = true };
        }

        public static ExchangeCurrencyStub CreateExchangeWichManageOnlyOneCurrency()
        {
            return new ExchangeCurrencyStub { CanOnlyMakeIntermediateChangeResult = true };
        }
    }
}
