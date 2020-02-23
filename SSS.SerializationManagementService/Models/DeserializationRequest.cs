using System;

namespace SSS.SerializationManagementService.Models
{
    public class DeserializationRequest
    {
        public string InputFilePath { get; set; }
        public Type DeserializationType { get; set; }
    }
}