using WildlifeTrackerSystem.Models;

namespace WildlifeTrackerSystem.Views;

public partial class Fish : ContentView
{
	public Fish()
	{
		InitializeComponent();
        InitializeSpeciesPicker();
	}

    /// <summary>
    /// Sets the values of the fish species picker "speciesPicker"
    /// </summary>
    private void InitializeSpeciesPicker()
    {
        List<string> speciesList = new List<string>();
        foreach (string specie in Enum.GetNames(typeof(EFishType)))
            speciesList.Add(specie);
        speciesPicker.ItemsSource = speciesList;
    }
}