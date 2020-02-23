using System;

namespace SSS.SerializationManagementService.Models
{
    public class SerializationRequest
    {
        public object ObjectToSerialize { get; set; }
        public Type ObjectType { get; set; }
        public string OutputFilePath { get; set; }
    }
}