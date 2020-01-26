using System;
using System.ComponentModel;

namespace TestProject
{
    // It converts an ListEntry object to string representation for use in a property grid.
    internal class ListEntryConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
        {
            if (destType == typeof(string) && value is ListEntry)
            {
                // Cast the value to an Employee type
                ListEntry emp = (ListEntry)value;

                // Return department and department role separated by comma.
                return emp.Key + ", " + emp.Value;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    // This is a special type converter which will be associated with the EmployeeCollection class.
    // It converts an EmployeeCollection object to a string representation for use in a property grid.
    internal class ListEntryCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
        {
            if (destType == typeof(string) && value is ListEntryCollection)
            {
                // Return department and department role separated by comma.
                return "Control List Entries";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}