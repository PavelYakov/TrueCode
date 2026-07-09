using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UserService.Domain.Entites;

namespace CurrencyUpdater.Services
{
    public class CbrCurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _cbrUrl;


        public CbrCurrencyService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;

            _cbrUrl = configuration["Cbr:CbrUrl"]
                    ?? throw new Exception("CbrUrl is not configured");
        }


        public async Task<List<Currency>> GetCurrenciesAsync()
        {
            var xml = await _httpClient
                .GetStringAsync(_cbrUrl);


            var document = XDocument.Parse(xml);


            var currencies = document
                .Descendants("Valute")
                .Select(x =>
                {
                    var value = decimal.Parse(
                        x.Element("Value")!.Value,
                        new CultureInfo("ru-RU"));


                    var nominal = decimal.Parse(
                        x.Element("Nominal")!.Value);


                    return new Currency
                    {
                        Name = x.Element("Name")!.Value,

                        Rate = value / nominal,

                        Code = x.Element("CharCode")!.Value,
                    };
                })
                .ToList();


            return currencies;
        }
    }
}
