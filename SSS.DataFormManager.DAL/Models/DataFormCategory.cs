namespace SSS.DataFormManager.DAL.Models
{
    public class DataFormCategory: DataFormBaseModel
    {
        private long dataFormCategoryId;
        private string categoryName;
        private string categoryDescription;
        private bool isActive;

        public long DataFormCategoryId
        {
            get
            {
                return dataFormCategoryId;
            }
            set
            {
                dataFormCategoryId = value;
                OnPropertyChanged("DataFormCategoryId");
            }
        }
        public string CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }
        public string CategoryDescription
        {
            get
            {
                return categoryDescription;
            }
            set
            {
                categoryDescription = value;
                OnPropertyChanged("CategoryDescription");
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