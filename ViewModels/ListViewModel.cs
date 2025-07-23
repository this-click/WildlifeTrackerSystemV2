using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;
using WildlifeTrackerSystem.Models;
using WildlifeTrackerSystem.Views;

namespace WildlifeTrackerSystem.ViewModels
{
    /// <summary>
    /// Class responsible for holding the state of ListPage and handling the business logic of:
    /// - displaying the list of animals
    /// - deleting an animal
    /// - navigation to EditPage, to update an animal
    /// - navigation to MainPage to create a new animal
    /// - displaying category and species specific animal data based on SelectedAnimal
    /// - navigation to FoodPage when user adds a new meal for SelectedAnimal
    /// </summary>
    public partial class ListViewModel : ViewModelBase
    {
        [ObservableProperty]
        Animal selectedAnimal;

        [ObservableProperty]
        List<Animal> animals;

        [ObservableProperty]
        string foodSchedule;

        [ObservableProperty]
        string eaterType;

        [ObservableProperty]
        string extraInfo;

        [ObservableProperty]
        bool isExtraVisible;

        [ObservableProperty]
        bool isDataSaved;

        private AnimalManager animalManager;
        private FoodManager foodManager;
        private FileManager fileManager;
        private IFileSaver fileSaver;

        public ListViewModel(AnimalManager animalManager, FoodManager foodManager, FileManager fileManager)
        {
            // the managers are injected as dependencies, this is constructor injection. 
            this.animalManager = animalManager;
            this.foodManager = foodManager;
            this.fileManager = fileManager;
            //Populates the list page with animals list on loading
            Animals = GetAnimals();
        }

        /// <summary>
        /// Navigates back to CreatePage when user clicks on Add new button.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task Add()
        {
            await NavigateTo(nameof(CreatePage));
        }

        /// <summary>
        /// Displays the following data about an animal, which the user selected by clicking on the UI list:
        /// - food information
        /// - category and species specific data
        /// 
        /// Sets the visibility to true for extraInfo and foodInfo layouts. This way, only when the animal selection changes, 
        /// the extra information becomes visible.
        /// </summary>
        [RelayCommand]
        private void AnimalChanged()
        {
            try
            {
                if (SelectedAnimal != null)
                {
                    EaterType = Enum.GetName(SelectedAnimal.GetFoodSchedule().EaterType);
                    ExtraInfo = SelectedAnimal.GetExtraInfo();

                    IsExtraVisible = true;

                    FoodSchedule = GetMeals();
                }
            }
            catch (Exception e)
            {
                Shell.Current.DisplayAlert("Error", e.Message, "Ok");
            }

        }

        /// <summary>
        /// Deletes the animal selected by the user.
        /// </summary>
        [RelayCommand]
        private void Delete()
        {
            if (SelectedAnimal != null)
            {
                int index = GetIndexFromId(SelectedAnimal.Id);

                if (SelectedAnimal != null && index >= 0)
                {
                    animalManager.DeleteAt(SelectedAnimal, index);
                    Animals = GetAnimals();

                    //Resets animal selection
                    SelectedAnimal = null;
                    IsExtraVisible = false;
                }
            }
        }

        /// <summary>
        /// Navigates to EditPage with SelectedAnimal as a string query parameter.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task Update()
        {
            if (SelectedAnimal != null)
            {
                //Ideally I would have sent the SelectedAnimal as an object to EditPage. 
                //But I couldn't fix InvalidCastException when sending Animal as an object, so I'm sending it as a string instead.
                await NavigateTo($"{nameof(EditPage)}?AnimalToUpdate={SelectedAnimal}");
            }
        }

        /// <summary>
        /// Navigates to add FoodPage when user clicks on Add meal button. SelectedAnimalId is sent as a string query parameter,
        /// so the selected animal is connected to the meal that is about to be created.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task UpdateFood()
        {
            await NavigateTo(($"{nameof(FoodPage)}?AnimalToUpdateId={SelectedAnimal.Id}"));
        }

