using System;
using dinero.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public AccountView AccountView;

        public RegisterPage()
        {
            InitializeComponent();
            AccountView = new AccountView();
            btnLogin.Clicked += BtnLogin_Clicked;
            btnRegister.Clicked += BtnRegister_Clicked;
        }

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            var user = new UserRegister();
            user.Email = txtEmail.Text;
            user.Name = txtName.Text;
            user.Password = txtPassword.Text;
            if (string.IsNullOrEmpty(user.Email))
            {
                await DisplayAlert("Validation errors", "The Name is required", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                await DisplayAlert("Validation errors", "The Password is required", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                await DisplayAlert("Validation errors", "The Email is required", "Ok");
                return;
            }

            var result = await AccountView.RegisterRequest(user);
            var output = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Account Created", "Account created, go to login page", "Ok");
                await Navigation.PushModalAsync(new LoginPage());
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
        
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}