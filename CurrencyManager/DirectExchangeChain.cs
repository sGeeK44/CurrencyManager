using System;

namespace CurrencyManager
{
    /// <summary>
    /// Represent a change without need intermediate change
    /// </summary>
    public class DirectExchange : IExchangeChain
    {
        private IExchangeCurrency _directExchangeCurrency;

        private DirectExchange() { }
        
        /// <summary>
        /// Create a new instance of DirectExchange
        /// </summary>
        /// <param name="directExchangeCurrency"></param>
        /// <returns></returns>
        public static DirectExchange Create(IExchangeCurrency directExchangeCurrency)
        {
            if (directExchangeCurrency == null) throw new ArgumentNullException("directExchangeCurrency");

            return new DirectExchange
            {
                _directExchangeCurrency = directExchangeCurrency
            };
        }

        /// <summary>
        /// Change money with current rate
        /// </summary>
        /// <param name="initialCurrency">Initial currency to change</param>
        /// <param name="targetCurrency">Target currency to expected</param>
        /// <param name="valueToChange">Amount of money to change</param>
        /// <returns>Changed money</returns>
        public double Change(string initialCurrency, string targetCurrency, int valueToChange)
        {
            return _directExchangeCurrency.Change(initialCurrency, targetCurrency, valueToChange);
        }

        /// <summary>
        /// Get number of intermediate change needed to complete change
        /// </summary>
        /// <param name="initialCurrency">Initial currency to change</param>
        /// <param name="targetCurrency">Target currency to expected</param>
        public int CountIntermediateChangeNeeded(string initialCurrency, string targetCurrency)
        {
            return 1;
        }
    }
}