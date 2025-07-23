
using WildlifeTrackerSystem.ViewModels;

namespace WildlifeTrackerSystem.Views;

public partial class ListPage : ContentPage
{
    public ListPage(ListViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}