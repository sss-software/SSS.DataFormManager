using System.Windows;

namespace SSS.DataFormManager.Views.Helpers.EventArgs
{
    public class ItemEventArgs : RoutedEventArgs
    {
        #region Protected Members

        private object _item;

        #endregion Protected Members

        #region Constructor

        internal ItemEventArgs(RoutedEvent routedEvent, object newItem) : base(routedEvent)
        {
            _item = newItem;
        }

        #endregion Constructor

        #region Property Item

        public object Item
        {
            get
            {
                return _item;
            }
        }

        #endregion Property Item
    }
}