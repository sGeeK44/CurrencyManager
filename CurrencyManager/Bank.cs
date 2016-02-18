using System;
using System.Collections.Generic;

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
        /// <param name="targetCurrency">Name target currency of a change</param>
        /// <param name="amount">Amount of money to change</param>
        /// <returns>Changed money</returns>
        public int Change(string initialCurrency, string targetCurrency, int amount)
        {
            if (string.IsNullOrEmpty(initialCurrency)) throw new ArgumentNullException("initialCurrency");
            if (string.IsNullOrEmpty(targetCurrency)) throw new ArgumentNullException("targetCurrency");
            if (AvailableExchangeCurrency.Count == 0) throw new NotSupportedException(string.Format("Bank can not change from {0} to {1}. None exchange currency available.", initialCurrency, targetCurrency));

            var factory = new ExchangeChainFactory(AvailableExchangeCurrency);
            var exchangeChain = factory.Create(initialCurrency, targetCurrency);

            if (exchangeChain == null) throw new NotSupportedException(string.Format("Bank can not change from {0} to {1}. None exchange currency corresponds to request change.", initialCurrency, targetCurrency));
            
            return (int)Math.Round(exchangeChain.Change(amount));
        }
    }
}
