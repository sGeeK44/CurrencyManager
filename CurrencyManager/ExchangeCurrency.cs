using System;

namespace CurrencyManager
{
    /// <summary>
    /// Provide methods to change money with a specified rate
    /// </summary>
    public class ExchangeCurrency : IExchangeCurrency, IEquatable<ExchangeCurrency>
    {
        private readonly string _firstCurrency;
        private readonly string _secondCurrency;
        private IExchangeRate _rate;

        private ExchangeCurrency(string initialCurrency, string targetCurrency, IExchangeRate rate)
        {
            _firstCurrency = initialCurrency;
            _secondCurrency = targetCurrency;
            _rate = rate;
        }

        /// <summary>
        /// Set new IExchangeRate to use for next change
        /// </summary>
        /// <param name="newRate">NewIExchange to use</param>
        public void SetNewRate(IExchangeRate newRate)
        {
            if (newRate == null) throw new ArgumentNullException("newRate");
            _rate = newRate;
        }

        /// <summary>
        /// Get current rate used to change money from initial currency to target
        /// </summary>

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return Equals(obj as ExchangeCurrency);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(ExchangeCurrency other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_firstCurrency, other._firstCurrency) && string.Equals(_secondCurrency, other._secondCurrency);
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
                return (_firstCurrency.GetHashCode() * 397) ^ _secondCurrency.GetHashCode();
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
            if (IsEqualToFirstCurrency(currencyToChange))
            {
                changeToCurrency = _secondCurrency;
                return true;
            }

            if (IsEqualToSecondCurrency(currencyToChange))
            {
                changeToCurrency = _firstCurrency;
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
            if (!CanChange(initialCurrency, targetCurrency)) throw new ArgumentException(string.Format("Specified change can not be done. Unmanaged currency. Initial:{0}. Target:{1}.", initialCurrency, targetCurrency));
            return IsEqualToFirstCurrency(initialCurrency) ? _rate.Change(valueToChange) : _rate.ChangeBack(valueToChange);
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

            return new ExchangeCurrency(initialCurrency, targetCurrency, ExchangeRate.Create(rate));
        }

        private bool IsEqualToFirstCurrency(string currency)
        {
            return _firstCurrency.Equals(currency, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsEqualToSecondCurrency(string currency)
        {
            return _secondCurrency.Equals(currency, StringComparison.OrdinalIgnoreCase);
        }
    }
}
