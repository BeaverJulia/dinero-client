using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using dinero.Models;
using Newtonsoft.Json;
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
    }
}
