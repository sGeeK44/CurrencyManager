﻿namespace CurrencyManager
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
        int CountPartInvolve { get; }

        /// <summary>
        /// Indicate if specified chain a less part involve than current
        /// </summary>
        /// <param name="otherChain">Other chain to compare</param>
        /// <returns>True if current is faster than specified, false else</returns>
        bool IsFasterThan(IExchangeChain otherChain);
    }
}