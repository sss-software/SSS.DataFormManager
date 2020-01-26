using System;
using System.Xml.Serialization;

namespace SSS.DataFormManager.Models
{
    [Serializable]
    public class DataFormFooter
    {
        public DataFormFooter()
        {
        }

        [XmlElement("CapturedBy")]
        public string CapturedBy { get; set; }

        [XmlElement("CapturedOn")]
        public DateTime CapturedOn { get; set; }

        [XmlElement("Note")]
        public string Note { get; set; }
    }
}