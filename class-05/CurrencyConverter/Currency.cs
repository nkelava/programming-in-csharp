using Newtonsoft.Json;


namespace CurrencyConverter
{
    // Property names are mapped to the field names of the Croatian National Bank (HNB) API
    public class Currency
    {
        [JsonProperty("broj_tecajnice")]
        public string ExchangeRateListNumber { get; set; }

        [JsonProperty("datum_primjene")]
        public string EffectiveDate { get; set; }

        [JsonProperty("drzava")]
        public string Country { get; set; }

        [JsonProperty("drzava_iso")]
        public string CountryIsoCode { get; set; }

        [JsonProperty("sifra_valute")]
        public string NumericCode { get; set; }

        [JsonProperty("valuta")]
        public string Code { get; set; }

        [JsonProperty("jedinica")]
        public string Unit { get; set; }

        [JsonProperty("kupovni_tecaj")]
        public string BuyingRate { get; set; }

        [JsonProperty("srednji_tecaj")]
        public string MiddleRate { get; set; }

        [JsonProperty("prodajni_tecaj")]
        public string SellingRate { get; set; }
    }
}
