namespace WildlifeTrackerSystem
{
    /// <summary>
    /// Interface for manager classes to implement.
    /// Hosts a collection of the type List<typeparamref name="T"/> where T can be any object type. 
    /// 
    /// In this documentation, the collection is referred to as my_list.
    /// </summary>
    /// <typeparam name="T">object type</typeparam>
    public interface IListManager<T>
    {
        /// <summary>
        /// Returns the number of items in the collection my_list.
        /// </summary>
        public int Count();

        /// <summary>
        /// Adds an item to the collection my_list.
        /// </summary>
        /// <param name="item">an item</param>
        /// <returns>true if successful, false otherwise</returns>
        public bool Add(T item);

        /// <summary>
        /// Changes an item in the collection my_list, found at a given index.
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="index">index</param>
        /// <returns>true if successful, false otherwise</returns>
        public bool ChangeAt(T item, int index);

        /// <summary>
        /// Removes an item from the collection my_list, found at a given index.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool DeleteAt(T item, int index);

        /// <summary>
        /// Removes all the items from the collection my_list.
        /// </summary>
        /// <returns>true if successful, false otherwise</returns>
        public bool DeleteAll();

        /// <summary>
        /// Returns an item from the collection my_list, found at a given index.
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>an item of type parameter T</returns>
        public T GetAt(int index);

        /// <summary>
        /// Returns the collection my_list as an array of strings.
        /// Every string represents an item in the collection.
        /// </summary>
        /// <returns>an array of strings</returns>
        public string[] ToStringArray();

        /// <summary>
        /// Verifies if item at index is valid.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool CheckIndex(int index);

        /// <summary>
        /// Serialize to JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string JsonSerialize(List<T> obj);

        /// <summary>
        /// Deserialize to JSON
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        dynamic JsonDeserialize(string txt);
    }
}
