using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SSS.DataFormManager.Views.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value != null) && (value is bool))
            {
                if ((bool)value)
                {
                    return Visibility.Visible;
                }
                else
                    return Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}