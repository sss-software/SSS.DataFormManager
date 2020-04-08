using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SSS.DataFormManager.Models
{
    [Serializable]
    public class DataFormTypeDTO : INotifyPropertyChanged
    {
        private long dataFormTypeId;
        private string formTypeName;
        private string formTypeDescription;
        private bool isActive;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        public DataFormTypeDTO()
        {
        }

        [XmlElement("DataFormTypeId")]
        public long DataFormTypeId
        {
            get
            {
                return dataFormTypeId;
            }
            set
            {
                dataFormTypeId = value;
                OnPropertyChanged("DataFormTypeId");
            }
        }

        [XmlElement("FormTypeName")]
        public string FormTypeName
        {
            get
            {
                return formTypeName;
            }
            set
            {
                formTypeName = value;
                OnPropertyChanged("FormTypeName");
            }
        }

        [XmlElement("FormTypeDescription")]
        public string FormTypeDescription
        {
            get
            {
                return formTypeDescription;
            }
            set
            {
                formTypeDescription = value;
                OnPropertyChanged("FormTypeDescription");
            }
        }

        [XmlElement("IsActive")]
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                OnPropertyChanged("IsActive");
            }
        }
    }
}