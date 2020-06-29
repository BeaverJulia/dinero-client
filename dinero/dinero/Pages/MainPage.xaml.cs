using dinero.Models;
using Xamarin.Forms;

namespace dinero
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            Children.Add(new PanelPage());
            Children.Add(new ExchangePage());
            Children.Add(new UserSettings());
        }
    }
}
