using SSS.DataFormManager.Views.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Point = System.Windows.Point;

namespace SSS.DataFormManager.Views.Controls.CustomControls.DraggablePopupCustomControl
{
    public class DraggablePopupCustomControl : Popup
    {
        private Point _initialMousePosition;
        private bool _isDragging;
        private DataFormListEntryCollection _DataFormListEntryCollection;

        public DraggablePopupCustomControl()
        {
            NewItemTypes = new List<Type>();
            NewItemTypes.Add(typeof(DataFormListEntry));
            DataFormListEntryCollection = new DataFormListEntryCollection();
            ItemsSourceType = typeof(DataFormListEntryCollection);
        }

        #region NewItemType Property

        public static readonly DependencyProperty NewItemTypesProperty =
            DependencyProperty.Register("NewItemTypes",
                                        typeof(IList),
                                        typeof(DraggablePopupCustomControl),
                                        new UIPropertyMetadata(null));

        public IList<Type> NewItemTypes
        {
            get
            {
                return (IList<Type>)GetValue(NewItemTypesProperty);
            }
            set
            {
                SetValue(NewItemTypesProperty, value);
            }
        }

        #endregion NewItemType Property

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(DraggablePopupCustomControl),
            new UIPropertyMetadata(null));

        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ItemsSourceTypeProperty = DependencyProperty.Register("ItemsSourceType", typeof(Type), typeof(DraggablePopupCustomControl),
                                                                            new UIPropertyMetadata(null));

        public Type ItemsSourceType
        {
            get
            {
                return (Type)GetValue(ItemsSourceTypeProperty);
            }
            set
            {
                SetValue(ItemsSourceTypeProperty, value);
            }
        }

        #region EditorDefinitions Property

        public static readonly DependencyProperty EditorDefinitionsProperty = DependencyProperty.Register("EditorDefinitions", typeof(EditorDefinitionCollection),
            typeof(DraggablePopupCustomControl), new UIPropertyMetadata(null));

        public EditorDefinitionCollection EditorDefinitions
        {
            get
            {
                return (EditorDefinitionCollection)GetValue(EditorDefinitionsProperty);
            }
            set
            {
                SetValue(EditorDefinitionsProperty, value);
            }
        }

        #endregion EditorDefinitions Property

        [Editor(typeof(DataFormListEntryCollectionEditor), typeof(UITypeEditor))]
        [Category("A - User Lists")]
        [DisplayName("User List Entries")]
        [Description("A collection of user list entries")]
        [NewItemTypes(typeof(DataFormListEntry))]
        public DataFormListEntryCollection DataFormListEntryCollection
        {
            get { return _DataFormListEntryCollection; }
            set
            {
                _DataFormListEntryCollection = value;
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            var contents = Child as FrameworkElement;
            Debug.Assert(contents != null, "DraggablePopup either has no content if content that does not derive from FrameworkElement. Must be fixed for dragging to work.");
            contents.MouseLeftButtonDown += Child_MouseLeftButtonDown;
            contents.MouseLeftButtonUp += Child_MouseLeftButtonUp;
            contents.MouseMove += Child_MouseMove;
        }

        private void Child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            _initialMousePosition = e.GetPosition(null);
            element.CaptureMouse();
            _isDragging = true;
            e.Handled = true;
        }

        private void Child_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                var currentPoint = e.GetPosition(null);
                HorizontalOffset = HorizontalOffset + (currentPoint.X - _initialMousePosition.X);
                VerticalOffset = VerticalOffset + (currentPoint.Y - _initialMousePosition.Y);
            }
        }

        private void Child_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragging)
            {
                var element = sender as FrameworkElement;
                element.ReleaseMouseCapture();
                _isDragging = false;
                e.Handled = true;
            }
        }
    }
}