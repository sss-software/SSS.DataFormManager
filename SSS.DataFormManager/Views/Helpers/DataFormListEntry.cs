using System;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace SSS.DataFormManager.Views.Helpers
{
    [Serializable]
    [DisplayName("Data Form List Entry")]
    public class DataFormListEntry : INotifyPropertyChanged
    {
        #region Private Fields

        private string keyName;
        private string keyValue;
        private string description;

        #endregion Private Fields

        #region Public Properties

        [XmlElement("KeyName")]
        [Category("A - User Lists")]
        [DisplayName("Key name")]
        [Description("The unique identifier of the user list entry.")]
        public string KeyName
        {
            get { return keyName; }
            set
            {
                keyName = value;
                OnPropertyChanged("KeyName");
            }
        }

        [XmlElement("KeyValue")]
        [Category("A - User Lists")]
        [DisplayName("Key value")]
        [Description("The value of user list entry.")]
        public string KeyValue
        {
            get { return keyValue; }
            set
            {
                keyValue = value;
                OnPropertyChanged("KeyValue");
            }
        }

        [XmlElement("Description")]
        [Category("A - User Lists")]
        [DisplayName("Description")]
        [Description("The description  user list entry.")]
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if ((string.IsNullOrEmpty(this.KeyName)) || (string.IsNullOrEmpty(this.KeyValue)))
            {
                sb.Append("<<<");
                sb.Append(" - New List Entry - ");
                sb.Append(">>>");
                return sb.ToString();
            }
            else
            {
                sb.Append(this.KeyName);
                sb.Append(" - ");
                sb.Append(this.KeyValue);
                return sb.ToString();
            }
        }

        #endregion Public Properties

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}