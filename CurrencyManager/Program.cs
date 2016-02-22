using System;
using System.Globalization;

namespace CurrencyManager
{
    public class Program
    {
        private static int Amount { get; set; }
        private static string TargetCurrency { get; set; }
        private static string InitialCurrency { get; set; }

        public static void Main(string[] args)
        {
            SetRequestChange();
            var bank = CreateBankExchange();
            Console.WriteLine(bank.Change(InitialCurrency, TargetCurrency, Amount));
            Console.ReadKey();
        }

        private static void SetRequestChange()
        {
            var requestedChange = Console.ReadLine();
            var requestedChangeSplitted = requestedChange.Split(';');
            InitialCurrency = requestedChangeSplitted[0];
            Amount = int.Parse(requestedChangeSplitted[1]);
            TargetCurrency = requestedChangeSplitted[2];
        }

        private static Bank CreateBankExchange()
        {
            var bank = new Bank();

            var countExchangeRate = int.Parse(Console.ReadLine());
            for (var i = 0; i < countExchangeRate; i++)
            {
                SetNewExchangeRate(bank);
            }

            return bank;
        }

        private static void SetNewExchangeRate(Bank bank)
        {
            var exchangeRate = Console.ReadLine();
            var exchangeRateSplitted = exchangeRate.Split(';');
            var initialCurrency = exchangeRateSplitted[0];
            var targetCurrency = exchangeRateSplitted[1];
            var rate = double.Parse(exchangeRateSplitted[2], CultureInfo.InvariantCulture);
            bank.AddExchangeRate(ExchangeCurrency.Create(initialCurrency, targetCurrency, rate));
        }
    }
}
