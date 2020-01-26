using System.ComponentModel;

namespace TestProject
{
    public class ListItem
    {
        private int _key = 0;
        private string _value = "";
        private string _description = "";

        public ListItem()
        {
        }

        [Category("Required")]
        public int Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        [Category("Required")]
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        [Category("Optional")]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
    }
}