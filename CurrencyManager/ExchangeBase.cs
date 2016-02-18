namespace CurrencyManager
{
    public abstract class ExchangeBase : IExchangeChain
    {
        /// <summary>
        /// Change money with current rate
        /// </summary>
        /// <param name="valueToChange">Amount of money to change</param>
        /// <returns>Changed money</returns>
        public abstract double Change(double valueToChange);

        /// <summary>
        /// Get number of intermediate change needed to complete change
        /// </summary>
        public abstract int CountPartInvolve { get; }

        /// <summary>
        /// Indicate if specified chain a less part involve than current
        /// </summary>
        /// <param name="otherChain">Other chain to compare</param>
        /// <returns>True if current is faster than specified, false else</returns>
        public bool IsFasterThan(IExchangeChain otherChain)
        {
            if (otherChain == null) return true;
            return CountPartInvolve < otherChain.CountPartInvolve;
        }
    }
}
