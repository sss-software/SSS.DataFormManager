using System.Windows;

namespace SSS.DataFormManager.Views.Helpers.EventArgs
{
    public class ItemDeletingEventArgs : CancelRoutedEventArgs
    {
        #region Private Members

        private object _item;

        #endregion Private Members

        #region Constructor

        public ItemDeletingEventArgs(RoutedEvent itemDeletingEvent, object itemDeleting) : base(itemDeletingEvent)
        {
            _item = itemDeleting;
        }

        #region Property Item

        public object Item
        {
            get
            {
                return _item;
            }
        }

        #endregion Property Item

        #endregion Constructor
    }
}