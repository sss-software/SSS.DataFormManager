using SSS.DataFormManager.Views.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SSS.DataFormManager.Views.Converters
{
    public class NewItemTypesComboBoxConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
                throw new ArgumentException("The 'values' argument should contain 2 objects.");

            if (values[1] != null)
            {
                if (!values[1].GetType().IsGenericType || !(values[1].GetType().GetGenericArguments().First().GetType() is Type))
                    throw new ArgumentException("The 'value' argument is not of the correct type.");

                return values[1];
            }
            else if (values[0] != null)
            {
                if (!(values[0].GetType() is Type))
                    throw new ArgumentException("The 'value' argument is not of the correct type.");

                List<Type> types = new List<Type>();
                Type listType = ListHelper.GetListItemType((Type)values[0]);
                if (listType != null)
                {
                    types.Add(listType);
                }

                return types;
            }
            return null;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}