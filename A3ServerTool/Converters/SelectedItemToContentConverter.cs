using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace A3ServerTool.Converters
{
    public class SelectedItemToContentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targeTypes, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length > 1)
            {
                return values[0] ?? values[1];
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return targetTypes.Select(t => Binding.DoNothing).ToArray();
        }
    }
}
