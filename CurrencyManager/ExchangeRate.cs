﻿using System;

namespace CurrencyManager
{
    /// <summary>
    /// Provide methods to change money with a specified rate
    /// </summary>
    public class ExchangeRate
    {
        private const int EXCHANGE_PRECISION = 4;
        
        /// <summary>
        /// Get or Set current rate used to change money
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Get current rate to change back money
        /// </summary>
        public double InvertRate { get { return Math.Round(1 / Rate, EXCHANGE_PRECISION); } }

        /// <summary>
        /// Create a new instance of ExchangeRate
        /// </summary>
        /// <param name="rate">Initial rate</param>
        public ExchangeRate(double rate)
        {
            Rate = rate;
        }

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
    }
}
