using System;

namespace CurrencyManager
{
    /// <summary>
    /// Provide methods to change money with a specified rate
    /// </summary>
    public class ExchangeCurrency : IExchangeCurrency, IEquatable<IExchangeCurrency>
    {
        private ExchangeCurrency() { }

        /// <summary>
        /// Get name of initial currency of a change
        /// </summary>
        public string InitialCurrency { get; set; }

        /// <summary>
        /// Get name target currency of a change
        /// </summary>
        public string TargetCurrency { get; set; }

        /// <summary>
        /// Get current rate used to change money from initial currency to target
        /// </summary>
        public IExchangeRate Rate { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return Equals(obj as IExchangeCurrency);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(IExchangeCurrency other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(InitialCurrency, other.InitialCurrency) && string.Equals(TargetCurrency, other.TargetCurrency);
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
                return (InitialCurrency.GetHashCode() * 397) ^ TargetCurrency.GetHashCode();
            }
        }

        /// <summary>
        /// Indicate if current exchange rate can change initial currency to target
        /// </summary>
        /// <param name="initialCurrency">Initial currency to change</param>
        /// <param name="targetCurrency">Target currency to expected</param>
        /// <returns>True if it can, false else</returns>
        public bool CanChange(string initialCurrency, string targetCurrency)
        {
            string changeTo;
            return CanChangeFrom(initialCurrency, out changeTo) && CanChangeFrom(targetCurrency, out changeTo);
        }

        /// <summary>
        /// Indicate if current exchange rate can change initial currency to another
        /// </summary>
        /// <param name="currencyToChange">Currency to change to another</param>
        /// <param name="changeToCurrency">Currency after change if current can change, else null</param>
        /// <returns>True if it can, false else</returns>
        public bool CanChangeFrom(string currencyToChange, out string changeToCurrency)
        {
            if (IsEqualToInitialeCurrency(currencyToChange))
            {
                changeToCurrency = TargetCurrency;
                return true;
            }

            if (IsEqualToTargetCurrency(currencyToChange))
            {
                changeToCurrency = InitialCurrency;
                return true;
            }

            changeToCurrency = null;
            return false;
        }

        /// <summary>
        /// Indicate if current exchange rate can only change initial currency to another target (not requested)
        /// </summary>
        /// <param name="initialCurrency">Initial currency to change</param>
        /// <param name="targetCurrency">Target currency to wanted</param>
        /// <returns>True if it can only make intermediate, false else</returns>
        public bool CanOnlyMakeIntermediateChange(string initialCurrency, string targetCurrency)
        {
            string changeTo;
            if (CanChange(initialCurrency, targetCurrency)) return false;
            if (CanChangeFrom(initialCurrency, out changeTo)) return true;
            if (CanChangeFrom(targetCurrency, out changeTo)) return true;
            return false;
        }

        /// <summary>
        /// Change money with current rate
        /// </summary>
        /// <param name="initialCurrency">Initial currency to change</param>
        /// <param name="targetCurrency">Target currency to expected</param>
        /// <param name="valueToChange">Amount of money to change</param>
        /// <returns>Changed money</returns>
        public double Change(string initialCurrency, string targetCurrency, double valueToChange)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new instance of ExchangeRate
        /// </summary>
        /// <param name="initialCurrency">Initial currency to change</param>
        /// <param name="targetCurrency">Target currency to expected</param>
        /// <param name="rate">Initial rate</param>
        public static IExchangeCurrency Create(string initialCurrency, string targetCurrency, double rate)
        {
            if (string.IsNullOrWhiteSpace(initialCurrency)) throw new ArgumentNullException("initialCurrency");
            if (string.IsNullOrWhiteSpace(targetCurrency)) throw new ArgumentNullException("targetCurrency");
            if (string.Equals(initialCurrency, targetCurrency, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException(string.Format("Initial currency have to be different from target change currency. Initial:{0}. Target:{1}.", initialCurrency, targetCurrency));

            return new ExchangeCurrency
            {
                InitialCurrency = initialCurrency,
                TargetCurrency = targetCurrency,
                Rate = ExchangeRate.Create(rate)
            };
        }

        private bool IsEqualToInitialeCurrency(string currency)
        {
            return InitialCurrency.Equals(currency, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsEqualToTargetCurrency(string currency)
        {
            return TargetCurrency.Equals(currency, StringComparison.OrdinalIgnoreCase);
        }
    }
}
