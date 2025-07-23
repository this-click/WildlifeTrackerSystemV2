using WildlifeTrackerSystem.ViewModels;

namespace WildlifeTrackerSystem.Views;

public partial class EditPage : ContentPage
{
	public EditPage(EditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}