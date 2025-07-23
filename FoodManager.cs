using WildlifeTrackerSystem.Models;

namespace WildlifeTrackerSystem
{
    /// <summary>
    /// Container class responsible for displaying a list of meals for the selected animal.
    /// Most of its functionality comes from its base class, ListManager, except for establishing EaterType.
    /// 
    /// </summary>
    public class FoodManager : ListManager<FoodItem>
    {
        private EEaterType _eaterType;
        private List<string> _foodList;

        public FoodManager()
        {
            _foodList = new List<string>();
        }

        /// <summary>
        /// Property for type of eater: omnivore, carnivore, herbivore.
        /// </summary>
        public EEaterType EaterType
        {
            get { return _eaterType; }
            set { _eaterType = value; }
        }
    }
}
