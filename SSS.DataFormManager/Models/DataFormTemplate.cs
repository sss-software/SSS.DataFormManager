using System.Collections.Generic;

namespace SSS.DataFormManager.Models
{
    public class DataFormTemplate
    {
        public DataFormTemplate()
        {
            PopulateDataFormTemplateHeaders();
        }

        public IEnumerable<string> DataFormTemplateHeaders { get; private set; }

        private void PopulateDataFormTemplateHeaders()
        {
            List<string> options = new List<string>();
            options.Add(". . .");
            options.Add("Label Control");
            options.Add("Data Control");
            options.Add("Data Type");
            options.Add("Data Value");
            options.Add("Validation Rule");
            DataFormTemplateHeaders = options;
        }
    }
}