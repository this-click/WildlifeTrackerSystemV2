using WildlifeTrackerSystem.ViewModels;

namespace WildlifeTrackerSystem.Views;

public partial class FoodPage : ContentPage
{
	public FoodPage(FoodViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}