using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            btnLogin.Clicked += BtnLogin_Clicked;
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            var userName = txtLogin.Text;
            var password = txtPassword.Text;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Validation errors", "The UserName is required", "Ok");
                return;
            }
            var result = await SendRequest(userName, password);
            /*Application.Current.Properties["header"]
            Application.Current.SavePropertiesAsync();*/
         
        }

        private async Task<string> SendRequest(string username, string password)
        {
            JObject json = new JObject(username,password);
            var uri = new Uri("https://286ad451db46.ngrok.io/api/v1/accounts/login");
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var httpClient = new HttpClient(clientHandler);
            var response = await httpClient.PostAsJsonAsync(uri, json);
            var responsemessage =response.RequestMessage;
            var statuscode = response.StatusCode;
            return await response.Content.ReadAsStringAsync();

        }

    }
}