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
        /// <returns>Chain builded if it exist, else null</returns>
        /// <remarks>Strategy to determine is:<Br/>
        /// First try to get a ExchangeRate without intermediary change<Br/>
        /// If it's not possible, then build all available chain and keep the fastest path<Br/>
        /// If no way null is returned.
        /// </remarks>
        public IExchangeChain Create(string initialCurrency, string targetCurrency)
        {
            if (string.IsNullOrEmpty(initialCurrency)) throw new ArgumentNullException("initialCurrency");
            if (string.IsNullOrEmpty(targetCurrency)) throw new ArgumentNullException("targetCurrency");

            var directExchange = GetFirstOrDefaultDirectExchange(initialCurrency, targetCurrency);
            if (directExchange != null) return DirectExchange.Create(directExchange, initialCurrency, targetCurrency);
            
            return GetFastestChain(initialCurrency, targetCurrency);
        }

        private IExchangeChain GetFastestChain(string initialCurrency, string targetCurrency)
        {
            var availableThroughExchangeList = GetExchangeWhoManagedOnlyOneCurrency(initialCurrency, targetCurrency);
            if (availableThroughExchangeList.Count == 0) return null;

            return availableThroughExchangeList.Select(_ => CreateNewChain(initialCurrency, targetCurrency, _))
                                               .Aggregate<IExchangeChain, IExchangeChain>(null, KeepFasterChain);
        }

        private IExchangeChain CreateNewChain(string initialCurrency, string targetCurrency, IExchangeCurrency potentialThroughExchange)
        {
            var availableExchangeExcludeCurrent = CloneAvailableExchangeExcludePotential(potentialThroughExchange);
            return ThroughExchange.Create(potentialThroughExchange, initialCurrency, targetCurrency, availableExchangeExcludeCurrent);
        }

        private static IExchangeChain KeepFasterChain(IExchangeChain currenFastestChain, IExchangeChain newChain)
        {
            currenFastestChain = currenFastestChain ?? newChain; // First chain available
            return newChain != null && newChain.IsFasterThan(currenFastestChain)
                 ? newChain
                 : currenFastestChain;
        }

        private List<IExchangeCurrency> CloneAvailableExchangeExcludePotential(IExchangeCurrency potential)
        {
            var availableExchangeExcludePotential = new List<IExchangeCurrency>(AvailableExchangeCurrency);
            availableExchangeExcludePotential.Remove(potential);
            return availableExchangeExcludePotential;
        }

        private IExchangeCurrency GetFirstOrDefaultDirectExchange(string initialCurrency, string targetCurrency)
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