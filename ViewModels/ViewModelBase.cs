using CommunityToolkit.Mvvm.ComponentModel;

namespace WildlifeTrackerSystem.ViewModels
{
    /// <summary>
    /// Base ViewModel, from which all View Models inherit functionality.
    /// </summary>
    public class ViewModelBase : ObservableObject
    {

        /// <summary>
        /// Removes instances of unused pages to increase performance and keep shell navigation stack predictable and lean.
        /// 
        /// Bug which made this necessary: Maui Shell always re-uses pages. No option to re-create on navigation.
        /// https://github.com/dotnet/maui/issues/10164
        /// </summary>
        /// <param name="uri">target page string</param>
        /// <returns></returns>
        public async Task NavigateTo(string uri)
        {
            var page = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            await Shell.Current.GoToAsync(uri);
            if (page != null)
                Application.Current.MainPage.Navigation.RemovePage(page);
        }
    }
}
