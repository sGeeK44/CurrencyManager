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
        private readonly List<IExchangeRate> _availableExchangeRateList = new List<IExchangeRate>();
        
        /// <summary>
        /// Add a exchange rate on available exchange rate pool
        /// </summary>
        /// <param name="exchangeRate">New exchange rate.</param>
        public void AddExchangeRate(IExchangeRate exchangeRate)
        {
            if (_availableExchangeRateList.Contains(exchangeRate)) return;

            _availableExchangeRateList.Add(exchangeRate);
        }

        /// <summary>
        /// Get all available exchange rate
        /// </summary>
        public IList<IExchangeRate> AvailableExchangeRate { get { return _availableExchangeRateList; } }

        /// <summary>
        /// Change money with current rate
        /// </summary>
        /// <param name="fromCurrency">Name of initial currency of a change</param>
        /// <param name="toCurrency">Name target currency of a change</param>
        /// <param name="amount">Amount of money to change</param>
        /// <returns>Changed money</returns>
        public int Change(string fromCurrency, string toCurrency, int amount)
        {
            var availableExchangeForInitial = GetAvailableExchangeRateForInitialCurrency(fromCurrency);
            if (availableExchangeForInitial.Count() == 0) throw new NotSupportedException(string.Format("None exchange rate are available to change this initial currency. Currency:{0}.", fromCurrency));

            var exchangeRate = GetExchangeRateForTargetCurrency(availableExchangeForInitial, toCurrency);
            if (exchangeRate == null) throw new NotSupportedException(string.Format("None exchange rate are available to change this target currency. Currency:{0}.", toCurrency));

            return (int) Math.Round(exchangeRate.Change(amount));
        }

        private IEnumerable<IExchangeRate> GetAvailableExchangeRateForInitialCurrency(string initialCurrency)
        {
            return AvailableExchangeRate.Where(_ => _.IsManagedInitialeCurrency(initialCurrency));
        }

        private IExchangeRate GetExchangeRateForTargetCurrency(IEnumerable<IExchangeRate> availableExchangeRate, string targetCurrency)
        {
            return availableExchangeRate.FirstOrDefault(_ => _.IsManagedTargetCurrency(targetCurrency));
        }
    }
}
