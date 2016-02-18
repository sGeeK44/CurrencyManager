namespace CurrencyManager
{
    /// <summary>
    /// Expose methods to change money
    /// </summary>
    public interface IExchangeCurrency
    {
        /// <summary>
        /// Get name of initial currency of a change
        /// </summary>
        string InitialCurrency { get; }

        /// <summary>
        /// Get name target currency of a change
        /// </summary>
        string TargetCurrency { get; }

        /// <summary>
        /// Get current rate used to change money from initial currency to target
        /// </summary>
        IExchangeRate Rate { get; }

        /// <summary>
        /// Change money with current rate
        /// </summary>
        /// <param name="initialCurrency">Initial currency to change</param>
        /// <param name="targetCurrency">Target currency to expected</param>
        /// <param name="valueToChange">Amount of money to change</param>
        /// <returns>Changed money</returns>
        double Change(string initialCurrency, string targetCurrency, double valueToChange);

        /// <summary>
        /// Indicate if current exchange rate can change initial currency to target
        /// </summary>
        /// <param name="initialCurrency">Initial currency to change</param>
        /// <param name="targetCurrency">Target currency to expected</param>
        /// <returns>True if it can, false else</returns>
        bool CanChange(string initialCurrency, string targetCurrency);

        /// <summary>
        /// Indicate if current exchange rate can change initial currency to another
        /// </summary>
        /// <param name="currencyToChange">Currency to change to another</param>
        /// <param name="changeToCurrency">Currency after change if current can change, else null</param>
        /// <returns>True if it can, false else</returns>
        bool CanChangeFrom(string currencyToChange, out string changeToCurrency);
    }
}