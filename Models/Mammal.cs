namespace WildlifeTrackerSystem.Models
{
    /// <summary>
    /// Mammal is an Animal
    /// </summary>
    [Serializable]
    public class Mammal : Animal
    {
		private string _habitat;
        private string _activityTime;
        private string _id;

        /// <summary>
        /// Property for _habitat
        /// </summary>
        public string Habitat
		{
			get { return _habitat; }
			set { _habitat = value; }
		}

        /// <summary>
        /// Property for _activityTime
        /// </summary>
        public string ActivityTime
		{
			get { return _activityTime; }
			set { _activityTime = value; }
		}

        /// <summary>
        /// Property for animal ID
        /// </summary>
        public override string Id { get => _id; set => _id = value; }

        /// <summary>
        /// Adds mammal specific data to this method
        /// </summary>
        /// <returns></returns>
        public override string GetExtraInfo()
        {
            return $"{base.GetExtraInfo()}mammal \n Habitat: {_habitat} \n Activity Time: {_activityTime}";
        }

        public override string? ToString()
        {
            string text = $"{base.ToString()} \n Category: Mammal \n Habitat: {_habitat} \n ActivityTime: {_activityTime}";
            return text;
        }
    }
}
