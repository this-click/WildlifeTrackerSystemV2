namespace WildlifeTrackerSystem.Models
{
    /// <summary>
	/// Lizard is a Reptile and an Animal
	/// </summary>
	[Serializable]
    public class Lizard : Reptile
    {
		private double _size;
        private bool _isVenomous;
        private FoodManager _foodManager;

        public Lizard()
        {
            SetFoodSchedule();
        }


        /// <summary>
        /// Property for _size
        /// </summary>
        public double Size
		{
			get { return _size; }
			set { _size = value; }
		}

        /// <summary>
        /// Property for _isVenomous
        /// </summary>
        public bool IsVenomous
		{
			get { return _isVenomous; }
			set { _isVenomous = value; }
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
        /// Sets food schedule for Lizard. Called from the constructor
        /// </summary>
        private void SetFoodSchedule()
        {
            _foodManager = new FoodManager();
            _foodManager.EaterType = EEaterType.Omnivore;
        }

        /// <summary>
        /// Adds lizard specific info to this method
        /// </summary>
        /// <returns></returns>
        public override string GetExtraInfo()
        {
            return $"{base.GetExtraInfo()} \n\nSpecies: lizard \n Size: {_size} \n Is venomous: {_isVenomous}";
        }

        /// <summary>
        /// Returns the species in string format. Needed in JSON deserialize.
        /// </summary>
        public override string Species { get => nameof(EReptileType.Lizard); }

        public override string? ToString()
        {
            string text = $"{base.ToString()} \n Species: Lizard \n Size: {_size} \n IsVenomous?: {_isVenomous}";
            return text;
        }
    }
}
