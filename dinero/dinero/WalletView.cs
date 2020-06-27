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
    public class WalletView
    {
       
        public async Task<string> CreateRequest(Wallet wallet)
        {
            dynamic json = new JObject();
            json.name = wallet.Name;
            json.currency = wallet.Currency;
            json.balance = wallet.Balance;
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var response = await httpClient.PostAsync(new Uri(ServerUrls.Wallets), content);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<Wallet>> GetRequest()
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
            var response = await httpClient.GetAsync(new Uri(ServerUrls.Wallets)).ConfigureAwait(false);
            var output = response.Content.ReadAsStringAsync().Result;
            var bla = response.Content.ReadAsStringAsync();
            var wallets = JsonConvert.DeserializeObject<List<Wallet>>(output);
            return wallets;
        }

        public async Task<Wallet> GetDetailsRequest(int id)
        {
  
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var response = await httpClient.GetStringAsync(new Uri(ServerUrls.Wallets));
            var wallet = JsonConvert.DeserializeObject<Wallet>(response);
            return wallet;
        }
    }
}