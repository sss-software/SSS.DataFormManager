using SSS.SerializationManagementService.Models;

namespace SSS.SerializationManagementService
{
    public interface ISerializationManager
    {
        SerializationResult DeserializeToObject(DeserializationRequest deserializationRequest);
        SerializationResult SerializeToXML(SerializationRequest serializationRequest);
    }
}