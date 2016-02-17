namespace CurrencyManager
{
    /// <summary>
    /// Expose methods to change money with a specified rate
    /// </summary>
    public interface IExchangeRate
    {
        /// <summary>
        /// Get or set name of initial currency of a change
        /// </summary>
        string ComeFromCurrency { get; set; }

        /// <summary>
        /// Get or set name target currency of a change
        /// </summary>
        string GoToCurrency { get; set; }

        /// <summary>
        /// Get or Set current rate used to change money
        /// </summary>
        double Rate { get; set; }

        /// <summary>
        /// Get current rate to change back money
        /// </summary>
        double InvertRate { get; }

        /// <summary>
        /// Change money with current rate
        /// </summary>
        /// <param name="valueToChange">Amount of money to change</param>
        /// <returns>Changed money</returns>
        double Change(double valueToChange);

        /// <summary>
        /// Change back money with current rate
        /// </summary>
        /// <param name="valueToChange">Amount of money to change back</param>
        /// <returns>Changed back money</returns>
        double ChangeInvert(double valueToChange);

        /// <summary>
        /// Indicate if current exchange rate can change specified currency
        /// </summary>
        /// <param name="currency">Currency to change</param>
        /// <returns>True if it can, false else</returns>
        bool CanChange(string currency);
        
        /// <summary>
        /// Indicate if current exchange rate can change back specified currency
        /// </summary>
        /// <param name="currency">Currency to change back</param>
        /// <returns>True if it can, false else</returns>
        bool CanChangeBack(string currency);
    }
}