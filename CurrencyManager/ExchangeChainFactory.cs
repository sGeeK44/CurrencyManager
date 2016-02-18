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
            if (directExchange != null) return DirectExchange.Create(directExchange);

            var intermediatePossibleExchange = GetExchangeWhoManagedOnlyOneCurrency(initialCurrency, targetCurrency);
            if (intermediatePossibleExchange.Count == 0) return null;
            
            return null;
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