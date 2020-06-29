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
            var response =
                await httpClient.PostAsync(new Uri("https://dinero.azurewebsites.net/api/v1/accounts/registration"),
                    content);
            return response;
        }

        public async Task<List<User>> UserSearch(string user, string limit)
        {
            var query = "?limit=" + limit + "&search=" + user;
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            clientHandler.SslProtocols = SslProtocols.Ssl3;

            var httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Application.Current.Properties["header"].ToString());

            var uri = new Uri(ServerUrls.UserSearch + query);
            var output = await httpClient.GetAsync(uri).ConfigureAwait(false);
            var response = output.Content.ReadAsStringAsync().Result;
            var users = JsonConvert.DeserializeObject<GenericOutput<User>>(response);
            return users.Results;
        }
        public async Task<HttpResponseMessage> PasswordRequest(string email)
        {
            dynamic json = new JObject();
            json.email = email;
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var output = await httpClient.PostAsync(new Uri(ServerUrls.GetPassword), content);
            return output;
        }
        public async Task<HttpResponseMessage> PasswordResendRequest(string email)
        {
            dynamic json = new JObject();
            json.email = email;
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var output = await httpClient.PostAsync(new Uri(ServerUrls.ResendEmail), content);
            return output;
        }
        
        public async Task<HttpResponseMessage> PasswordChangeRequest(ChangePassword data)
        {
            dynamic json = new JObject();
            json.old_password = data.OldPassword;
            json.new_password = data.NewPassword;
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Application.Current.Properties["header"].ToString());
            var output = await httpClient.PutAsync(new Uri(ServerUrls.ChangePassword), content);
            return output;
        }
        
        public async Task<HttpResponseMessage> PatchAsync(UserRegister user)
        {
            dynamic json = new JObject();
            json.name = user?.Name;
            json.email = user?.Email;
            json.password = user?.Password;
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            clientHandler.SslProtocols = SslProtocols.Ssl3;
            var httpClient = new HttpClient(clientHandler);


            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Application.Current.Properties["header"].ToString());
            var uri = new Uri(ServerUrls.LoggedUser);
            var request = await httpClient.PatchAsync(uri, content);
            return request;
        }
        public async Task<UserRegister> GetUserInfo()
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

            var uri = new Uri(ServerUrls.LoggedUser);
            var output = await httpClient.GetAsync(uri).ConfigureAwait(false);
            var response = output.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<UserRegister>(response);
            return user;
        }
    }
}