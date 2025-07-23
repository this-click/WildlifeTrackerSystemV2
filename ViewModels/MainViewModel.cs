using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WildlifeTrackerSystem.Views;

namespace WildlifeTrackerSystem.ViewModels
{
    /// <summary>
    /// Class with the purpose of being very static and the first in the navigation stack, so it hides .NET MAUI bugs listed below.
    /// 
    /// I can't fulfill the requirement for resetting the page (see bug #2 below) if that page has radio buttons or components rendered conditionally.
    /// So my workaround is to reset from a static page and the reset means deleting all the items in the FoodManager and AnimalManager lists.
    /// 
    /// Regarding menu bar items, I have split them between MainViewModel and ListViewModel on the sole criteria of only leaving those that actually work 
    /// on the pages where they are displayed (see bug #1 below).
    /// 
    /// Bugs forcing my design choices: 
    /// 1. Child issue of #5382 - Commands bound with MenuBar items fail to execute after navigation due to the original BindingContext issue.
    /// https://github.com/dotnet/maui/issues/10835
    /// 
    /// 2. Maui Shell always re-uses pages. No option to re-create on navigation.
    /// https://github.com/dotnet/maui/issues/10164
    /// </summary>

    // Query parameter IsDataSaved comes from ListPage. Used to display alert in case of unsaved data to file.
    [QueryProperty(nameof(IsDataSaved), "IsDataSaved")]
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        bool isDataSaved;

        private readonly AnimalManager animalManager;
        private readonly FoodManager foodManager;

        public MainViewModel(AnimalManager animalManager, FoodManager foodManager)
        {
            // the managers are injected as dependencies, this is constructor injection. 
            this.animalManager = animalManager;
            this.foodManager = foodManager;
        }

        [RelayCommand]
        private async Task AddAnimal()
        {
            await NavigateTo(nameof(CreatePage));
        }

        [RelayCommand]
        private async Task ListAnimals()
        {
            await NavigateTo(nameof(ListPage));
        }

        [RelayCommand]
        private async Task Reset()
        {
            bool answer = false;

            if (IsDataSaved)
            {
                animalManager.DeleteAll();
                foodManager.DeleteAll();
                IsDataSaved = false;
            }
            else
            {
                answer = await Shell.Current.DisplayAlert("Are you sure?", "You haven't saved to file!", "Ok", "Cancel");
                if (answer)
                {
                    animalManager.DeleteAll();
                    foodManager.DeleteAll();
                }
            }
        }

        //Closes the app when user clicks on menu item Exit.
        [RelayCommand]
        private async Task Exit()
        {
            bool answer = await Shell.Current.DisplayAlert("Are you sure?", "This will close the app.", "Ok", "Cancel");
            if (answer)
                Application.Current.Quit();
        }
    }
}

