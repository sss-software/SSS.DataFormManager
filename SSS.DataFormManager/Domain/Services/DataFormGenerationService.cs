using SSS.DataFormManager.Models;

namespace SSS.DataFormManager.Domain.Services
{
    public class DataFormGenerationService
    {
        public DataFormDTO CreateDataFormDTO(DataFormDesignTemplate dataFormDesignTemplate)
        {
            DataFormDTO dto = new DataFormDTO()
            {
                DataFormHeader = dataFormDesignTemplate.DataFormHeader,
                DataFormFooter = dataFormDesignTemplate.DataFormFooter,
                DataFormBody = dataFormDesignTemplate.DataFormBody
            };
            return dto;
        }
    }
}