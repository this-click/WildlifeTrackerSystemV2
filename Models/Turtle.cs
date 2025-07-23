namespace WildlifeTrackerSystem.Models
{
    /// <summary>
	/// Turtle is a Reptile and an Animal
	/// </summary>
	[Serializable]
    public class Turtle : Reptile
    {
		private string _habitat;
        private double _speed;
        private FoodManager _foodManager;

        public Turtle()
        {
            SetFoodSchedule();
        }


        /// <summary>
        /// Property for _habitat
        /// </summary>
        public string Habitat
		{
			get { return _habitat; }
			set { _habitat = value; }
		}

        /// <summary>
        /// Property for _speed
        /// </summary>
        public double Speed
		{
			get { return _speed; }
			set { _speed = value; }
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
        /// Sets food schedule for Turtle. Called from the constructor
        /// </summary>
        private void SetFoodSchedule()
        {
            _foodManager = new FoodManager();
            _foodManager.EaterType = EEaterType.Omnivore;
        }

        /// <summary>
        /// Adds turtle specific info to this method
        /// </summary>
        /// <returns></returns>
        public override string GetExtraInfo()
        {
            return $"{base.GetExtraInfo()} \n\nSpecies: turtle \n Habitat: {_habitat} \n Movement speed: {_speed}";
        }

        /// <summary>
        /// Returns the species in string format. Needed in JSON deserialize.
        /// </summary>
        public override string Species { get => nameof(EReptileType.Turtle); }

        public override string? ToString()
        {
            string text = $"{base.ToString()} \n Species: Turtle \n Habitat: {_habitat} \n MovementSpeed: {_speed} ";
            return text;
        }
    }
}
