namespace SSS.DataFormManager.DAL.Models
{
    public class DataFormType
    {
        public long DataFormTypeId { get; set; }
        public string FormTypeName { get; set; }
        public string FormTypeNameDescription { get; set; }
        public bool IsActive { get; set; }
    }
}