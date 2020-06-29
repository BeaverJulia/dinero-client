using System;
using dinero.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public AccountView AccountView;

        public LoginPage()
        {
            InitializeComponent();
            btnLogin.Clicked += BtnLogin_Clicked;
            btnRegister.Clicked += BtnRegister_Clicked;
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            AccountView = new AccountView();
            var user = new UserLogin();
            user.Email = txtLogin.Text;
            user.Password = txtPassword.Text;
            if (string.IsNullOrEmpty(user.Email))
            {
                await DisplayAlert("Validation errors", "The UserName is required", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                await DisplayAlert("Validation errors", "The Password is required", "Ok");
                return;
            }

            var result = await AccountView.LoginRequest(user);
            var output = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject<Token>(output);
                Application.Current.Properties["header"] = response.Access;
                await Application.Current.SavePropertiesAsync();
                await Navigation.PushModalAsync(new MainPage());
            }
            else
            {
                //TODO - change this validation
                try
                {
                    var response = JsonConvert.DeserializeObject<ResultMessage>(output);
                    await DisplayAlert("Validation errors", response.Message, "Ok");
                }
                catch
                {
                    await DisplayAlert("Validation errors", result.StatusCode.ToString(), "Ok");

                }
            }
        }

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}