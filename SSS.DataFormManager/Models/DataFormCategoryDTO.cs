using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SSS.DataFormManager.Models
{
    [Serializable]
    public class DataFormCategoryDTO : INotifyPropertyChanged
    {
        private long _categoryId;
        private string _categoryName;
        private string _categoryDescription;

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

        [XmlElement("CategoryName")]
        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                _categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        [XmlElement("Description")]
        public string Description
        {
            get
            {
                return _categoryDescription;
            }
            set
            {
                _categoryDescription = value;
                OnPropertyChanged("Description");
            }
        }

        [XmlElement("SubCategories")]
        public ICollection<DataFormSubCategoryDTO> SubCategories
        {
            get;
            set;
        }
    }
}