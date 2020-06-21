using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var uri = new Uri("https://dinero.azurewebsites.net/api/v1/payments/wallets");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            clientHandler.SslProtocols = SslProtocols.Ssl3;
            var httpClient = new HttpClient(clientHandler);
            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Application.Current.Properties["header"].ToString());
            var response = await httpClient.GetAsync(uri);
            var output = response.Content.ReadAsStringAsync().Result;
            var bla = response.Content.ReadAsStringAsync();
            var wallets = JsonConvert.DeserializeObject<List<Wallet>>(output);
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
