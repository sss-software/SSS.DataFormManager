using System.ComponentModel;
using System.Text;

namespace TestProject
{
    [TypeConverter(typeof(ListEntryConverter))]
    public class ListEntry : ListItem
    {
        private string _CategoryName = "";

        public ListEntry()
        {
        }

        [Category("Required")]
        public string CategoryName
        {
            get
            {
                return _CategoryName;
            }
            set
            {
                _CategoryName = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.CategoryName);
            sb.Append(",");
            sb.Append(this.Key);
            sb.Append(",");
            sb.Append(this.Value);
            sb.Append(",");
            sb.Append(this.Description);
            return sb.ToString();
        }
    }
}