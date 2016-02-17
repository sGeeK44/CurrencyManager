using System;

namespace CurrencyManager
{
    public class ExchangeRate
    {
        private const int EXCHANGE_PRECISION = 4;
        public double Rate { get; set; }

        public ExchangeRate(double rate)
        {
            Rate = rate;
        }

        public double Change(int valueToChange)
        {
            return Math.Round(valueToChange*Rate, EXCHANGE_PRECISION);
        }
    }
}
