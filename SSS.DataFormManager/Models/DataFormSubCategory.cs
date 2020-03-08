using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SSS.DataFormManager.Models
{
    [Serializable]
    public class DataFormSubCategory : INotifyPropertyChanged
    {
        private long _categoryId;
        private long _subCategoryId;
        private string _subCategoryName;
        private string _subCategoryDescription;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        [XmlElement("CategoryId")]
        public long CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                _categoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }

        [XmlElement("SubCategoryId")]
        public long SubCategoryId
        {
            get
            {
                return _subCategoryId;
            }
            set
            {
                _subCategoryId = value;
                OnPropertyChanged("SubCategoryId");
            }
        }

        [XmlElement("SubCategoryName")]
        public string SubCategoryName
        {
            get
            {
                return _subCategoryName;
            }
            set
            {
                _subCategoryName = value;
                OnPropertyChanged("SubCategoryName");
            }
        }

        [XmlElement("Description")]
        public string Description
        {
            get
            {
                return _subCategoryDescription;
            }
            set
            {
                _subCategoryDescription = value;
                OnPropertyChanged("Description");
            }
        }
    }
}