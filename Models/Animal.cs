namespace WildlifeTrackerSystem.Models
{
    /// <summary>
    /// Serves as base class for Reptile, Mammal and Fish classes
    /// </summary>
    [Serializable]
    public class Animal : IAnimal
    {
        private string _name;
        private EGenderType _gender;
        private int _age;
        private string species;

        /// <summary>
        /// Property for animal species
        /// </summary>
        public virtual string Species
        {
            get { return species; }
        }


        /// <summary>
        /// Property for animal Id
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// Property for _name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Property for _gender
        /// </summary>
        public EGenderType Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        /// <summary>
        /// Property for _age
        /// </summary>
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        /// <summary>
        /// Prepares a string made of the data that are not included in the Animal class: 
        /// category, species etc. Virtual allows some implementation at this level.
        /// </summary>
        /// <returns>info from category and species classes</returns>
        public virtual string GetExtraInfo()
        {
            return "Category: ";
        }

        /// <summary>
        /// The implementation of this method is provided by the species subclasses.
        /// </summary>
        /// <returns>an object of the FoodSchedule assigned to the particular object</returns>
        public virtual FoodManager GetFoodSchedule() { return null; }

        /// <summary>
        /// Builds the animal object to string.
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            string text = $"\n ID: {Id} \n Name: {_name} \n Gender: {_gender} \n Age: {_age}";
            return text;
        }
    }
}
