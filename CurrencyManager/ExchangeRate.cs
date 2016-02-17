using System;

namespace CurrencyManager
{
    public class ExchangeRate
    {
        private const int EXCHANGE_PRECISION = 4;

        public double Rate { get; set; }
        public double InvertRate { get { return Math.Round(1/Rate, EXCHANGE_PRECISION); } }
        
        public ExchangeRate(double rate)
        {
            Rate = rate;
        }

        public double Change(double valueToChange)
        {
            return Math.Round(valueToChange*Rate, EXCHANGE_PRECISION);
        }

        public double ChangeInvert(double valueToChange)
        {
            return Math.Round(valueToChange*InvertRate, EXCHANGE_PRECISION);
        }
    }
}
