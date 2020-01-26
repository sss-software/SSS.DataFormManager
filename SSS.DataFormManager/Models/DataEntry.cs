using SSS.DataFormManager.Views.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace SSS.DataFormManager.Models
{
    [Serializable]
    public class DataEntry
    {
        public DataEntry()
        {
        }

        [XmlElement("LabelControl")]
        public string LabelControl { get; set; }

        [XmlElement("LabelValue")]
        public string LabelValue { get; set; }

        [XmlElement("DataControl")]
        public string DataControl { get; set; }

        [XmlElement("TextBoxControlValue")]
        public string TextBoxControlValue { get; set; }

        [XmlElement("ListBoxControlSelectedValue")]
        public string ListBoxControlSelectedValue { get; set; }

        [XmlArray("ListBoxControlItemsSource")]
        [XmlArrayItem("DataFormListEntry", typeof(DataFormListEntry))]
        public ObservableCollection<DataFormListEntry> ListBoxControlItemsSource { get; set; }

        [XmlElement("ListBoxControlSelectedItem")]
        public DataFormListEntry ListBoxControlSelectedItem { get; set; }

        [XmlElement("ListBoxControlSelectedIndex")]
        public int ListBoxControlSelectedIndex { get; set; }

        [XmlElement("ComboBoxControlSelectedValue")]
        public string ComboBoxControlSelectedValue { get; set; }

        [XmlElement("ComboBoxControlItemsSource")]
        public ObservableCollection<DataFormListEntry> ComboBoxControlItemsSource { get; set; }

        [XmlElement("ComboBoxControlSelectedItem")]
        public DataFormListEntry ComboBoxControlSelectedItem { get; set; }

        [XmlElement("ComboBoxControlSelectedIndex")]
        public int ComboBoxControlSelectedIndex { get; set; }
    }
}