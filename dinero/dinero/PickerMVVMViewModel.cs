using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using dinero.Models;

namespace dinero
{
    public class PickerMVVMViewModel
    {
        public List<Currency> CurrenciesList { get; set; }
        public CurrenciesView CurrenciesView { get; set; }
        public PickerMVVMViewModel()
        {
            CurrenciesList = new List<Currency>();
            CurrenciesView = new CurrenciesView();
            var bla= CurrenciesView.GetRequest().Result;
            CurrenciesList = bla;
        }

    }
   
}