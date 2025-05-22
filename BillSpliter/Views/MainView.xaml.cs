using BillSpliter.ViewModels;

namespace BillSpliter.Views;

public partial class MainView : ContentPage
{
	public MainView(MainViewViewModel viewModel)
	{
		InitializeComponent();
        
        BindingContext = viewModel;
	}
}