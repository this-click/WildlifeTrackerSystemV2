using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WildlifeTrackerSystem.Models;

namespace WildlifeTrackerSystem.ViewModels
{
    /// <summary>
    /// Class responsible for handling the business logic of updating an animal and holding the state of EditPage.
    /// 
    /// Since I had issues sending SelectedAnimal as an object and settled for sending it as a string, I need to re-create it in order to pre-populate 
    /// all the fields with the animal old data. That happens when user clicks on Load old data button.
    /// This class inherits MainViewModel for 2 reasons: 
    /// - I have access to observable props and avoid adding them again (same fields as when creating a new animal). 
    /// - all the custom components (Mammal, Alpaca etc) depend on MainViewModel data type.
    /// 
    /// In order to prevent complications and a plethora of bugs, I'm restring the user from changing species and category, which are major 
    /// decision points in this app.
    /// </summary>


    // Query parameter AnimalToUpdate comes from ListPage and it's mostly useful for its ID, but also for displaying and verifying the correct 
    // animal information is being updated. The animal being edited can be identified based on it.
    [QueryProperty(nameof(AnimalToUpdate), "AnimalToUpdate")]
    public partial class EditViewModel : CreateViewModel
    {
        [ObservableProperty]
        string animalToUpdate;

        // Property used to restrict the user from changing species and category.
        [ObservableProperty]
        bool isSpeciesEnabled;

        private readonly AnimalManager animalManager;
        private readonly FileManager fileManager;
        Animal animalNew;

        public EditViewModel(AnimalManager animalManager) : base(animalManager)
        {
            //Initialize Gender picker with values from EGenders enum.
            //Convert from Array returned by Enum.GetValues to IEnumerable needed by the ObservableCollection
            Genders = new ObservableCollection<EGenderType>(Enum.GetValues(typeof(EGenderType)).Cast<EGenderType>());
            Categories = new ObservableCollection<ECategoryType>(Enum.GetValues(typeof(ECategoryType)).Cast<ECategoryType>());

            // AnimalManager is injected as dependency, this is constructor dependency injection. 
            this.animalManager = animalManager;

        }

        /// <summary>
        /// Updates an animal when user clicks on Update animal button.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task UpdateAnimal()
        {
            bool okCategory = false, okSpecies = false;
            switch (SelectedSpecies)
            {
                case nameof(EMammalType.Alpaca):
                    animalNew = new Models.Alpaca();
                    okCategory = ReadMammalInput(animalNew);
                    okSpecies = ReadAlpacaInput(animalNew);
                    break;

                case nameof(EMammalType.Donkey):
                    animalNew = new Models.Donkey();
                    okCategory = ReadMammalInput(animalNew);
                    okSpecies = ReadDonkeyInput(animalNew);
                    break;

                case nameof(EFishType.Fangtooth):
                    animalNew = new Models.Fangtooth();
                    okCategory = ReadFishInput(animalNew);
                    okSpecies = ReadFangtoothInput(animalNew);
                    break;

                case nameof(EFishType.Ray):
                    animalNew = new Models.Ray();
                    okCategory = ReadFishInput(animalNew);
                    okSpecies = ReadRayInput(animalNew);
                    break;

                case nameof(EReptileType.Lizard):
                    animalNew = new Models.Lizard();
                    okCategory = ReadReptileInput(animalNew);
                    okSpecies = ReadLizardInput(animalNew);
                    break;

                case nameof(EReptileType.Turtle):
                    animalNew = new Models.Turtle();
                    okCategory = ReadReptileInput(animalNew);
                    okSpecies = ReadTurtleInput(animalNew);
                    break;
            }

            bool okAnimal = ReadAnimalInput(animalNew);
            if (okAnimal)
                animalNew.Id = GetAnimalOldData()[1];

            if (okAnimal && okCategory && okSpecies && !string.IsNullOrEmpty(animalNew.Id))
            {
                int idx = GetAnimalIndex(animalNew.Id);
                animalManager.ChangeAt(animalNew, idx);

                //Navigate to MainPage so the transient list of animals is refreshed.
                await NavigateTo("../..");
            }
            else
                await Shell.Current.DisplayAlert("Error", "You must enter valid values for all fields!", "Ok");
        }

