using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyManager
{
    /// <summary>
    /// Expose method to build ExchangeChain
    /// </summary>
    public class ExchangeChainFactory
    {
        /// <summary>
        /// Create a new factory with specified available exchange currency
        /// </summary>
        /// <param name="availableExchangeCurrency">All available exchange currency can be use to build chain</param>
        public ExchangeChainFactory(IList<IExchangeCurrency> availableExchangeCurrency)
        {
            if (availableExchangeCurrency == null || availableExchangeCurrency.Count == 0) throw new ArgumentNullException("availableExchangeCurrency");

            AvailableExchangeCurrency = availableExchangeCurrency;
        }

        /// <summary>
        /// Get all available exchange currency that can be use to build a chain for a specified request
        /// </summary>
        public IList<IExchangeCurrency> AvailableExchangeCurrency { get; private set; }

        /// <summary>
        /// Create a new Chain to be able to change specified currencies
        /// </summary>
        /// <param name="initialCurrency">Initial currency to change</param>
        /// <param name="targetCurrency">Target currency to change</param>
        /// <returns>Chain builded</returns>
        public IExchangeChain Create(string initialCurrency, string targetCurrency)
        {
            if (string.IsNullOrEmpty(initialCurrency)) throw new ArgumentNullException("initialCurrency");
            if (string.IsNullOrEmpty(targetCurrency)) throw new ArgumentNullException("targetCurrency");

            var directExchange = GetExchangeWhoManagedBothCurrency(initialCurrency, targetCurrency);
            if (directExchange != null) return DirectExchange.Create(directExchange, initialCurrency, targetCurrency);

            var throughExchangeList = GetExchangeWhoManagedOnlyOneCurrency(initialCurrency, targetCurrency);
            if (throughExchangeList.Count == 0) return null;

            IExchangeChain result = null;
            foreach (var throughExchange in throughExchangeList)
            {
                var availableExchangeExcludeCurrent = new List<IExchangeCurrency>(AvailableExchangeCurrency);
                availableExchangeExcludeCurrent.Remove(throughExchange);

                var newChain = ThroughExchange.Create(throughExchange, initialCurrency, targetCurrency, availableExchangeExcludeCurrent);
                if (result == null || newChain != null && newChain.CountIntermediateChangeNeeded() < result.CountIntermediateChangeNeeded()) result = newChain;
            }
            return result;
        }

        private IExchangeCurrency GetExchangeWhoManagedBothCurrency(string initialCurrency, string targetCurrency)
        {
            return AvailableExchangeCurrency.FirstOrDefault(_ => _.CanChange(initialCurrency, targetCurrency));
        }

        private IList<IExchangeCurrency> GetExchangeWhoManagedOnlyOneCurrency(string initialCurrency, string targetCurrency)
        {
            return AvailableExchangeCurrency.Where(_ => _.CanOnlyMakeIntermediateChange(initialCurrency, targetCurrency))
                                            .ToList();
        }
    }
}