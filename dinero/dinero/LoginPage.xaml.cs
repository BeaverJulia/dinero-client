using System;
using dinero.Models;
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
            /*Application.Current.Properties["header"]
            Application.Current.SavePropertiesAsync();*/
        }

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}