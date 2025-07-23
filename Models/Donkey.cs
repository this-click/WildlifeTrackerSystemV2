namespace WildlifeTrackerSystem.Models
{
    /// <summary>
    /// Donkey is a Mammal and an Animal
    /// </summary>
    [Serializable]
    public class Donkey : Mammal
    {
		private string _color;
        private string _origin;
        private FoodManager _foodManager;

        public Donkey()
        {
            SetFoodSchedule();
        }

        /// <summary>
        /// Property for _color
        /// </summary>
        public string Color
		{
			get { return _color; }
			set { _color = value; }
		}

		/// <summary>
		/// Property for _origin
		/// </summary>
		public string Origin
		{
			get { return _origin; }
			set { _origin = value; }
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
        /// Sets food schedule for donkey. Called from the constructor
        /// </summary>
        private void SetFoodSchedule()
        {
            _foodManager = new FoodManager();
            _foodManager.EaterType = EEaterType.Herbivore;
        }

        /// <summary>
        /// Adds donkey specific info to this method
        /// </summary>
        /// <returns></returns>
        public override string GetExtraInfo()
        {
            return $"{base.GetExtraInfo()} \n\nSpecies: donkey \n Color: {_color} \n Origin: {_origin}";
        }

        /// <summary>
        /// Returns the species in string format. Needed in JSON deserialize.
        /// </summary>
        public override string Species { get => nameof(EMammalType.Donkey); }

        public override string? ToString()
        {
            string text = $"{base.ToString()} \n Species: Donkey \n Color: {_color} \n Origin: {_origin}";
            return text;
        }
    }
}
