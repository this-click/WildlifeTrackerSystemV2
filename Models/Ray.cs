namespace WildlifeTrackerSystem.Models
{
    /// <summary>
	/// Ray is a Fish and an Animal
	/// </summary>
	[Serializable]
    public class Ray : Fish
    {
		private bool _isSpotted;
        private string _reproductionType;
        private FoodManager _foodManager;

        public Ray()
        {
            SetFoodSchedule();
        }

        /// <summary>
        /// Property for _isSpotted
        /// </summary>
        public bool IsSpotted
		{
			get { return _isSpotted; }
			set { _isSpotted = value; }
		}

        /// <summary>
        /// Property for _reproductionType
        /// </summary>
        public string ReproductionType
		{
			get { return _reproductionType; }
			set { _reproductionType = value; }
		}

        /// <summary>
        /// Gets the food schedule for the animal
        /// </summary>
        /// <returns>a list of food items</returns>
        public override FoodManager GetFoodSchedule()
        {
            return _foodManager;
        }

        /// <summary>
        /// Sets food schedule for Ray. Called from the constructor
        /// </summary>
        private void SetFoodSchedule()
        {
            _foodManager = new FoodManager();
            _foodManager.EaterType = EEaterType.Carnivore;
        }

        /// <summary>
        /// Adds ray specific info to this method
        /// </summary>
        /// <returns></returns>
        public override string GetExtraInfo()
        {
            return $"{base.GetExtraInfo()} \n\nSpecies: ray \n Has spots: {_isSpotted} \n Reproduces by: {_reproductionType}";
        }

        /// <summary>
        /// Returns the species in string format. Needed in JSON deserialize.
        /// </summary>
        public override string Species { get => nameof(EFishType.Ray); }

        public override string? ToString()
        {
            string text = $"{base.ToString()} \n Species: Ray \n HasSpots?: {_isSpotted} \n ReproducesBy: {_reproductionType} ";
            return text;
        }
    }
}
