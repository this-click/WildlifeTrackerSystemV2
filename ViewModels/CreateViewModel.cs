using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WildlifeTrackerSystem.Models;
using WildlifeTrackerSystem.Views;

namespace WildlifeTrackerSystem.ViewModels
{
    public partial class CreateViewModel : ViewModelBase
    {
        // Type of Animal is determined dynamically
        private Animal currentAnimal;

        // Animal observable properties
        [ObservableProperty]
        private string animalName;

        [ObservableProperty]
        private string animalAge;

        [ObservableProperty]
        private ObservableCollection<EGenderType> genders;

        [ObservableProperty]
        private EGenderType selectedGender;

        // Animal category properties
        [ObservableProperty]
        private ObservableCollection<ECategoryType> categories;

        [ObservableProperty]
        private ECategoryType selectedCategory;

        // Mammal properties
        [ObservableProperty]
        private string mammalHabitatName;

        [ObservableProperty]
        private string mammalActivityTime;

        // Reptile properties
        [ObservableProperty]
        private string numOfLimbs;

        [ObservableProperty]
        private string tailLength;

        // Fish properties
        [ObservableProperty]
        private string airSelection;

        [ObservableProperty]
        private string waterSelection;

        // SelectedSpecies property, declared as string so I can have just 1 variable for all possible animal categories
        [ObservableProperty]
        private string selectedSpecies;

        // Donkey properties
        [ObservableProperty]
        private string donkeyColor;

        [ObservableProperty]
        private string donkeyOrigin;

        // Alpaca properties
        [ObservableProperty]
        private string woolType;

        [ObservableProperty]
        private string isAlpacaShaved;

        // Fangtooth properties
        [ObservableProperty]
        private string isHungry;

        [ObservableProperty]
        private string preferredDepth;

        // Ray properties
        [ObservableProperty]
        private string hasSpots;

        [ObservableProperty]
        private string reproductionType;

        // Turtle properties
        [ObservableProperty]
        private string turtleHabitat;

        [ObservableProperty]
        private string movementSpeed;

        // Lizard properties
        [ObservableProperty]
        private string lizardSize;

        [ObservableProperty]
        private string isLizardVenomous;

        // Summary container properties
        [ObservableProperty]
        private string summaryText;

        [ObservableProperty]
        private bool isSummaryVisible = false;

        //Property used for restricting the user from updating the selected species and category. 
        [ObservableProperty]
        private bool isSpeciesEnabled = true;

        [ObservableProperty]
        private ImageSource imageSrc;

        private readonly AnimalManager animalManager;
        private readonly FileManager fileManager;

        public CreateViewModel(AnimalManager animalManager, FileManager fileManager)
        {
            //Initialize Gender picker with values from EGenders enum.
            //Convert from Array returned by Enum.GetValues to IEnumerable needed by the ObservableCollection
            Genders = new ObservableCollection<EGenderType>(Enum.GetValues(typeof(EGenderType)).Cast<EGenderType>());
            Categories = new ObservableCollection<ECategoryType>(Enum.GetValues(typeof(ECategoryType)).Cast<ECategoryType>());

            // Managers are injected as dependencies, this is constructor dependency injection. 
            this.animalManager = animalManager;
            this.fileManager = fileManager;

            // Display default picture, before the user selects their own from disk.
            ImageSrc = ImageSource.FromFile("koala.jpg");

        }

        public CreateViewModel(AnimalManager animalManager)
        {
            //Initialize Gender picker with values from EGenders enum.
            //Convert from Array returned by Enum.GetValues to IEnumerable needed by the ObservableCollection
            Genders = new ObservableCollection<EGenderType>(Enum.GetValues(typeof(EGenderType)).Cast<EGenderType>());
            Categories = new ObservableCollection<ECategoryType>(Enum.GetValues(typeof(ECategoryType)).Cast<ECategoryType>());

            // Animal Manager is injected as dependencies, this is constructor dependency injection. 
            this.animalManager = animalManager;

        }

