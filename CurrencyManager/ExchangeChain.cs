namespace CurrencyManager
{
    /// <summary>
    /// Expose method to change money with several intermediate change
    /// </summary>
    public interface IExchangeChain
    {
        /// <summary>
        /// Change money with current rate
        /// </summary>
        /// <param name="initialCurrency">Initial currency to change</param>
        /// <param name="targetCurrency">Target currency to expected</param>
        /// <param name="valueToChange">Amount of money to change</param>
        /// <returns>Changed money</returns>
        double Change(string initialCurrency, string targetCurrency, int valueToChange);

        /// <summary>
        /// Get number of intermediate change needed to complete change
        /// </summary>
        /// <param name="initialCurrency">Initial currency to change</param>
        /// <param name="targetCurrency">Target currency to expected</param>
        int CountIntermediateChangeNeeded(string initialCurrency, string targetCurrency);
    }
}