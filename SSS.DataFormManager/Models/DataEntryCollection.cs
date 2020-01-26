using System.Collections;
using System.Collections.Generic;

namespace SSS.DataFormManager.Models
{
    public class DataEntryCollection : CollectionBase
    {
        public List<DataEntry> LoadDataEntries()
        {
            List<DataEntry> dataEntries = new List<DataEntry>();
            //dataEntries.Add(new DataEntry()
            //{
            //    DataEntryId = 1,
            //    UniqueId = Guid.NewGuid(),
            //    LabelControl = "Text Block",
            //    LabelValue = "Username",
            //    DataControl = "Text Box",
            //    DataValue = "ABC"
            //});
            return dataEntries;
        }
    }
}