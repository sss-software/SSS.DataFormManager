using System;
using System.ComponentModel;
using System.Text;

namespace SSS.DataFormManager.Views.Helpers
{
    public class DataFormListEntryPropertyDescriptor : PropertyDescriptor
    {
        private DataFormListEntryCollection collection = null;
        private int index = -1;

        public DataFormListEntryPropertyDescriptor(DataFormListEntryCollection coll, int idx) : base("#" + idx.ToString(), null)
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
                DataFormListEntry entry = this.collection[index];
                return entry.KeyName + " " + entry.KeyValue + " " + entry.Description;
            }
        }

        public override string Description
        {
            get
            {
                DataFormListEntry entry = this.collection[index];
                StringBuilder sb = new StringBuilder();
                sb.Append(entry.KeyName);
                sb.Append(",");
                sb.Append(entry.KeyValue);
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
            this.collection[index] = (DataFormListEntry)value;
        }
    }
}