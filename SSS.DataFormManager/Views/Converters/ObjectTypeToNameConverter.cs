using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SSS.DataFormManager.Views.Converters
{
    public class ObjectTypeToNameConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value is Type)
                {
                    var displayNameAttribute = ((Type)value).GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault();
                    var x = (displayNameAttribute != null) ? displayNameAttribute.DisplayName : ((Type)value).Name;
                    return (displayNameAttribute != null) ? displayNameAttribute.DisplayName : ((Type)value).Name;
                }

                var type = value.GetType();
                var valueString = value.ToString();
                if (string.IsNullOrEmpty(valueString)
                 || (valueString == type.UnderlyingSystemType.ToString()))
                {
                    var displayNameAttribute = type.GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault();
                    return (displayNameAttribute != null) ? displayNameAttribute.DisplayName : type.Name;
                }
                return value;
            }
            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}