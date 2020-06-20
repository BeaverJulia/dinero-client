using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using dinero.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dinero
{
    public class WalletView
    {
        private List<Wallet> wallets;
        public async Task<string> CreateRequest(Wallet wallet)
        {
            dynamic json = new JObject();
            json.name = wallet.Name;
            json.currency = wallet.Currency;
            json.balance = wallet.Balance;
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var uri = new Uri("https://286ad451db46.ngrok.io/api/v1/accounts/registration");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var response = await httpClient.PostAsync(uri, content);

            return await response.Content.ReadAsStringAsync();
        }
        public async Task<List<Wallet>> GetRequest()
        {
            dynamic json = new JObject();
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var uri = new Uri("https://286ad451db46.ngrok.io/api/v1/accounts/registration");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var response = await httpClient.GetStringAsync(uri);
            wallets = JsonConvert.DeserializeObject<List<Wallet>>(response);
            return wallets;
        }

        public async Task<Wallet> GetDetailsRequest(int id)
        {
            dynamic json = new JObject();
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var uri = new Uri("https://286ad451db46.ngrok.io/api/v1/accounts/registration");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var response = await httpClient.GetStringAsync(uri);
            var wallet = JsonConvert.DeserializeObject<Wallet>(response);
            return wallet;
        }
    }
}
