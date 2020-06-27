using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
            _okCommand = new Command(OkCommand_Execute);

        }



        private ICommand _okCommand { get; set; }

        private  void OkCommand_Execute(Object obj)
        {
            var bla = obj;
        }
    }
}