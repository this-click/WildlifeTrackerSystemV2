using Newtonsoft.Json;

namespace WildlifeTrackerSystem
{
    /// <summary>
    /// Generic class that implements the IListManager interface.
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    public class ListManager<T> : IListManager<T>
    {
        private readonly List<T> _list;

        public ListManager()
        {
            _list = new List<T>();
        }

        /// <summary>
        /// Adds a new item in the collection.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if the item was added successfully, false otherwise</returns>
        public bool Add(T item)
        {
            if (item != null && _list != null)
            {
                _list.Add(item);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Changes an item in the collection _list, found at a given index.
        /// </summary>
        /// <param name="newItem">item</param>
        /// <param name="index">index</param>
        /// <returns>true if successful, false otherwise</returns>
        public bool ChangeAt(T newItem, int index)
        {
            bool okItem = true;

            if (newItem != null && _list != null && this.CheckIndex(index))
                _list[index] = newItem;
            else
                okItem = false;
            return okItem;
        }

        /// <summary>
        /// Removes an item from the collection _list, found at a given index.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns>true if successful, false otherwise</returns>
        public bool DeleteAt(T item, int index)
        {
            bool okItem = true;

            if (this.CheckIndex(index))
                _list.RemoveAt(index);
            else
                okItem = false;

            return okItem;
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        /// <returns>true if successful, false otherwise</returns>
        public bool DeleteAll()
        {
            bool okItem = true;

            if (_list != null)
                // Delete ALL items in the list.
                _list.RemoveAll(item => true);
            else
                okItem = false;

            return okItem;
        }

        /// <summary>
        /// Returns an item from the collection _list, found at a given index.
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>an item of type parameter T if index is valid, otherwise returns the default value for the type T</returns>
        public T GetAt(int index)
        {
            if (this.CheckIndex(index))
                return _list[index];
            else return default(T);
        }

        /// <summary>
        /// Returns the number of items in the collection _list, if the list is not null.
        /// Returns 0 otherwise.
        /// </summary>
        public int Count()
        {
            if (_list is null) { return 0; }
            else
                return _list.Count;
        }

        /// <summary>
        /// Returns the collection _list as an array of strings.
        /// Every string represents an item in the collection.
        /// </summary>
        /// <returns>an array of strings</returns>
        public string[] ToStringArray()
        {
            string[] stringArray = new string[this.Count()];

            for (int i = 0; i < this.Count(); i++)
            {
                stringArray[i] = _list[i].ToString();
            }

            return stringArray;
        }

        /// <summary>
        /// Verifies if the given index is valid: has a positive value and doesn't exceed the list's length.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>true if successful, false otherwise</returns>
        public bool CheckIndex(int index)
        {
            if (_list != null)
                return (index >= 0) && (index < this.Count());
            else return false;
        }

        /// <summary>
        /// Converts the list in a JSON string.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string JsonSerialize(List<T> list)
        {
            string? jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(list);

            return jsonString;
        }

        /// <summary>
        /// Converts a JSON string into an object.
        /// Since we don't know the animal species that will be in the JSON string, we return a dynamic object.
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns>a dynamic object</returns>
        public dynamic JsonDeserialize(string jsonString)
        {
            return JsonConvert.DeserializeObject<dynamic>(jsonString);
        }
    }
}
