using System;
using System.Xml.Serialization;

namespace SSS.DataFormManager.Models
{
    [Serializable]
    public class DataFormHeader
    {
        public DataFormHeader()
        {
        }

        [XmlElement("UniqueId")]
        public Guid UniqueId { get; set; }

        [XmlElement("LocalId")]
        public long LocalId { get; set; }

        [XmlElement("CloudId")]
        public long CloudId { get; set; }

        [XmlElement("Title")]
        public string Title { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("IsEncrypted")]
        public bool IsEncrypted { get; set; }
    }
}