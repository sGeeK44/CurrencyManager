using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyManager
{
    public class ExchangeChainFactory
    {
        public ExchangeChainFactory(IList<IExchangeCurrency> availableExchangeCurrency)
        {
            if (availableExchangeCurrency == null || availableExchangeCurrency.Count == 0) throw new ArgumentNullException("availableExchangeCurrency");

            AvailableExchangeCurrency = availableExchangeCurrency;
        }

        public IList<IExchangeCurrency> AvailableExchangeCurrency { get; private set; }

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