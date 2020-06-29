using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using dinero.Models;

namespace dinero
{
    public class CurrencyPickerMVVMViewModel : INotifyPropertyChanged
    {
        public CurrencyPickerMVVMViewModel()
        {
            CurrenciesList = new List<Currency>();
            CurrenciesView = new CurrenciesView();
            CurrenciesList = CurrenciesView.GetRequest().Result;
        }

        public List<Currency> CurrenciesList { get; set; }
        public CurrenciesView CurrenciesView { get; set; }
        private Currency _selectedCurrency { get; set; }

        public Currency SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                if (_selectedCurrency != value) _selectedCurrency = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
