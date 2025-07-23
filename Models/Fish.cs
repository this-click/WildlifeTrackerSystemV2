namespace WildlifeTrackerSystem.Models
{

    /// <summary>
    /// Fish is an Animal
    /// </summary>
    [Serializable]
    public class Fish : Animal
    {
		private bool _canBreatheAir;
        private string _waterType;
        private string _id;

        /// <summary>
        ///  Property for _canBreatheAir
        /// </summary>
        public bool CanBreatheAir
		{
			get { return _canBreatheAir; }
			set { _canBreatheAir = value; }
		}

        /// <summary>
        /// Property for _waterType
        /// </summary>
        public string WaterType
		{
			get { return _waterType; }
			set { _waterType = value; }
		}

        public override string Id { get => _id; set => _id = value; }

        /// <summary>
        /// Adds fish specific data to this method
        /// </summary>
        /// <returns></returns>
        public override string GetExtraInfo()
        {
            return $"{base.GetExtraInfo()}fish \n Can breathe air: {_canBreatheAir} \n Lives in: {_waterType}";
        }

        public override string? ToString()
        {
            string text = $"{base.ToString()} \n Category: Fish \n CanBreatheAir: {_canBreatheAir} \n LivesIn: {_waterType}";
            return text;
        }
    }
}
