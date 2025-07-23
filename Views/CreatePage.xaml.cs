using WildlifeTrackerSystem.ViewModels;

namespace WildlifeTrackerSystem.Views;

public partial class CreatePage : ContentPage
{
	public CreatePage(CreateViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}