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
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var output = await httpClient.PostAsync(new Uri(ServerUrls.Login), content);

            return output;
        }
        public async Task<HttpResponseMessage> RegisterRequest(UserRegister user)
        {
            dynamic json = new JObject();
            json.name = user.Name;
            json.email = user.Email;
            json.password = user.Password;
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var response = await httpClient.PostAsync(new Uri("https://dinero.azurewebsites.net/api/v1/accounts/registration"), content);
            return response;
        }
        public async Task<HttpResponseMessage> UserSearch(String user, string limit)
        {
            var query = "?limit=" + limit + "&search=" + user;
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var uri = new Uri(ServerUrls.UserSearch + query);
            var output = await httpClient.GetAsync(uri);
            return output;
        }
    }
}
