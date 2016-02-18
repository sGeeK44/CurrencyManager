using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyManager
{
    /// <summary>
    /// Provide methods to Change money
    /// </summary>
    public class Bank
    {
        private readonly List<IExchangeCurrency> _availableExchangeCurrencyList = new List<IExchangeCurrency>();
        
        /// <summary>
        /// Add a exchange currency on available exchange currency pool
        /// </summary>
        /// <param name="exchangeCurrency">New exchange currency</param>
        public void AddExchangeRate(IExchangeCurrency exchangeCurrency)
        {
            if (_availableExchangeCurrencyList.Contains(exchangeCurrency)) return;

            _availableExchangeCurrencyList.Add(exchangeCurrency);
        }

        /// <summary>
        /// Get all available exchange currency
        /// </summary>
        public IList<IExchangeCurrency> AvailableExchangeCurrency { get { return _availableExchangeCurrencyList; } }

        /// <summary>
        /// Change money with current rate
        /// </summary>
        /// <param name="initialCurrency">Name of initial currency of a change</param>
        /// <param name="toCurrency">Name target currency of a change</param>
        /// <param name="amount">Amount of money to change</param>
        /// <returns>Changed money</returns>
        public int Change(string initialCurrency, string toCurrency, int amount)
        {
            var exchangeCurrency = GetExchangeRate(initialCurrency, toCurrency);
            if (exchangeCurrency == null) throw new NotSupportedException(string.Format("Bank can not change from {0} to {1}. None exchange currency available.", initialCurrency, toCurrency));

            return (int)Math.Round(exchangeCurrency.Change(initialCurrency, toCurrency, amount));
        }

        private IExchangeCurrency GetExchangeRate(string initialCurrency, string targetCurrency)
        {
            return AvailableExchangeCurrency.FirstOrDefault(_ => _.CanChange(initialCurrency, targetCurrency));
        }
    }
}
