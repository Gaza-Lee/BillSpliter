using BillSpliter.ViewModels;
using BillSpliter.Views;
namespace BillSpliter
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainView(new MainViewViewModel());
        }
    }
}
