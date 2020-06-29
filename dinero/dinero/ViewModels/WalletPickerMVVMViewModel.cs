using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using dinero.Models;

namespace dinero
{
    public class WalletPickerMVVMViewModel : INotifyPropertyChanged
    {
        public WalletPickerMVVMViewModel(List<Wallet> walletsFrom, List<Wallet> walletsTo)
        {

            WalletsFrom = new List<Wallet>();
            WalletsTo = new List<Wallet>();
            WalletsTo = walletsTo;
            WalletsFrom = walletsFrom;
            
        }

        public List<Wallet> WalletsTo { get; set; }
        public List<Wallet> WalletsFrom { get; set; }
        private Wallet _selectedWalletFrom { get; set; }

        public Wallet SelectedWalletFrom
        {
            get => _selectedWalletFrom;
            set
            {
                if (_selectedWalletFrom != value) _selectedWalletFrom = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}