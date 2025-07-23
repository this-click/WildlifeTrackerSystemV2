namespace WildlifeTrackerSystem.Models
{
    /// <summary>
    /// Alpaca is a Mammal and an Animal
    /// </summary>

    [Serializable]
    public class Alpaca : Mammal
    {
        private string _woolType;
        private bool _isShaved;
        private FoodManager _foodManager;

        public Alpaca()
        {
            SetFoodSchedule();
        }

        /// <summary>
        /// Property for _woolType with read/write access
        /// </summary>
        public string WoolType
        {
            get { return _woolType; }
            set { _woolType = value; }
        }

        /// <summary>
        /// Property for _isShaved with read/write access
        /// </summary>
        public bool IsShaved
        {
            get { return _isShaved; }
            set { _isShaved = value; }
        }

        /// <summary>
        /// Returns the species in string format. Needed in JSON deserialize.
        /// </summary>
        public override string Species { get => nameof(EMammalType.Alpaca); }

        /// <summary>
        /// Sets food schedule for alpaca. Called from the constructor
        /// </summary>
        private void SetFoodSchedule()
        {
            _foodManager = new FoodManager();
            _foodManager.EaterType = EEaterType.Herbivore;
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
        /// Adds alpaca specific info to this method
        /// </summary>
        /// <returns></returns>
        public override string GetExtraInfo()
        {
            return $"{base.GetExtraInfo()} \n\nSpecies: alpaca \n Wool type: {_woolType} \n Is shaved: {_isShaved}";
        }

        public override string? ToString()
        {
            string text = $"{base.ToString()} \n Species: Alpaca \n WoolType: {_woolType} \n IsShaved?: {_isShaved}";
            return text;
        }
    }
}
