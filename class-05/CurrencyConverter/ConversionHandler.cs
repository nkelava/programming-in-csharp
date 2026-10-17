using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;


namespace CurrencyConverter
{
    public class ConversionHandler
    {
        public ObservableCollection<Currency> CurrencyList { get; set; }
        private string API = "https://api.hnb.hr/tecajn/v2";

        public ConversionHandler()
        {
            var jsonObject = new HttpClient().GetStringAsync(API).GetAwaiter().GetResult();
            CurrencyList = new ObservableCollection<Currency>();
            CurrencyList = JsonConvert.DeserializeObject<ObservableCollection<Currency>>(jsonObject);
        }

        public static decimal Calculate(string amount, Currency currFrom, Currency currTo)
        {
            try
            {
                var result = (ParseDecimal(amount) * RatePerUnit(currFrom));
                result /= RatePerUnit(currTo);
                return Math.Round(result, 2);
            }
            catch
            {
                return 0;
            }
        }

        // A rate is given for "Unit" units of a currency, for example for 100 JPY
        private static decimal RatePerUnit(Currency currency)
        {
            return ParseDecimal(currency.MiddleRate) / ParseDecimal(currency.Unit);
        }

        // Accepts both "1,5" and "1.5" regardless of the language settings of the computer
        private static decimal ParseDecimal(string input)
        {
            return decimal.Parse(FixFormat(input), CultureInfo.InvariantCulture);
        }

        private static string FixFormat(string input)
        {
            return (input.Contains(",")) ? input.Replace(",", ".") : input;
        }
    }
}
