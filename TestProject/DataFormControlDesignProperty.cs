using System.ComponentModel;

namespace TestProject
{
    public class DataFormControlDesignProperty
    {
        private ListEntryCollection entries = new ListEntryCollection();
        private ListEntry[] ents = new ListEntry[2];

        public DataFormControlDesignProperty()
        {
            ListEntry entry1 = new ListEntry();
            entry1.Key = 1;
            entry1.Value = "A new dawn";
            entry1.Description = "A new book by author J. Jennings";
            entry1.CategoryName = "Books";
            this.entries.Add(entry1);

            ListEntry entry2 = new ListEntry();
            entry2.Key = 2;
            entry2.Value = "You Magazine";
            entry2.Description = "A new magazine";
            entry2.CategoryName = "Magazines";
            this.entries.Add(entry2);

            ents[0] = entry1;
            ents[1] = entry2;
        }

        [TypeConverter(typeof(ListEntryCollectionConverter))]
        public ListEntryCollection ListEntries
        {
            get { return entries; }
        }
    }
}