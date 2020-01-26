using System.Windows;

namespace SSS.DataFormManager.Views.Helpers.EventArgs
{
    public class ItemAddingEventArgs : CancelRoutedEventArgs
    {
        #region Constructor

        public ItemAddingEventArgs(RoutedEvent itemAddingEvent, object itemAdding) : base(itemAddingEvent)
        {
            Item = itemAdding;
        }

        #endregion Constructor

        #region Properties

        #region Item Property

        public object Item
        {
            get;
            set;
        }

        #endregion Item Property

        #endregion Properties
    }
}