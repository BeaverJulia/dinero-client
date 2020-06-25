using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dinero.Models;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dinero
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class WalletDetailsPage : INotifyPropertyChanged
    {
        public WalletDetailsPage()
        {
            InitializeComponent(); 
            _okCommand = new RelayCommand<Wallet>(OkCommand_Execute);
        }
      
                   
        
        private RelayCommand<Wallet> _okCommand { get; set; }

        private  void OkCommand_Execute(Wallet obj)
        {
            var bla = obj.Currency;
        }
    }
}