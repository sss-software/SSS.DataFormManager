using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace SSS.DataFormManager.Models
{
    [Serializable]
    public class DataFormHeader
    {
        public DataFormHeader()
        {
        }

        [XmlElement("SynchronizationId")]
        public long SynchronizationId { get; set; }

        [XmlElement("LocalId")]
        public string LocalId { get; set; }

        [XmlElement("CloudId")]
        public string CloudId { get; set; }

        [XmlElement("FormTitle")]
        public string FormTitle { get; set; }

        [XmlElement("FormDescription")]
        public string FormDescription { get; set; }

        [XmlElement("FormDisplayHeader")]
        public string FormDisplayHeader { get; set; }

        [XmlElement("SelectedFormType")]
        public string SelectedFormType { get; set; }

        [XmlElement("SelectedFormTypeIndex")]
        public long SelectedFormTypeIndex { get; set; }

        [XmlElement("SelectedCategory")]
        public string SelectedCategory { get; set; }

        [XmlElement("SelectedCategoryIndex")]
        public long SelectedCategoryIndex { get; set; }

        [XmlElement("SelectedSubCategory")]
        public string SelectedSubCategory { get; set; }

        [XmlElement("SelectedSubCategoryIndex")]
        public long SelectedSubCategoryIndex { get; set; }

        [XmlArray("DataFormCategories")]
        [XmlArrayItem("DataFormCategory", typeof(DataFormCategoryDTO))]
        public ObservableCollection<DataFormCategoryDTO> DataFormCategories { get; set; }

        [XmlArray("DataFormSubCategories")]
        [XmlArrayItem("DataFormSubCategory", typeof(DataFormSubCategoryDTO))]
        public ObservableCollection<DataFormSubCategoryDTO> DataFormSubCategories { get; set; }

        [XmlArray("DataFormTypes")]
        [XmlArrayItem("DataFormType", typeof(DataFormTypeDTO))]
        public ObservableCollection<DataFormTypeDTO> DataFormTypes { get; set; }

        [XmlElement("IsEncrypted")]
        public bool IsEncrypted { get; set; }
    }
}