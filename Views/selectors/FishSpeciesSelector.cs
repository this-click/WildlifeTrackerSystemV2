using WildlifeTrackerSystem.Models;

/// <summary>
/// Custom component responsible for conditional rendering the correct fish species view, based on user selection. 
/// It's called in Fish.xaml
/// 
/// Author: SingletonSean
/// https://github.com/SingletonSean/maui-tutorials/tree/master/ConditionalRender
/// 
/// </summary>
namespace WildlifeTrackerSystem.Views.selectors;

public class FishSpeciesSelector : ContentView
{
    public static readonly BindableProperty ConditionProperty =
       BindableProperty.Create(nameof(Condition), typeof(string), typeof(FishSpeciesSelector), "", propertyChanged: OnContentDependentPropertyChanged);

    public string Condition
    {
        get => (string)GetValue(ConditionProperty);
        set => SetValue(ConditionProperty, value);
    }

    public static readonly BindableProperty Option1Property =
        BindableProperty.Create(nameof(Option1), typeof(View), typeof(FishSpeciesSelector), null, propertyChanged: OnContentDependentPropertyChanged);

    public View Option1
    {
        get => (View)GetValue(Option1Property);
        set => SetValue(Option1Property, value);
    }

    public static readonly BindableProperty Option2Property =
        BindableProperty.Create(nameof(Option2), typeof(View), typeof(FishSpeciesSelector), null, propertyChanged: OnContentDependentPropertyChanged);

    public View Option2
    {
        get => (View)GetValue(Option2Property);
        set => SetValue(Option2Property, value);
    }

    public static readonly BindableProperty DefaultOptionProperty =
        BindableProperty.Create(nameof(DefaultOption), typeof(View), typeof(FishSpeciesSelector), null, propertyChanged: OnContentDependentPropertyChanged);

    public View DefaultOption
    {
        get => (View)GetValue(DefaultOptionProperty);
        set => SetValue(DefaultOptionProperty, value);
    }

    private static void OnContentDependentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        FishSpeciesSelector currentSelector = (FishSpeciesSelector)bindable;
        currentSelector.UpdateContent();
    }

    private void UpdateContent()
    {
        if (Condition == Enum.GetName(EFishType.Ray))
        {
            Content = Option1;
        }
        else if (Condition == Enum.GetName(EFishType.Fangtooth))
            Content = Option2;
    }
}