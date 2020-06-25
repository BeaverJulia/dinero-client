using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dinero.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsPage : ContentPage
    {
        public List<Transaction> TransactionsList;
        public TransactionsPage()
        {
            InitializeComponent();
            TransactionsList = new List<Transaction>
            {
                new Transaction(),
                new Transaction(),
                new Transaction(),
            };
            Transactions.ItemsSource = TransactionsList;
        }
    }
}