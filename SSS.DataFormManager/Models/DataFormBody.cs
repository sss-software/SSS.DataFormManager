using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace SSS.DataFormManager.Models
{
    [Serializable]
    public class DataFormBody
    {
        public DataFormBody()
        {
        }

        [XmlArray("DataEntries")]
        [XmlArrayItem("DataEntry", typeof(DataEntry))]
        public ObservableCollection<DataEntry> DataEntries { get; set; }
    }
}