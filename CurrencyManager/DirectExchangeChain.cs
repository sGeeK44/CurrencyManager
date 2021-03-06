﻿using System;

namespace CurrencyManager
{
    /// <summary>
    /// Represent a change without need intermediate change
    /// </summary>
    public class DirectExchange : ExchangeBase
    {
        private IExchangeCurrency _directExchangeCurrency;
        private string _initialCurrency;
        private string _targetCurrency;

        private DirectExchange() { }

        /// <summary>
        /// Change money with current rate
        /// </summary>
        /// <param name="valueToChange">Amount of money to change</param>
        /// <returns>Changed money</returns>
        public override double Change(double valueToChange)
        {
            return _directExchangeCurrency.Change(_initialCurrency, _targetCurrency, valueToChange);
        }

        /// <summary>
        /// Get number of intermediate change needed to complete change
        /// </summary>
        public override int CountPartInvolve { get { return 1; } }

        /// <summary>
        /// Create a new instance of DirectExchange
        /// </summary>
        /// <param name="initialCurrency">Initial currency to change</param>
        /// <param name="targetCurrency">Target currency to expected</param>
        /// <param name="directExchangeCurrency"></param>
        /// <returns>Return new Direct Exchange</returns>
        public static IExchangeChain Create(IExchangeCurrency directExchangeCurrency, string initialCurrency, string targetCurrency)
        {
            if (directExchangeCurrency == null) throw new ArgumentNullException("directExchangeCurrency");

            return new DirectExchange
            {
                _initialCurrency = initialCurrency,
                _targetCurrency = targetCurrency,
                _directExchangeCurrency = directExchangeCurrency
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
            return string.Format("{0};{1}", _initialCurrency, _targetCurrency);
        }
    }
}