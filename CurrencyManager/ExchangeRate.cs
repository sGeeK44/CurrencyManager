using System;

namespace CurrencyManager
{
    /// <summary>
    /// Provide methods to change money with a specified rate
    /// </summary>
    public class ExchangeRate : IExchangeRate, IEquatable<IExchangeRate>
    {
        private const int EXCHANGE_PRECISION = 4;

        private ExchangeRate() { }

        /// <summary>
        /// Get or set name of initial currency of a change
        /// </summary>
        public string ComeFromCurrency { get; set; }

        /// <summary>
        /// Get or set name target currency of a change
        /// </summary>
        public string GoToCurrency { get; set; }

        /// <summary>
        /// Get or Set current rate used to change money
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Get current rate to change back money
        /// </summary>
        public double InvertRate { get { return Math.Round(1 / Rate, EXCHANGE_PRECISION); } }

        /// <summary>
        /// Change money with current rate
        /// </summary>
        /// <param name="valueToChange">Amount of money to change</param>
        /// <returns>Changed money</returns>
        public double Change(double valueToChange)
        {
            return Math.Round(valueToChange * Rate, EXCHANGE_PRECISION);
        }

        /// <summary>
        /// Change back money with current rate
        /// </summary>
        /// <param name="valueToChange">Amount of money to change back</param>
        /// <returns>Changed back money</returns>
        public double ChangeInvert(double valueToChange)
        {
            return Math.Round(valueToChange * InvertRate, EXCHANGE_PRECISION);
        }
        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return Equals(obj as IExchangeRate);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(IExchangeRate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(ComeFromCurrency, other.ComeFromCurrency) && string.Equals(GoToCurrency, other.GoToCurrency);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return (ComeFromCurrency.GetHashCode() * 397) ^ GoToCurrency.GetHashCode();
            }
        }

        /// <summary>
        /// Create a new instance of ExchangeRate
        /// </summary>
        /// <param name="fromName">Name of initial currency of a change</param>
        /// <param name="toName">Name target currency of a change</param>
        /// <param name="rate">Initial rate</param>
        public static IExchangeRate Create(string fromName, string toName, double rate)
        {
            if (string.IsNullOrWhiteSpace(fromName)) throw new ArgumentNullException("fromName");
            if (string.IsNullOrWhiteSpace(toName)) throw new ArgumentNullException("toName");
            if (string.Equals(fromName, toName, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException(string.Format("Initial currency have to be different from target change currency. Initial:{0}. Target:{1}.", fromName, toName));

            return new ExchangeRate
            {
                ComeFromCurrency = fromName,
                GoToCurrency = toName,
                Rate = rate
            };
        }

        /// <summary>
        /// Indicate if current exchange rate can change specified currency
        /// </summary>
        /// <param name="currency">Currency to change</param>
        /// <returns>True if it can, false else</returns>
        public bool CanChange(string currency)
        {
            return ComeFromCurrency.Equals(currency, StringComparison.OrdinalIgnoreCase);
        }
        
        /// <summary>
        /// Indicate if current exchange rate can change back specified currency
        /// </summary>
        /// <param name="currency">Currency to change back</param>
        /// <returns>True if it can, false else</returns>
        public bool CanChangeBack(string currency)
        {
            return GoToCurrency.Equals(currency, StringComparison.OrdinalIgnoreCase);
        }
    }
}