        /// <summary>
        /// Saves all the values entered by the user in the appropriate objects, following the class hierarchy: 
        /// Animal has Mammal, Reptile or Fish categories. 
        /// Each category has 2 species, e.g. Alpaca is a Mammal which in turn is an Animal.
        /// 
        /// Gets called when user clicks on Save button.
        /// </summary>
        [RelayCommand]
        private void SaveAllUserInput()
        {
            bool okAnimal = false, okCategory = false;

            switch (SelectedCategory)
            {
                case ECategoryType.Mammal:
                    okCategory = SaveMammalSpeciesAndCategory();
                    break;
                case ECategoryType.Reptile:
                    okCategory = SaveReptileSpeciesAndCategory();
                    break;
                case ECategoryType.Fish:
                    okCategory = SaveFishSpeciesAndCategory();
                    break;
            }

            //Checks if user tries to save an animal with empty data.
            if (currentAnimal is null)
                Shell.Current.DisplayAlert("Error", "You must enter valid values for all fields!", "Ok");
            else
            {
                okAnimal = ReadAnimalInput(currentAnimal);

                if (okAnimal && okCategory)
                {
                    //Add the new animal to the container class AnimalManager. This will also increment ID
                    animalManager.AddAnimal(currentAnimal);

                    //Set summary container to Visible, only if all inputs are valid
                    IsSummaryVisible = true;
                    SummaryText = currentAnimal.ToString();

                    ResetUserInput();
                }
                else
                    Shell.Current.DisplayAlert("Error", "You must enter valid values for all fields!", "Ok");
            }
        }

        /// <summary>
        /// Navigates to list animals page when user clicks on List animals button
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task List()
        {
            await NavigateTo(nameof(ListPage));
        }

        /// <summary>
        /// Selects picture from user's local storage. 
        /// </summary>
        [RelayCommand]
        private async Task UploadNewPicture()
        {
            try
            {
                ImageSrc = await fileManager.ReadProfilePic();
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong, please try again.", "Ok");
            }
        }

        /// <summary>
        /// Clears all entry fields.
        /// </summary>
        private void ResetUserInput()
        {
            AnimalName = string.Empty;
            AnimalAge = string.Empty;

            if (currentAnimal != null)
            {
                if (currentAnimal is Mammal)
                {
                    MammalHabitatName = string.Empty;
                    MammalActivityTime = string.Empty;

                    if (currentAnimal is Models.Donkey)
                    {
                        DonkeyColor = string.Empty;
                        DonkeyOrigin = string.Empty;
                    }
                }

                if (currentAnimal is Reptile)
                {
                    TailLength = string.Empty;
                    NumOfLimbs = string.Empty;

                    if (currentAnimal is Models.Turtle)
                    {
                        TurtleHabitat = string.Empty;
                        MovementSpeed = string.Empty;
                    }

                    if (currentAnimal is Models.Lizard)
                        LizardSize = string.Empty;
                }

                if (currentAnimal is Models.Ray)
                    ReproductionType = string.Empty;

                if (currentAnimal is Models.Fangtooth)
                    PreferredDepth = string.Empty;
            }

            // Resets current animal object.
            currentAnimal = null;
        }

        /// <summary>
        /// Saves the fish species and category user input in currentAnimal
        /// </summary>
        /// <returns>true id user input valid</returns>
        private bool SaveFishSpeciesAndCategory()
        {
            bool okSpecies = false, okFish = false;

            if (SelectedSpecies == Enum.GetName(EFishType.Fangtooth))
            {
                currentAnimal = new Models.Fangtooth();
                okSpecies = ReadFangtoothInput(currentAnimal);
            }

            if (SelectedSpecies == Enum.GetName(EFishType.Ray))
            {
                currentAnimal = new Models.Ray();
                okSpecies = ReadRayInput(currentAnimal);
            }

            if (currentAnimal != null)
            {
                okFish = ReadFishInput(currentAnimal);
                currentAnimal.Id = animalManager.GetNewId(ECategoryType.Fish);
            }


            return okSpecies && okFish;
        }

        /// <summary>
        /// Saves the reptile species and category user input in currentAnimal.
        /// </summary>
        /// <returns>true id user input valid</returns>
        private bool SaveReptileSpeciesAndCategory()
        {
            bool okSpecies = false, okReptile = false;

            if (SelectedSpecies == Enum.GetName(EReptileType.Turtle))
            {
                currentAnimal = new Models.Turtle();
                okSpecies = ReadTurtleInput(currentAnimal);
            }

            if (SelectedSpecies == Enum.GetName(EReptileType.Lizard))
            {
                currentAnimal = new Models.Lizard();
                okSpecies = ReadLizardInput(currentAnimal);
            }

            if (currentAnimal != null)
            {
                okReptile = ReadReptileInput(currentAnimal);
                currentAnimal.Id = animalManager.GetNewId(ECategoryType.Reptile);
            }

            return okSpecies && okReptile;
        }

