namespace WildlifeTrackerSystem.Models
{
    /// <summary>
    /// The animal blueprint that all inheritors must follow.
    /// </summary>
    interface IAnimal
    {
        string Name { get; set; }

        string Id { get; set; }

        EGenderType Gender { get; set; }

        string Species { get;}

        FoodManager GetFoodSchedule();
        string GetExtraInfo();
    }
}
