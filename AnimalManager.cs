using WildlifeTrackerSystem.Models;

namespace WildlifeTrackerSystem
{
    /// <summary>
    /// Container class responsible for CRUD operations on the list of animals. 
    /// Most of its functionality comes from its base class, ListManager, except for setting animal ID. 
    /// </summary>
    public class AnimalManager : ListManager<Animal>
    {
        private int _startId;

        /// <summary>
        /// Add a new animal in the list. Calls the Add method from its base class ListManager.
        /// </summary>
        /// <param name="animal"></param>
        /// <returns>true if animal was added in the list</returns>
        public bool AddAnimal(Animal animal)
        {
            bool okAdd = Add(animal);
            _startId++;
            return okAdd;
        }

        /// <summary>
        /// Generates a new ID for an animal, based on its category: M for Mammal, R for Reptile, F for Fish.
        /// </summary>
        /// <param name="category">Enum</param>
        /// <returns>a new ID</returns>
        public string GetNewId(ECategoryType category)
        {
            string newId = "";

            switch (category)
            {
                case ECategoryType.Mammal:
                    newId = "M" + _startId;
                    break;
                case ECategoryType.Reptile:
                    newId = "R" + _startId;
                    break;
                case ECategoryType.Fish:
                    newId = "F" + _startId;
                    break;
            }
            return newId;
        }
    }
}