        /// <summary>
        /// Read and validate the mammal species properties. This will also assign ID.
        /// </summary>
        /// <returns>true id user input valid</returns>
        private bool SaveMammalSpeciesAndCategory()
        {
            bool okSpecies = false, okMammal = false;

            if (SelectedSpecies == Enum.GetName(EMammalType.Alpaca))
            {
                currentAnimal = new Models.Alpaca();
                okSpecies = ReadAlpacaInput(currentAnimal);
            }

            if (SelectedSpecies == Enum.GetName(EMammalType.Donkey))
            {
                currentAnimal = new Models.Donkey();
                okSpecies = ReadDonkeyInput(currentAnimal);
            }

            if (currentAnimal != null)
            {
                okMammal = ReadMammalInput(currentAnimal);
                currentAnimal.Id = animalManager.GetNewId(ECategoryType.Mammal);
            }

            return okSpecies && okMammal;
        }

        /// <summary>
        /// Saves the fangtooth specific user input in currentAnimal object
        /// </summary>
        protected bool ReadFangtoothInput(Animal currentAnimal)
        {
            bool isValid = true;

            if (!string.IsNullOrEmpty(PreferredDepth))
            {
                bool okDepth = double.TryParse(PreferredDepth, out double parsedDepth);
                if (okDepth && parsedDepth >= 0)
                    ((Models.Fangtooth)currentAnimal).Depth = parsedDepth;
                else
                    isValid = false;
            }
            else
                isValid = false;

            if (!string.IsNullOrEmpty(IsHungry))
            {
                if (IsHungry.StartsWith("True"))
                    ((Models.Fangtooth)currentAnimal).IsHungry = true;
                else
                    ((Models.Fangtooth)currentAnimal).IsHungry = false;
            }
            else
                isValid = false;

            return isValid;
        }

        /// <summary>
        /// Saves the ray specific user input in currentAnimal object
        /// </summary>
        protected bool ReadRayInput(Animal currentAnimal)
        {
            bool isValid = true;

            if (!string.IsNullOrEmpty(HasSpots))
            {
                if (HasSpots.StartsWith("True"))
                    ((Models.Ray)currentAnimal).IsSpotted = true;
                else
                    ((Models.Ray)currentAnimal).IsSpotted = false;
            }

            else { isValid = false; }

            if (!string.IsNullOrEmpty(ReproductionType))
                ((Models.Ray)currentAnimal).ReproductionType = ReproductionType;
            else
                isValid = false;

            return isValid;
        }

        /// <summary>
        /// Saves the lizard specific user input in currentAnimal object
        /// </summary>
        protected bool ReadLizardInput(Animal currentAnimal)
        {
            bool isValid = true;

            if (!string.IsNullOrEmpty(LizardSize))
            {
                bool okSize = double.TryParse(LizardSize, out double parsedSize);
                if (okSize && parsedSize >= 0)
                    ((Models.Lizard)currentAnimal).Size = parsedSize;
                else
                    isValid = false;
            }
            else
                isValid = false;

            if (!string.IsNullOrEmpty(IsLizardVenomous))
                if (IsLizardVenomous.StartsWith("True"))
                    ((Models.Lizard)currentAnimal).IsVenomous = true;
                else
                    ((Models.Lizard)currentAnimal).IsVenomous = false;

            return isValid;
        }

        /// <summary>
        /// Saves the turtle specific user input in currentAnimal object
        /// </summary>
        protected bool ReadTurtleInput(Animal currentAnimal)
        {
            bool isValid = true;

            if (!string.IsNullOrEmpty(TurtleHabitat))
                ((Models.Turtle)currentAnimal).Habitat = TurtleHabitat;
            else isValid = false;

            if (!string.IsNullOrEmpty(MovementSpeed))
            {
                bool okSpeed = double.TryParse(MovementSpeed, out double speed);
                if (okSpeed && speed >= 0)
                    ((Models.Turtle)currentAnimal).Speed = speed;
                else
                    isValid = false;
            }
            else
                isValid = false;

            return isValid;
        }

        /// <summary>
        /// Read and validate alpaca specific user input.
        /// </summary>
        protected bool ReadAlpacaInput(Animal currentAnimal)
        {
            bool isValid = true;

            string SURI = "Suri";
            string HUACAYA = "Huacaya";

            if (!string.IsNullOrEmpty(WoolType))
            {
                if (WoolType.Equals(SURI))
                    ((Models.Alpaca)currentAnimal).WoolType = SURI;
                else if (WoolType.Equals(HUACAYA))
                    ((Models.Alpaca)currentAnimal).WoolType = HUACAYA;
            }

            else
            {
                isValid = false;
            }

            if (!string.IsNullOrEmpty(IsAlpacaShaved))
            {
                if (IsAlpacaShaved.StartsWith("True"))
                    ((Models.Alpaca)currentAnimal).IsShaved = true;
                else
                    ((Models.Alpaca)currentAnimal).IsShaved = false;
            }
            else
                isValid = false;

            return isValid;
        }

