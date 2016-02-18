using System.Collections.Generic;

namespace CurrencyManager
{
    public class ThroughExchange : IExchangeChain
    {
        private IExchangeCurrency _throughExchange;
        private IExchangeChain _nextExchange;
        private string _initialCurrency;
        private string _intermediateCurrency;

        public double Change(double valueToChange)
        {
            var intermediateChange = _throughExchange.Change(_initialCurrency, _intermediateCurrency, valueToChange);
            return _nextExchange.Change(intermediateChange);
        }

        public int CountIntermediateChangeNeeded()
        {
            return 1 + _nextExchange.CountIntermediateChangeNeeded();
        }

        internal static IExchangeChain Create(IExchangeCurrency throughExchange, string initialCurrency, string targetCurrency, List<IExchangeCurrency> availableExchangeExcludeCurrent)
        {
            string changeTo;
            if (!throughExchange.CanChangeFrom(initialCurrency, out changeTo)) return null;

            var factory = new ExchangeChainFactory(availableExchangeExcludeCurrent);
            var nextExchange = factory.Create(changeTo, targetCurrency);
            if (nextExchange == null) return null;

            return new ThroughExchange
            {
                _initialCurrency = initialCurrency,
                _intermediateCurrency = changeTo,
                _throughExchange = throughExchange,
                _nextExchange = nextExchange
            };
        }
    }
}