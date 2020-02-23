using System.ComponentModel;

namespace SSS.DataFormManager.Models
{
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