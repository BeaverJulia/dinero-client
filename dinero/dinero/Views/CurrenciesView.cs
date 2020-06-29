using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using dinero.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace dinero
{
   public class CurrenciesView
    {
        public async Task<List<Currency>> GetRequest()
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            clientHandler.SslProtocols = SslProtocols.Ssl3;
            var httpClient = new HttpClient(clientHandler);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Application.Current.Properties["header"].ToString());
            var response = await httpClient.GetAsync(new Uri(ServerUrls.GetCurrencies)).ConfigureAwait(false);
            var output = response.Content.ReadAsStringAsync().Result;
            var currencies = JsonConvert.DeserializeObject<List<Currency>>(output);
            return currencies;
        }
        public async Task<HttpResponseMessage> CreateRequest(Exchange exchange)
        {
            dynamic json = new JObject();
            json.from_wallet = exchange.from_wallet;
            json.to_wallet = exchange.to_wallet;
            json.from_amount = exchange.from_amount;
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["header"].ToString());
            var response = await httpClient.PostAsync(new Uri("https://dinero.azurewebsites.net/api/v1/payments/exchange-currency"), content);

            return response;
        }
        public async Task<List<ExchangeRate>> GetExchangeRatesRequest()
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            clientHandler.SslProtocols = SslProtocols.Ssl3;
            var httpClient = new HttpClient(clientHandler);

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Application.Current.Properties["header"].ToString());
            var response = await httpClient.GetAsync(new Uri(ServerUrls.ExchangeRates)).ConfigureAwait(false);
            var output = response.Content.ReadAsStringAsync().Result;
            var exchangeRates = JsonConvert.DeserializeObject<List<ExchangeRate>>(output);
            return exchangeRates;
        }
    }
}
