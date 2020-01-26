using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace SSS.DataFormManager.Views.Helpers
{
    public static class DependencyObjectHelper
    {
        public static List<T> FindChildrenByType<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            List<T> children = new List<T>();
            if (dependencyObject != null)
            {
                for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(dependencyObject) - 1; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i);
                    if (child != null && child is T)
                    {
                        children.Add((T)child);
                    }
                    children.AddRange(FindChildrenByType<T>(child));
                }
            }
            return children;
        }
    }
}