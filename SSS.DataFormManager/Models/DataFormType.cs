using System.ComponentModel;

namespace SSS.DataFormManager.Models
{
    public class DataFormType : INotifyPropertyChanged
    {
        private long dataFormTypeId;
        private string formTypeName;
        private string formTypeNameDescription;
        private bool isActive;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        public DataFormType()
        {
        }

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

        public string FormTypeNameDescription
        {
            get
            {
                return formTypeNameDescription;
            }
            set
            {
                formTypeNameDescription = value;
                OnPropertyChanged("FormTypeNameDescription");
            }
        }

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