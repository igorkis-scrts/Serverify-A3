namespace A3ServerTool.Converters;

[ValueConversion(typeof(bool), typeof(Enum))]
public class EnumToBoolConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return parameter.Equals(value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((bool)value) ? parameter : DependencyProperty.UnsetValue;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}
