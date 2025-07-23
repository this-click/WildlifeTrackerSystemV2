namespace WildlifeTrackerSystem.Models
{
    /// <summary>
    /// Class that represents an animal's meal. It is represented by a name and a list of ingredients (foods).
    /// It's linked to a particular animal selected by the user through AnimalId property.
    /// </summary>
    public class FoodItem
    {
        private string _id;
        private string _name;
        private ListManager<string> _ingredients;

        public FoodItem()
        {
            _ingredients = new ListManager<string>();
        }

        /// <summary>
        /// Property for animal ID so that only the selected animal's food schedule is displayed.
        /// </summary>
        public string AnimalId
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Property for meal name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Property for the list of foods included in a single meal.
        /// </summary>
        public ListManager<string> Ingredients
        {
            get { return _ingredients; }
        }

        /// <summary>
        /// Displays a meal in a nice format, "meal name": "ingredients list" separated by comma
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            string txt = "";
            int count = _ingredients.Count();

            for (int i = 0; i < count; i++)
            {
                txt += _ingredients.ToStringArray()[i];
                //Adds a comma between food items, doesn't add a comma after the last item
                if (i < count - 1)
                    txt += ", ";
            }
            return $"{_name}: {txt} \n";
        }


    }
}
