using System.Collections.Generic;
using System.ComponentModel;

namespace SSS.DataFormManager.Models
{
    public class DataFormCategory : INotifyPropertyChanged
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

        public ICollection<DataFormSubCategory> SubCategories
        {
            get;
            set;
        }
    }
}