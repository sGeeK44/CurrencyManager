using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace CurrencyManager
{
    public class Program
    {
        private static int Amount { get; set; }
        private static string TargetCurrency { get; set; }
        private static string InitialCurrency { get; set; }

        public static void Main(string[] args)
        {
            const string inputs = @"EUR;550;JPY
6
AUD;CHF;0.9661
JPY;KRW;13.1151
EUR;CHF;1.2053
AUD;JPY;86.0305
EUR;USD;1.2989
JPY;INR;0.6571";
            var result = Change(new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(inputs))));
            Console.WriteLine("{0} {1} correspond à {2} {3}.", Amount, InitialCurrency, result, TargetCurrency);
            Console.WriteLine("Appuyer sur une touche pour continuer...");
            Console.ReadKey();
        }

        private static int Change(TextReader inputs)
        {
            SetRequestChange(inputs);
            var bank = CreateBankExchange(inputs);
            return bank.Change(InitialCurrency, TargetCurrency, Amount);
        }

        private static Bank CreateBankExchange(TextReader inputs)
        {
            var bank = new Bank();

            var countExchangeRate = int.Parse(inputs.ReadLine());
            for (int i = 0; i < countExchangeRate; i++)
            {
                SetNewExchangeRate(bank, inputs);
            }

            return bank;
        }

        private static void SetNewExchangeRate(Bank bank, TextReader inputs)
        {
            var exchangeRate = inputs.ReadLine();
            var exchangeRateSplitted = exchangeRate.Split(';');
            var initialCurrency = exchangeRateSplitted[0];
            var targetCurrency = exchangeRateSplitted[1];
            var rate = double.Parse(exchangeRateSplitted[2], CultureInfo.InvariantCulture);
            bank.AddExchangeRate(ExchangeCurrency.Create(initialCurrency, targetCurrency, rate));
        }

        private static void SetRequestChange(TextReader inputs)
        {
            var requestedChange = inputs.ReadLine();
            var requestedChangeSplitted = requestedChange.Split(';');
            InitialCurrency = requestedChangeSplitted[0];
            Amount = int.Parse(requestedChangeSplitted[1]);
            TargetCurrency = requestedChangeSplitted[2];
        }
    }
}
