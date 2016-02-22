using System.Collections.Generic;

namespace CurrencyManager
{
    /// <summary>
    /// Represent a change which need intermediate change
    /// </summary>
    public class ThroughExchange : ExchangeBase
    {
        private IExchangeCurrency _throughExchange;
        private IExchangeChain _nextExchange;
        private string _initialCurrency;
        private string _intermediateCurrency;

        /// <summary>
        /// Change money with current rate
        /// </summary>
        /// <param name="valueToChange">Amount of money to change</param>
        /// <returns>Changed money</returns>
        public override double Change(double valueToChange)
        {
            var intermediateChange = _throughExchange.Change(_initialCurrency, _intermediateCurrency, valueToChange);
            return _nextExchange.Change(intermediateChange);
        }

        /// <summary>
        /// Get number of intermediate change needed to complete change
        /// </summary>
        public override int CountPartInvolve { get { return 1 + _nextExchange.CountPartInvolve; } }
        
        /// <summary>
        /// Create a new instance of ThroughExchange
        /// </summary>
        /// <param name="initialCurrency">Initial currency to change</param>
        /// <param name="targetCurrency">Target currency to expected</param>
        /// <param name="throughExchange"></param>
        /// <param name="availableExchangeExcludeCurrent">All avalaible exchange currency to chain with current</param>
        /// <returns>Return new Direct Exchange</returns>
        public static IExchangeChain Create(IExchangeCurrency throughExchange, string initialCurrency, string targetCurrency, IList<IExchangeCurrency> availableExchangeExcludeCurrent)
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

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("{0};{1}", _initialCurrency, _nextExchange);
        }
    }
}