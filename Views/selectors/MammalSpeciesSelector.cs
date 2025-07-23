using WildlifeTrackerSystem.Models;

namespace WildlifeTrackerSystem.Views.selectors;

/// <summary>
/// Custom component responsible for conditional rendering the correct mammal species view, based on user selection. 
/// It's called in Mammals.xaml
/// 
/// Author: SingletonSean
/// https://github.com/SingletonSean/maui-tutorials/tree/master/ConditionalRender
/// 
/// </summary>
public class MammalSpeciesSelector : ContentView
{
    public static readonly BindableProperty ConditionProperty =
         BindableProperty.Create(nameof(Condition), typeof(string), typeof(MammalSpeciesSelector), "", propertyChanged: OnContentDependentPropertyChanged);

    public string Condition
    {
        get => (string)GetValue(ConditionProperty);
        set => SetValue(ConditionProperty, value);
    }

    public static readonly BindableProperty Option1Property =
        BindableProperty.Create(nameof(Option1), typeof(View), typeof(MammalSpeciesSelector), null, propertyChanged: OnContentDependentPropertyChanged);

    public View Option1
    {
        get => (View)GetValue(Option1Property);
        set => SetValue(Option1Property, value);
    }

    public static readonly BindableProperty Option2Property =
        BindableProperty.Create(nameof(Option2), typeof(View), typeof(MammalSpeciesSelector), null, propertyChanged: OnContentDependentPropertyChanged);

    public View Option2
    {
        get => (View)GetValue(Option2Property);
        set => SetValue(Option2Property, value);
    }

    public static readonly BindableProperty DefaultOptionProperty =
        BindableProperty.Create(nameof(DefaultOption), typeof(View), typeof(MammalSpeciesSelector), null, propertyChanged: OnContentDependentPropertyChanged);

    public View DefaultOption
    {
        get => (View)GetValue(DefaultOptionProperty);
        set => SetValue(DefaultOptionProperty, value);
    }

    private static void OnContentDependentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        MammalSpeciesSelector currentSelector = (MammalSpeciesSelector)bindable;
        currentSelector.UpdateContent();
    }

    private void UpdateContent()
    {
        if (Condition == Enum.GetName(EMammalType.Alpaca))
        {
            Content = Option1;
        }
        else if (Condition == Enum.GetName(EMammalType.Donkey))
            Content = Option2;
    }
}