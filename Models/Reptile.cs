namespace WildlifeTrackerSystem.Models
{
    /// <summary>
    /// Reptile is an Animal
    /// </summary>
    [Serializable]
    public class Reptile : Animal
    {
        private int _numberOfLimbs;
        private double _tailLenghth;
        private string _id;

        /// <summary>
        /// Property for _numberOfLimbs
        /// </summary>
        public int LimbsNumber
        {
            get { return _numberOfLimbs; }
            set { _numberOfLimbs = value; }
        }

        /// <summary>
        /// Property for _tailLenghth
        /// </summary>
        public double TailLength
        {
            get { return _tailLenghth; }
            set { _tailLenghth = value; }
        }

        public override string Id { get => _id; set => _id = value; }

        /// <summary>
        /// Adds reptile specific data to this method
        /// </summary>
        /// <returns></returns>
        public override string GetExtraInfo()
        {
            return $"{base.GetExtraInfo()}reptile \n Number of limbs: {_numberOfLimbs} \n Tail length: {_tailLenghth}";
        }

        public override string? ToString()
        {
            string text = $"{base.ToString()} \n Category: Reptile \n NumberOfLimbs: {_numberOfLimbs} \n TailLength: {_tailLenghth}";
            return text;
        }
    }
}
