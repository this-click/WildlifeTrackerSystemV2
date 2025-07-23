using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WildlifeTrackerSystem.Models;

namespace WildlifeTrackerSystem.ViewModels
{
    /// <summary>
    /// Class responsible for handling the business logic of adding a new meal for the user's selected animal and holding the state of FoodPage.
    /// It also handles CRUD operations on ingredients (foods) read from user input.
    /// </summary>

    // Query parameter AnimalToUpdateId comes from ListPage. The animal for which food is added can be identified based on it.
    [QueryProperty(nameof(AnimalToUpdateId), "AnimalToUpdateId")]
    public partial class FoodViewModel : ViewModelBase
    {
        [ObservableProperty]
        string animalToUpdateId;

        [ObservableProperty]
        string mealName;

        [ObservableProperty]
        ObservableCollection<string> mealIngredients;

        [ObservableProperty]
        String ingredientName;

        [ObservableProperty]
        string selectedIngredient;

        // Property used for separating the logic of creating an ingredient from the logic of updating an ingredient, in the same form.
        [ObservableProperty]
        bool isEditMode;

        private FoodItem meal;
        private FoodManager foodManager;

        public FoodViewModel(FoodManager foodManager)
        {
            meal = new FoodItem();
            MealIngredients = new ObservableCollection<string>();

            // foodManager is injected as a dependency, this is constructor injection. 
            this.foodManager = foodManager;
        }


        /// <summary>
        /// If IsEditMode = true, updates an ingredient selected by the user when the user clicks on + button.
        /// Else, adds a new ingredient when the user clicks on + button.
        /// </summary>
        [RelayCommand]
        private void AddIngredients()
        {
            if (!IsEditMode)
            {
                if (string.IsNullOrEmpty(MealName) || string.IsNullOrEmpty(IngredientName))
                {
                    Shell.Current.DisplayAlert("Error", "Please enter valid values for all fields", "Ok");
                }
                else
                {
                    //Set the foodItem object
                    meal.Name = MealName;
                    meal.Ingredients.Add(IngredientName);
                    meal.AnimalId = AnimalToUpdateId;

                    //Set the observable property so it's displayed as it's added
                    MealIngredients.Add(IngredientName);

                    //Reset field after each addition
                    IngredientName = "";
                }
            }
            else
            {
                int index = MealIngredients.IndexOf(SelectedIngredient);
                if (!string.IsNullOrEmpty(IngredientName))
                {
                    meal.Ingredients.ChangeAt(IngredientName, index);
                    MealIngredients[index] = IngredientName;
                    
                    //Reset field, selection, edit mode
                    IngredientName = "";
                    SelectedIngredient = string.Empty;
                    IsEditMode = false;
                }
            }
        }

        /// <summary>
        /// Adds the new meal with name and ingredient properties to the container class foodManager when the user clicks on Save meal button
        /// Afterwards, it navigates back to ListPage. 
        /// 
        /// A meal is valid if it has a name and at least 1 ingredient. If the meal is not valid, an alert will inform the user to add the necessary
        /// information.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task SaveMeal()
        {
            if (meal is null || string.IsNullOrEmpty(MealName) || MealIngredients.Count <= 0)
                await Shell.Current.DisplayAlert("Error", "Please enter valid values for all fields", "Ok");
            else
            {
                foodManager.Add(meal);
                //Navigate to MainPage so every transient page is properly renewed
                await NavigateTo("../..");
            }
        }

        /// <summary>
        /// Deletes an ingredient from the meal.
        /// </summary>
        [RelayCommand]
        private void Delete()
        {
            if (SelectedIngredient != null)
            {
                int index = MealIngredients.IndexOf(SelectedIngredient);

                if (SelectedIngredient != null && index >= 0)
                {
                    //Deletes the ingredient from the observable collection.
                    MealIngredients.Remove(SelectedIngredient);
                    //Deletes the ingredient from the meal object.
                    meal.Ingredients.DeleteAt(SelectedIngredient, index);

                    //Resets ingredient selection in the form.
                    SelectedIngredient = string.Empty;
                }
            }
        }

        /// <summary>
        /// Populates the ingredient name field in the form with the old value.
        /// The actual update is done when user clicks on + button, action handled by AddIngredients relay command above.
        /// </summary>
        [RelayCommand]
        private void Update()
        {
            if (!string.IsNullOrEmpty(SelectedIngredient))
            {
                IngredientName = SelectedIngredient;
                IsEditMode = true;
            }
        }
    }
}
