using System;

namespace SSS.DataFormManager.DAL.Models
{
    public class SynchronizationRegister
    {
        public long Id { get; set; }
        public string LocalId { get; set; }
        public string CloudId { get; set; }
        public string ResourceHeader { get; set; }
        public string ResourceDescription { get; set; }
        public string UploadedBy { get; set; }
        public Nullable<DateTime> UploadedOn { get; set; }
        public string LastSyncedBy { get; set; }
        public Nullable<DateTime> LastSyncedOn { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<bool> IsTrashed { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}