using SSS.DataFormManager.Views.Helpers;
using SSS.DataFormManager.Views.Helpers.EventArgs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace SSS.DataFormManager.Views.Controls.CustomControls.CollectionCustomControl
{
    [TemplatePart(Name = PART_NewItemTypesComboBox, Type = typeof(ComboBox))]
    [TemplatePart(Name = PART_PropertyGrid, Type = typeof(PropertyGrid))]
    [TemplatePart(Name = PART_ListBox, Type = typeof(ListBox))]
    public class CollectionCustomControl : Control
    {
        private const string PART_NewItemTypesComboBox = "PART_NewItemTypesComboBox";
        private const string PART_PropertyGrid = "PART_PropertyGrid";
        private const string PART_ListBox = "PART_ListBox";

        #region Private Members

        private bool _isCollectionUpdated;
        private ComboBox _newItemTypesComboBox;
        private ListBox _listBox;
        private PropertyGrid _propertyGrid;

        #endregion Private Members

        static CollectionCustomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CollectionCustomControl), new FrameworkPropertyMetadata(typeof(CollectionCustomControl)));
        }

        public CollectionCustomControl()
        {
            Items = new ObservableCollection<DataFormListEntry>();
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, this.AddNew, this.CanAddNew));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, this.Delete, this.CanDelete));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, this.Duplicate, this.CanDuplicate));
            CommandBindings.Add(new CommandBinding(ComponentCommands.MoveDown, this.MoveDown, this.CanMoveDown));
            CommandBindings.Add(new CommandBinding(ComponentCommands.MoveUp, this.MoveUp, this.CanMoveUp));
        }

        #region ItemAdding Event

        public delegate void ItemAddingRoutedEventHandler(object sender, ItemAddingEventArgs e);

        public static readonly RoutedEvent ItemAddingEvent = EventManager.RegisterRoutedEvent("ItemAdding",
                                                                                              RoutingStrategy.Bubble,
                                                                                              typeof(ItemAddingRoutedEventHandler),
                                                                                              typeof(CollectionCustomControl));

        public event ItemAddingRoutedEventHandler ItemAdding
        {
            add
            {
                AddHandler(ItemAddingEvent, value);
            }
            remove
            {
                RemoveHandler(ItemAddingEvent, value);
            }
        }

        #endregion ItemAdding Event

        #region ItemAdded Event

        public delegate void ItemAddedRoutedEventHandler(object sender, ItemEventArgs e);

        public static readonly RoutedEvent ItemAddedEvent = EventManager.RegisterRoutedEvent("ItemAdded",
                                                            RoutingStrategy.Bubble,
                                                            typeof(ItemAddedRoutedEventHandler),
                                                            typeof(CollectionCustomControl));

        public event ItemAddedRoutedEventHandler ItemAdded
        {
            add
            {
                AddHandler(ItemAddedEvent, value);
            }
            remove
            {
                RemoveHandler(ItemAddedEvent, value);
            }
        }

        #endregion ItemAdded Event

        #region ItemDeleting Event

        public delegate void ItemDeletingRoutedEventHandler(object sender, ItemDeletingEventArgs e);

        public static readonly RoutedEvent ItemDeletingEvent =
                                            EventManager.RegisterRoutedEvent("ItemDeleting",
                                            RoutingStrategy.Bubble,
                                            typeof(ItemDeletingRoutedEventHandler),
                                            typeof(CollectionCustomControl));

        public event ItemDeletingRoutedEventHandler ItemDeleting
        {
            add
            {
                AddHandler(ItemDeletingEvent, value);
            }
            remove
            {
                RemoveHandler(ItemDeletingEvent, value);
            }
        }

        #endregion ItemDeleting Event

        #region ItemDeleted Event

        public delegate void ItemDeletedRoutedEventHandler(object sender, ItemEventArgs e);

        public static readonly RoutedEvent ItemDeletedEvent =
                                           EventManager.RegisterRoutedEvent("ItemDeleted",
                                           RoutingStrategy.Bubble,
                                           typeof(ItemDeletedRoutedEventHandler),
                                           typeof(CollectionCustomControl));

        public event ItemDeletedRoutedEventHandler ItemDeleted
        {
            add
            {
                AddHandler(ItemDeletedEvent, value);
            }
            remove
            {
                RemoveHandler(ItemDeletedEvent, value);
            }
        }

        #endregion ItemDeleted Event

        #region ItemMovedDown Event

        public delegate void ItemMovedDownRoutedEventHandler(object sender, ItemEventArgs e);

        public static readonly RoutedEvent ItemMovedDownEvent =
                                           EventManager.RegisterRoutedEvent("ItemMovedDown",
                                           RoutingStrategy.Bubble,
                                           typeof(ItemMovedDownRoutedEventHandler),
                                           typeof(CollectionCustomControl));

        public event ItemMovedDownRoutedEventHandler ItemMovedDown
        {
            add
            {
                AddHandler(ItemMovedDownEvent, value);
            }
            remove
            {
                RemoveHandler(ItemMovedDownEvent, value);
            }
        }

        #endregion ItemMovedDown Event

        #region ItemMovedUp Event

        public delegate void ItemMovedUpRoutedEventHandler(object sender, ItemEventArgs e);

        public static readonly RoutedEvent ItemMovedUpEvent = EventManager.RegisterRoutedEvent("ItemMovedUp",
                                                              RoutingStrategy.Bubble,
                                                              typeof(ItemMovedUpRoutedEventHandler),
                                                              typeof(CollectionCustomControl));

        public event ItemMovedUpRoutedEventHandler ItemMovedUp
        {
            add
            {
                AddHandler(ItemMovedUpEvent, value);
            }
            remove
            {
                RemoveHandler(ItemMovedUpEvent, value);
            }
        }

        #endregion ItemMovedUp Event

        #region TypeSelectionLabel Property

        public static readonly DependencyProperty TypeSelectionLabelProperty =
                                        DependencyProperty.Register("TypeSelectionLabel",
                                        typeof(object),
                                        typeof(CollectionCustomControl),
                                        new UIPropertyMetadata("Select type:"));

        public object TypeSelectionLabel
        {
            get
            {
                return (object)GetValue(TypeSelectionLabelProperty);
            }
            set
            {
                SetValue(TypeSelectionLabelProperty, value);
            }
        }

        #endregion TypeSelectionLabel Property

        #region PropertiesLabel Property

        public static readonly DependencyProperty PropertiesLabelProperty =
                                        DependencyProperty.Register("PropertiesLabel",
                                        typeof(object),
                                        typeof(CollectionCustomControl),
                                        new UIPropertyMetadata("Properties:"));

        public object PropertiesLabel
        {
            get
            {
                return (object)GetValue(PropertiesLabelProperty);
            }
            set
            {
                SetValue(PropertiesLabelProperty, value);
            }
        }

        #endregion PropertiesLabel Property

        #region ItemsSourceType Property

        public static readonly DependencyProperty ItemsSourceTypeProperty =
            DependencyProperty.Register("ItemsSourceType",
                                        typeof(Type),
                                        typeof(CollectionCustomControl),
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

        #endregion ItemsSourceType Property

        #region NewItemType Property

        public static readonly DependencyProperty NewItemTypesProperty =
            DependencyProperty.Register("NewItemTypes",
                                        typeof(IList),
                                        typeof(CollectionCustomControl),
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

        #region SelectedItem Property

        public static readonly DependencyProperty SelectedItemProperty =
                                            DependencyProperty.Register("SelectedItem",
                                            typeof(object),
                                            typeof(CollectionCustomControl),
                                            new UIPropertyMetadata(null));

        public object SelectedItem
        {
            get
            {
                return (object)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        #endregion SelectedItem Property

        #region Items Property

        public static readonly DependencyProperty ItemsProperty =
                DependencyProperty.Register("Items",
                    typeof(ObservableCollection<DataFormListEntry>),
                    typeof(CollectionCustomControl),
                    new UIPropertyMetadata(null));

        public ObservableCollection<DataFormListEntry> Items
        {
            get
            {
                return (ObservableCollection<DataFormListEntry>)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        #endregion Items Property

        #region IsReadOnly Property

        public static readonly DependencyProperty IsReadOnlyProperty =
                                                  DependencyProperty.Register("IsReadOnly",
                                                                             typeof(bool),
                                                                             typeof(CollectionCustomControl),
                                                                             new UIPropertyMetadata(false));

        public bool IsReadOnly
        {
            get
            {
                return (bool)GetValue(IsReadOnlyProperty);
            }
            set
            {
                SetValue(IsReadOnlyProperty, value);
            }
        }

        #endregion IsReadOnly Property

        #region EditorDefinitions Property

        public static readonly DependencyProperty EditorDefinitionsProperty = DependencyProperty.Register("EditorDefinitions", typeof(EditorDefinitionCollection),
            typeof(CollectionCustomControl), new UIPropertyMetadata(null));

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

        #region ItemsSource Property

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<object>), typeof(CollectionCustomControl),
            new UIPropertyMetadata(null, OnItemsSourceChanged));

        public ObservableCollection<object> ItemsSource
        {
            get
            {
                return (ObservableCollection<object>)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var CollectionControl = (CollectionCustomControl)d;
            if (CollectionControl != null)
                CollectionControl.OnItemSourceChanged((IEnumerable)e.OldValue, (IEnumerable)e.NewValue);
        }

        public void OnItemSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (newValue != null)
            {
                var dict = newValue as IDictionary;
                if (dict != null)
                {
                    // A Dictionary contains KeyValuePair that can't be edited.
                    // We need to Add EditableKeyValuePairs from DictionaryEntries.
                    foreach (DictionaryEntry item in dict)
                    {
                        var keyType = (item.Key != null)
                                      ? item.Key.GetType()
                                      : (dict.GetType().GetGenericArguments().Count() > 0) ? dict.GetType().GetGenericArguments()[0] : typeof(object);
                        var valueType = (item.Value != null)
                                      ? item.Value.GetType()
                                      : (dict.GetType().GetGenericArguments().Count() > 1) ? dict.GetType().GetGenericArguments()[1] : typeof(object);
                        var editableKeyValuePair = ListHelper.CreateEditableKeyValuePair(item.Key
                                                                                            , keyType
                                                                                            , item.Value
                                                                                            , valueType);
                        this.Items.Add(editableKeyValuePair as DataFormListEntry);
                    }
                }
                else
                {
                    foreach (var item in newValue)
                    {
                        if (item != null)
                        {
                            Items.Add(item as DataFormListEntry);
                        }
                    }
                }
            }
        }

        #endregion ItemsSource Property

        #region EventHandlers

        private void NewItemTypesComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (_newItemTypesComboBox != null)
                _newItemTypesComboBox.SelectedIndex = 0;
        }

        private void PropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            if (_listBox != null)
            {
                _isCollectionUpdated = true;
                _listBox.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                {
                    _listBox.Items.Refresh();
                }
                ));
            }
        }

        #endregion EventHandlers

        #region Commands

        private void AddNew(object sender, ExecutedRoutedEventArgs e)
        {
            object newItem = this.CreateNewItem((Type)e.Parameter);
            this.ItemsSource.Add(newItem);
            this.AddNewCore(newItem);
        }

        private void CanAddNew(object sender, CanExecuteRoutedEventArgs e)
        {
            var t = e.Parameter as Type;
            this.CanAddNewCore(t, e);
        }

        private void CanAddNewCore(Type t, CanExecuteRoutedEventArgs e)
        {
            if ((t != null) && !this.IsReadOnly)
            {
                var isComplexStruct = t.IsValueType && !t.IsEnum && !t.IsPrimitive;

                if (isComplexStruct || (t.GetConstructor(Type.EmptyTypes) != null))
                {
                    e.CanExecute = true;
                }
            }
        }

        private object CreateNewItem(Type type)
        {
            return Activator.CreateInstance(type);
        }

        private void AddNewCore(object newItem)
        {
            if (newItem == null)
                throw new ArgumentNullException("newItem");

            var eventArgs = new ItemAddingEventArgs(ItemAddingEvent, newItem);
            this.RaiseEvent(eventArgs);
            if (eventArgs.Cancel)
                return;
            newItem = eventArgs.Item as DataFormListEntry;

            this.Items.Add(newItem as DataFormListEntry);

            this.RaiseEvent(new ItemEventArgs(ItemAddedEvent, newItem));
            _isCollectionUpdated = true;

            this.SelectedItem = newItem;
        }

        private void Delete(object sender, ExecutedRoutedEventArgs e)
        {
            var eventArgs = new ItemDeletingEventArgs(ItemDeletingEvent, e.Parameter);
            this.RaiseEvent(eventArgs);
            if (eventArgs.Cancel)
                return;
            this.Items.Remove(e.Parameter as DataFormListEntry);
            this.ItemsSource.Remove(e.Parameter as DataFormListEntry);

            this.RaiseEvent(new ItemEventArgs(ItemDeletedEvent, e.Parameter));
            _isCollectionUpdated = true;
        }

        private void CanDelete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = e.Parameter != null && !this.IsReadOnly;
        }

        private void Duplicate(object sender, ExecutedRoutedEventArgs e)
        {
            var newItem = this.DuplicateItem(e);
            this.AddNewCore(newItem as DataFormListEntry);
        }

        private void CanDuplicate(object sender, CanExecuteRoutedEventArgs e)
        {
            var t = (e.Parameter != null) ? e.Parameter.GetType() : null;
            this.CanAddNewCore(t, e);
        }

        private object DuplicateItem(ExecutedRoutedEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            var baseItem = e.Parameter;
            var newItemType = baseItem.GetType();
            var newItem = this.CreateNewItem(newItemType);

            var type = newItemType;
            while (type != null)
            {
                var baseProperties = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                foreach (var prop in baseProperties)
                {
                    prop.SetValue(newItem, prop.GetValue(baseItem));
                }
                type = type.BaseType;
            }

            return newItem;
        }

        private void MoveDown(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItem = e.Parameter as DataFormListEntry;
            var index = Items.IndexOf(selectedItem);
            Items.RemoveAt(index);
            Items.Insert(++index, selectedItem);

            this.RaiseEvent(new ItemEventArgs(ItemMovedDownEvent, selectedItem));
            _isCollectionUpdated = true;

            this.SelectedItem = selectedItem;
        }

        private void CanMoveDown(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter != null && Items.IndexOf(e.Parameter as DataFormListEntry) < (Items.Count - 1) && !this.IsReadOnly)
                e.CanExecute = true;
        }

        private void MoveUp(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedItem = e.Parameter as DataFormListEntry;
            var index = Items.IndexOf(selectedItem);
            this.Items.RemoveAt(index);
            this.Items.Insert(--index, selectedItem);

            this.RaiseEvent(new ItemEventArgs(ItemMovedUpEvent, selectedItem));
            _isCollectionUpdated = true;

            this.SelectedItem = selectedItem;
        }

        private void CanMoveUp(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter != null && Items.IndexOf(e.Parameter as DataFormListEntry) > 0 && !this.IsReadOnly)
                e.CanExecute = true;
        }

        #endregion Commands

        #region Override Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (_newItemTypesComboBox != null)
            {
                _newItemTypesComboBox.Loaded -= new RoutedEventHandler(this.NewItemTypesComboBox_Loaded);
            }
            _newItemTypesComboBox = GetTemplateChild(PART_NewItemTypesComboBox) as ComboBox;
            if (_newItemTypesComboBox != null)
            {
                _newItemTypesComboBox.Loaded += new RoutedEventHandler(this.NewItemTypesComboBox_Loaded);
            }

            _listBox = this.GetTemplateChild(PART_ListBox) as ListBox;

            if (_propertyGrid != null)
            {
                _propertyGrid.PropertyValueChanged -= this.PropertyGrid_PropertyValueChanged;
            }
            _propertyGrid = GetTemplateChild(PART_PropertyGrid) as PropertyGrid;
            if (_propertyGrid != null)
            {
                _propertyGrid.PropertyValueChanged += this.PropertyGrid_PropertyValueChanged;
            }
        }

        public PropertyGrid PropertyGrid
        {
            get
            {
                if (_propertyGrid == null)
                {
                    this.ApplyTemplate();
                }
                return _propertyGrid;
            }
            private set
            {
                _propertyGrid = value;
            }
        }

        #endregion Override Methods
    }
}