        /// <summary>
        /// Read and validate fish specific user input.
        /// </summary>
        protected bool ReadFishInput(Animal currentAnimal)
        {
            bool isValid = true;

            if (!string.IsNullOrEmpty(AirSelection))
            {
                if (AirSelection.StartsWith("True"))
                    ((Models.Fish)currentAnimal).CanBreatheAir = true;
                else
                    ((Models.Fish)currentAnimal).CanBreatheAir = false;
            }
            else
                isValid = false;

            if (!string.IsNullOrEmpty(WaterSelection))
                ((Models.Fish)currentAnimal).WaterType = WaterSelection;
            else
                isValid = false;

            return isValid;
        }

        /// <summary>
        /// Saves the reptile specific user input in currentAnimal object
        /// </summary>
        protected bool ReadReptileInput(Animal currentAnimal)
        {
            bool isValid = true;

            if (!string.IsNullOrEmpty(NumOfLimbs))
            {
                bool okLimbs = int.TryParse(NumOfLimbs, out int parsedNumberOfLimbs);
                if (okLimbs && parsedNumberOfLimbs >= 0)
                    ((Reptile)currentAnimal).LimbsNumber = parsedNumberOfLimbs;
                else
                    isValid = false;
            }
            else
                isValid = false;

            if (!string.IsNullOrEmpty(TailLength))
            {
                bool okTail = double.TryParse(TailLength, out double parsedTailLenght);
                if (okTail && parsedTailLenght >= 0)
                    ((Reptile)currentAnimal).TailLength = parsedTailLenght;
                else
                    isValid = false;
            }
            else
                isValid = false;

            return isValid;
        }

        /// <summary>
        /// Read and validate donkey specific user input.
        /// </summary>
        protected bool ReadDonkeyInput(Animal currentAnimal)
        {
            bool isDonkeyValid = true;

            if (!string.IsNullOrEmpty(DonkeyColor))
                ((Models.Donkey)currentAnimal).Color = DonkeyColor;
            else isDonkeyValid = false;

            if (!string.IsNullOrEmpty(DonkeyOrigin))
                ((Models.Donkey)currentAnimal).Origin = DonkeyOrigin;
            else isDonkeyValid = false;

            return isDonkeyValid;
        }

        /// <summary>
        /// Save in currentAnimal obj the animal name, age and gender. 
        /// The observable property generator initializes properties and performs some null checks, 
        /// see [global::System.Diagnostics.CodeAnalysis.MemberNotNull("animalName")] in MainViewModel.g.cs
        /// </summary>
        /// <param name="currentAnimal"></param>
        /// <returns>true if animal properties were valid, false otherwise</returns>
        protected bool ReadAnimalInput(Animal currentAnimal)
        {
            //Checks if user is trying to update an animal with empty data.
            if (currentAnimal is null)
                return false;
            else
            {
                bool isValid = true;

                if (!string.IsNullOrEmpty(AnimalName))
                    currentAnimal.Name = AnimalName;
                else
                    isValid = false;

                if (!string.IsNullOrEmpty(AnimalAge))
                {
                    bool okAge = int.TryParse(AnimalAge, out int age);
                    if (okAge && age >= 0)
                        currentAnimal.Age = age;
                    else isValid = false;
                }
                else isValid = false;

                //No validation is necessary because the default value of SelectedGender is ALWAYS "Unknown", index 0, set by the MVVM Community Toolkit
                //The only way this would crash is if currentAnimal is null, for which check is already in place, ln 549.
                currentAnimal.Gender = SelectedGender;

                return isValid;
            }
        }

        /// <summary>
        /// Read and validate mammal specific user input.
        /// </summary>
        protected bool ReadMammalInput(Animal currentAnimal)
        {
            bool isValid = true;

            if (!string.IsNullOrEmpty(MammalHabitatName))
                ((Mammal)currentAnimal).Habitat = MammalHabitatName;
            else isValid = false;

            if (!string.IsNullOrEmpty(MammalActivityTime))
                ((Mammal)currentAnimal).ActivityTime = MammalActivityTime;
            else isValid = false;

            return isValid;
        }
    }
}
