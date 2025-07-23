using WildlifeTrackerSystem.Models;

namespace WildlifeTrackerSystem.Views;

public partial class Reptiles : ContentView
{
	public Reptiles()
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
        foreach (string specie in Enum.GetNames(typeof(EReptileType)))
            speciesList.Add(specie);
        speciesPicker.ItemsSource = speciesList;
    }
}