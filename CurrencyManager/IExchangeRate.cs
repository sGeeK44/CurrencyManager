namespace CurrencyManager
{
    /// <summary>
    /// Expose methods to change money with a specified rate
    /// </summary>
    public interface IExchangeRate
    {
        /// <summary>
        /// Get current rate used to change money
        /// </summary>
        double Rate { get; }

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
        double ChangeBack(double valueToChange);
    }
}