        [RelayCommand]
        private async Task OpenJson()
        {
            List<Animal> animalsFromFile = new List<Animal>();

            if (Animals != null)
            {
                Animals.Clear();
                animalManager.DeleteAll();
                foodManager.DeleteAll();
            }

            try
            {
                (bool okRead, string jsonString) = await fileManager.ReadAnimalsFromJsonFile();
                if (okRead)
                {
                    try
                    {
                        var dynamicObj = animalManager.JsonDeserialize(jsonString);
                        Animal animal = null;

                        for (int i = 0; i < dynamicObj.Count; i++)
                        {
                            animal = GetAnimalType(dynamicObj[i]);

                            if (animal != null)
                            {
                                animalManager.Add(animal);
                                Animals = GetAnimals();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        await Shell.Current.DisplayAlert("Error", e.Message, "Ok");
                    }
                    for (int i = 0; i < animalsFromFile.Count; i++)
                    {
                        animalManager.Add(animalsFromFile[i]);
                    }
                    Animals = GetAnimals();
                    IsDataSaved = true;
                }
                else
                    await Shell.Current.DisplayAlert("Error", "Please open a json file.", "Ok");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", $"Something went wrong. Please check your file is correct.", "Ok");
            }
        }

        /// <summary>
        /// Saves the animal list in a JSON file.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task SaveJson()
        {
            string jsonString = "";
            try
            {
                jsonString = animalManager.JsonSerialize(GetAnimals());
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", $"{e.Message}", "Ok");
            }

            FileSaverResult result = null;

            try
            {
                result = await fileManager.SaveAnimalsToFile(jsonString, "AnimalsList.json");
            }
            catch (InvalidOperationException e)
            {
                await Shell.Current.DisplayAlert("Error", $"{e.Message}", "Ok");
            }
            catch (IOException e)
            {
                await Shell.Current.DisplayAlert("Error", $"{e.Message}", "Ok");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", $"{e.Message}", "Ok");
            }

            if (result != null && result.IsSuccessful)
                IsDataSaved = true;
        }

        /// <summary>
        /// Navigates to MainPage when user clicks on Go Home button.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task GoHome()
        {
            await NavigateTo(($"..?IsDataSaved={IsDataSaved}"));
        }

        /// <summary>
        /// Helper method which builds the string to be saved in a text file. Aims to improve performance by writing on the HDD fewer times.
        /// </summary>
        /// <returns></returns>
        private string GetAnimalsToString()
        {
            StringBuilder stringbuilder = new StringBuilder();
            for (int i = 0; i < GetAnimals().Count; i++)
            {
                stringbuilder.Append(animalManager.GetAt(i));
                stringbuilder.Append("\n+");
            }
            return stringbuilder.ToString();
        }

        /// <summary>
        /// Gets the list of registered animals.
        /// </summary>
        /// <returns></returns>
        private List<Animal> GetAnimals()
        {
            int numAnimals = animalManager.Count();
            List<Animal> animals = new List<Animal>();

            for (int i = 0; i < numAnimals; i++)
            {
                if (animalManager.GetAt(i) != null)
                    animals.Add(animalManager.GetAt(i));
            }
            return animals;
        }

        /// <summary>
        /// Returns a list of meals for an animal, if the selected animal ID matches the animal ID for that meal.
        /// </summary>
        /// <returns>list of meals in string format</returns>
        private string GetMeals()
        {
            int numMeals = foodManager.Count();
            string mealsTxt = string.Empty;

            for (int i = 0; i < numMeals; i++)
            {
                FoodItem meal = foodManager.GetAt(i);
                if (meal != null && meal.AnimalId == SelectedAnimal.Id)
                    mealsTxt += meal;
            }
            return mealsTxt;
        }

        /// <summary>
        /// Helper method to return the position of an animal in the animal list, based on its ID.
        /// This method is necessary to satisfy the index requirement of the project.
        /// </summary>
        /// <param name="id">animal ID</param>
        /// <returns>index if ID matches, -1 otherwise</returns>
        private int GetIndexFromId(string id)
        {
            if (Animals != null && !string.IsNullOrEmpty(id))
            {
                return Animals.FindIndex(animal => animal.Id == id);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Helper method that converts a dynamic object into the appropriate species specific animal object at runtime.
        /// It's used to add the animal read from a JSON file in the animal list via animalManager.
        /// It reads the species value from the JSON file and based on that, it converts to the appropriate animal object at runtime.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>animal object</returns>
        private static Animal GetAnimalType(dynamic obj)
        {
            Animal animal = null;
            string species = obj.Species.ToString();
            switch (species)
            {
                case nameof(EMammalType.Alpaca):
                    animal = obj.ToObject<Models.Alpaca>();
                    break;
                case nameof(EMammalType.Donkey):
                    animal = obj.ToObject<Models.Donkey>();
                    break;
                case nameof(EReptileType.Lizard):
                    animal = obj.ToObject<Models.Lizard>();
                    break;
                case nameof(EReptileType.Turtle):
                    animal = obj.ToObject<Models.Turtle>();
                    break;
                case nameof(EFishType.Fangtooth):
                    animal = obj.ToObject<Models.Fangtooth>();
                    break;
                case nameof(EFishType.Ray):
                    animal = obj.ToObject<Models.Ray>();
                    break;
            }

            return animal;
        }
    }
}
