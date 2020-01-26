using System;
using System.ComponentModel;
using System.Text;

namespace TestProject
{
    public class ListEntryCollectionPropertyDescriptor : PropertyDescriptor
    {
        private ListEntryCollection collection = null;
        private int index = -1;

        public ListEntryCollectionPropertyDescriptor(ListEntryCollection coll, int idx) :
            base("#" + idx.ToString(), null)
        {
            this.collection = coll;
            this.index = idx;
        }

        public override AttributeCollection Attributes
        {
            get
            {
                return new AttributeCollection(null);
            }
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get
            {
                return this.collection.GetType();
            }
        }

        public override string DisplayName
        {
            get
            {
                ListEntry entry = this.collection[index];
                return entry.Key + " " + entry.Value + " " + entry.Description;
            }
        }

        public override string Description
        {
            get
            {
                ListEntry entry = this.collection[index];
                StringBuilder sb = new StringBuilder();
                sb.Append(entry.Key);
                sb.Append(",");
                sb.Append(entry.Value);
                sb.Append(",");
                sb.Append(entry.Description);
                return sb.ToString();
            }
        }

        public override object GetValue(object component)
        {
            return this.collection[index];
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override string Name
        {
            get { return "#" + index.ToString(); }
        }

        public override Type PropertyType
        {
            get { return this.collection[index].GetType(); }
        }

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override void SetValue(object component, object value)
        {
            this.collection[index] = (ListEntry)value;
        }
    }
}