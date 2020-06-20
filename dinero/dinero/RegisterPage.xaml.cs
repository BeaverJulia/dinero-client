using System;
using dinero.Models;
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
        }
    }
}