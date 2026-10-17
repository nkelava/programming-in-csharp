using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;


namespace CurrencyConverter
{
    public class ConversionHandler
    {
        public ObservableCollection<Currency> CurrencyList { get; set; }
        private string API = "https://api.hnb.hr/tecajn/v2";

        public ConversionHandler()
        {
            var jsonObject = new WebClient().DownloadString(API);
            CurrencyList = new ObservableCollection<Currency>();
            CurrencyList = JsonConvert.DeserializeObject<ObservableCollection<Currency>>(jsonObject);
        }

        public static decimal Calculate(string amount, Currency currFrom, Currency currTo)
        {
            try
            {
                var result = (decimal.Parse(FixFormat(amount)) * decimal.Parse(currFrom.srednji_tecaj));
                result /= decimal.Parse(currTo.srednji_tecaj);
                return Math.Round(result, 2);
            }
            catch
            {
                return 0;
            }
        }

        private static string FixFormat(string input)
        {
            return (input.Contains(",")) ? input.Replace(",", ".") : input;
        }
    }
}
