using System;

namespace SSS.DataFormManager.Models
{
    public class DataForm
    {
        public long DataFormId { get; set; }
        public Guid DataFormGuid { get; set; }
        public DataFormHeader Header { get; set; }
        public DataFormBody Body { get; set; }
        public DataFormFooter Footer { get; set; }
    }
}