using System;
using System.Collections.Generic;
using System.Linq;

namespace SSS.DataFormManager.Views.Helpers
{
    internal class ListHelper
    {
        internal static Type GetListItemType(Type listType)
        {
            Type iListOfT = listType.GetInterfaces().FirstOrDefault(
              (i) => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IList<>));

            return (iListOfT != null)
              ? iListOfT.GetGenericArguments()[0]
              : null;
        }

        internal static object CreateEditableKeyValuePair(object key, Type keyType, object value, Type valueType)
        {
            var itemType = ListHelper.CreateEditableKeyValuePairType(keyType, valueType);
            return Activator.CreateInstance(itemType, key, value);
        }

        internal static Type CreateEditableKeyValuePairType(Type keyType, Type valueType)
        {
            //return an EditableKeyValuePair< TKey, TValue> Type from keyType and valueType
            var itemGenType = typeof(EditableKeyValuePair<,>);
            Type[] itemGenTypeArgs = { keyType, valueType };
            return itemGenType.MakeGenericType(itemGenTypeArgs);
        }
    }
}