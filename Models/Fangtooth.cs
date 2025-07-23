namespace WildlifeTrackerSystem.Models
{
    /// <summary>
	/// Fangtooth is a Fish and an Animal
	/// </summary>
	[Serializable]
    public class Fangtooth : Fish
    {
		private double _depth;
        private bool _isHungry;
        private FoodManager _foodManager;

        public Fangtooth()
        {
            SetFoodSchedule();
        }

        /// <summary>
        /// Property for _depth
        /// </summary>
        public double Depth
		{
			get { return _depth; }
			set { _depth = value; }
		}

		/// <summary>
		/// Property for _isHungry
		/// </summary>
		public bool IsHungry
		{
			get { return _isHungry; }
			set { _isHungry = value; }
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
        /// Sets food schedule for Fangtooth. Called from the constructor
        /// </summary>
        private void SetFoodSchedule()
        {
            _foodManager = new FoodManager();
            _foodManager.EaterType = EEaterType.Carnivore;
        }

        /// <summary>
        /// Adds fangtooth specific info to this method
        /// </summary>
        /// <returns></returns>
        public override string GetExtraInfo()
        {
            return $"{base.GetExtraInfo()} \n\nSpecies: fangtooth \n Preferred depth: {_depth} \n Is Hungry: {_isHungry}";
        }

        /// <summary>
        /// Returns the species in string format. Needed in JSON deserialize.
        /// </summary>
        public override string Species { get => nameof(EFishType.Fangtooth); }

        public override string? ToString()
        {
            string text = $"{base.ToString()} \n Species: Fangtooth \n PreferredDepth: {_depth} \n IsHungry?: {_isHungry}";
            return text;
        }
    }
}
