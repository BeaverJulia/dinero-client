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
    public partial class TransactionDetails : ContentPage
    {
        public TransactionDetails(Transaction transaction)
        {
            InitializeComponent();
            BindingContext = transaction;
        }
    }
}