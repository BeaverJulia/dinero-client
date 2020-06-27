using System;
using System.Globalization;
using dinero.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTransaction : ContentPage
    {
        public TransactionView TransactionView;

        public AddTransaction()
        {
            InitializeComponent();
            BindingContext = new PickerMVVMViewModel();
            btnSend.Clicked += BtnSend_Clicked;
        }

        private async void BtnSend_Clicked(object sender, EventArgs e)
        {
            var transaction = new Transaction();
            var user = new User();
            transaction.Amount = float.Parse(txtAmount.Text,
                CultureInfo.InvariantCulture);
            transaction.Currency = (Currency) blaPicker.SelectedItem;
            user.Name = txtToUser.Text;
            transaction.FromUser = user;
            if (transaction.Amount < 0)
            {
                await DisplayAlert("Validation errors", "Please provide correct Amount", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                await DisplayAlert("Validation errors", "The User Name is required", "Ok");
                return;
            }

            var result = await TransactionView.CreateRequest(transaction);
            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Transaction completed", result.ReasonPhrase, "Ok");
            }
            else
            {
                await DisplayAlert("Something went wrong", result.ReasonPhrase, "Ok");

            }
        }
    }
}