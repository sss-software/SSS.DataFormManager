using System;
using System.Xml.Serialization;

namespace SSS.DataFormManager.Models
{
    [Serializable]
    [XmlRoot(Namespace = "www.slambert.software.com")]
    public class DataFormDTO
    {
        public DataFormDTO()
        {
        }

        [XmlElement("DataFormHeader")]
        public DataFormHeader DataFormHeader { get; set; }

        [XmlElement("DataFormFooter")]
        public DataFormFooter DataFormFooter { get; set; }

        [XmlElement("DataFormBody")]
        public DataFormBody DataFormBody { get; set; }
    }
}