        //Pre-populates the old animal data in fields, so it's easier for the user to update an animal.
        [RelayCommand]
        private void LoadAnimalOldData()
        {
            IsSpeciesEnabled = false;

            string[] animalData = GetAnimalOldData();
            Animal animalOld = GetAnimalById(animalData[1]);

            if (animalOld is null)
                return;

            AnimalName = animalOld.Name;
            SelectedGender = animalOld.Gender;
            AnimalAge = animalOld.Age.ToString();

            try
            {
                string category = animalData[9];
                SetCategoryProperties(category, animalOld);
            }
            catch (IndexOutOfRangeException ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                return;
            }

            //SelectedSpecies relies on animal.ToString(). It's very important that animalData[15] returns species selection and not something else.
            try
            {
                SelectedSpecies = animalData[15];


                switch (SelectedSpecies)
                {
                    case nameof(EMammalType.Alpaca):
                        WoolType = ((Alpaca)animalOld).WoolType;
                        IsAlpacaShaved = ((Alpaca)animalOld).IsShaved.ToString();
                        break;

                    case nameof(EMammalType.Donkey):
                        DonkeyColor = ((Donkey)animalOld).Color;
                        DonkeyOrigin = ((Donkey)animalOld).Origin;
                        break;

                    case nameof(EFishType.Fangtooth):
                        PreferredDepth = ((Fangtooth)animalOld).Depth.ToString();
                        IsHungry = ((Fangtooth)animalOld).IsHungry.ToString();
                        break;

                    case nameof(EFishType.Ray):
                        HasSpots = ((Ray)animalOld).IsSpotted.ToString();
                        ReproductionType = ((Ray)animalOld).ReproductionType;
                        break;

                    case nameof(EReptileType.Turtle):
                        TurtleHabitat = ((Turtle)animalOld).Habitat;
                        MovementSpeed = ((Turtle)animalOld).Speed.ToString();
                        break;

                    case nameof(EReptileType.Lizard):
                        LizardSize = ((Lizard)animalOld).Size.ToString();
                        IsLizardVenomous = ((Lizard)animalOld).IsVenomous.ToString();
                        break;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                return;
            }
        }

        /// <summary>
        /// Helper method to get the index of the animal to update, based on its ID.
        /// It's needed to satisfy the method signature requirement of ChangeAt(animal, index).
        /// </summary>
        /// <param name="id">animal ID</param>
        /// <returns>index in animal list</returns>
        private int GetAnimalIndex(string id)
        {
            var Animals = GetAnimals();
            if (Animals != null && !string.IsNullOrEmpty(id))
                return Animals.FindIndex(animal => animal.Id == id);
            else
                return -1;
        }

        /// <summary>
        /// Helper method needed to extract animal data from the string query param AnimalToUpdate, coming from ListPage.
        /// </summary>
        /// <returns></returns>
        private string[] GetAnimalOldData()
        {
            char[] charSeparators = new char[] { ' ', '\n' };
            string[] animalData = AnimalToUpdate.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
            return animalData;
        }

        /// <summary>
        /// Populates the form fields with category specific data.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="animal"></param>
        private void SetCategoryProperties(string category, Animal animal)
        {
            if (category == null || animal is null)
                return;
            switch (category)
            {
                case "Mammal":
                    SelectedCategory = ECategoryType.Mammal;
                    MammalHabitatName = ((Mammal)animal).Habitat;
                    MammalActivityTime = ((Mammal)animal).ActivityTime;
                    break;

                case "Reptile":
                    SelectedCategory = ECategoryType.Reptile;
                    NumOfLimbs = ((Reptile)animal).LimbsNumber.ToString();
                    TailLength = ((Reptile)animal).TailLength.ToString();
                    break;

                case "Fish":
                    SelectedCategory = ECategoryType.Fish;
                    AirSelection = ((Fish)animal).CanBreatheAir.ToString();
                    WaterSelection = ((Fish)animal).WaterType;
                    break;
            }
        }

        /// <summary>
        /// Helper method, needed to find the animal to be updated in the animal list managed by animalManager container class.
        /// </summary>
        /// <param name="id">animal ID to find</param>
        /// <returns>animal obj</returns>
        private Animal GetAnimalById(string id)
        {
            var Animals = GetAnimals();
            if (Animals != null && !string.IsNullOrEmpty(id))
                return Animals.Find(animal => animal.Id == id);
            else
                return null;
        }

        /// <summary>
        /// Gets the animal list managed by animalManager container class.
        /// It's needed to satisfy the animal list privacy requirement for the container class.
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
    }
}
