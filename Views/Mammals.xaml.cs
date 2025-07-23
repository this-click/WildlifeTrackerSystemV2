using System.Collections.ObjectModel;
using WildlifeTrackerSystem.Models;

namespace WildlifeTrackerSystem.Views;

public partial class Mammals : ContentView
{
	public Mammals()
    {
        InitializeComponent();
        InitializeSpeciesPicker();
    }

    /// <summary>
    /// Sets the values of the mammal species picker "speciesPicker"
    /// </summary>
    private void InitializeSpeciesPicker()
    {
        var speciesList = Enum.GetNames(typeof(EMammalType)).ToList();
        speciesPicker.ItemsSource = speciesList;
    }
}