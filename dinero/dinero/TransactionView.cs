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
    public class TransactionView
    {
        public async Task<List<Transaction>> GetRequests(int limit)
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            clientHandler.SslProtocols = SslProtocols.Ssl3;
            var httpClient = new HttpClient(clientHandler);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["header"].ToString());
            var response = await httpClient.GetAsync(new Uri(ServerUrls.GetTransactions+limit.ToString())).ConfigureAwait(false);
            var output = response.Content.ReadAsStringAsync().Result;
            var transactions = JsonConvert.DeserializeObject<List<Transaction>>(output);
            return transactions;
        }
        public async Task<HttpResponseMessage> CreateRequest(Transaction transaction)
        {
            dynamic json = new JObject();
            json.toUser = transaction.ToUser;
            json.currency = transaction.Currency;
            json.amount = transaction.Amount;
            json.DateTime = DateTime.UtcNow;
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var response = await httpClient.PostAsync(new Uri(ServerUrls.PostTransactions), content);

            return  response;
        }
    }
}
