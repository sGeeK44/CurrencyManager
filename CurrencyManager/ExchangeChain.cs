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
        /// <param name="valueToChange">Amount of money to change</param>
        /// <returns>Changed money</returns>
        double Change(double valueToChange);

        /// <summary>
        /// Get number of intermediate change needed to complete change
        /// </summary>
        int CountIntermediateChangeNeeded();
    }
}