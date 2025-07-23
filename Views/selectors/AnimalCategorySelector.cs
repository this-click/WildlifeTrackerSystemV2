namespace WildlifeTrackerSystem.Views.selectors;

/// <summary>
/// Custom component responsible for conditional rendering the correct animal category, based on user selection. 
/// For example, if user selects Mammal animal category, only the Mammal View will be rendered.
/// It's called in MainPage.xaml
/// 
/// Author: SingletonSean
/// https://github.com/SingletonSean/maui-tutorials/tree/master/ConditionalRender
/// 
/// </summary>
public class AnimalCategorySelector : ContentView
{
    public static readonly BindableProperty ConditionProperty =
        BindableProperty.Create(nameof(Condition), typeof(int), typeof(AnimalCategorySelector), 0, propertyChanged: OnContentDependentPropertyChanged);

    public int Condition
    {
        get => (int)GetValue(ConditionProperty);
        set => SetValue(ConditionProperty, value);
    }

    public static readonly BindableProperty Option1Property =
        BindableProperty.Create(nameof(Option1), typeof(View), typeof(AnimalCategorySelector), null, propertyChanged: OnContentDependentPropertyChanged);

    public View Option1
    {
        get => (View)GetValue(Option1Property);
        set => SetValue(Option1Property, value);
    }

    public static readonly BindableProperty Option2Property =
        BindableProperty.Create(nameof(Option2), typeof(View), typeof(AnimalCategorySelector), null, propertyChanged: OnContentDependentPropertyChanged);

    public View Option2
    {
        get => (View)GetValue(Option2Property);
        set => SetValue(Option2Property, value);
    }

    public static readonly BindableProperty Option3Property =
        BindableProperty.Create(nameof(Option3), typeof(View), typeof(AnimalCategorySelector), null, propertyChanged: OnContentDependentPropertyChanged);

    public View Option3
    {
        get => (View)GetValue(Option3Property);
        set => SetValue(Option3Property, value);
    }

    public static readonly BindableProperty DefaultOptionProperty =
        BindableProperty.Create(nameof(DefaultOption), typeof(View), typeof(AnimalCategorySelector), null, propertyChanged: OnContentDependentPropertyChanged);

    public View DefaultOption
    {
        get => (View)GetValue(DefaultOptionProperty);
        set => SetValue(DefaultOptionProperty, value);
    }

    private static void OnContentDependentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        AnimalCategorySelector currentSelector = (AnimalCategorySelector)bindable;
        currentSelector.UpdateContent();
    }

    private void UpdateContent()
    {
        switch (Condition)
        {
            case 0:
                Content = Option1;
                break;
            case 1:
                Content = Option2;
                break;
            case 2:
                Content = Option3;
                break;
            default:
                Content = DefaultOption;
                break;
        }
    }
}
