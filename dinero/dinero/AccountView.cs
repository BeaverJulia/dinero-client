using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using dinero.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dinero
{
    public class AccountView
    {
        public async Task<HttpResponseMessage> LoginRequest(UserLogin user)
        {
            dynamic json = new JObject();
            json.email = user.Email;
            json.password = user.Password;
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var uri = new Uri("https://286ad451db46.ngrok.io/api/v1/accounts/login");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var output = await httpClient.PostAsync(uri, content);
            return output;
        }
        public async Task<string> RegisterRequest(UserRegister user)
        {
            dynamic json = new JObject();
            json.name = user.Name;
            json.email = user.Email;
            json.password = user.Password;
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var uri = new Uri("https://286ad451db46.ngrok.io/api/v1/accounts/registration");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var response = await httpClient.PostAsync(uri, content);
            var headers = response.Headers;
            return await response.Content.ReadAsStringAsync();
        }
    }
}
