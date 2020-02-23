using SSS.FileManagementService;
using SSS.FileManagementService.Constants;
using SSS.FileManagementService.Models;
using SSS.SerializationManagementService.Models;
using System;
using System.IO;
using System.Xml.Serialization;

namespace SSS.SerializationManagementService
{
    public class SerializationManager : ISerializationManager
    {
        internal SerializationResult result;

        #region Constructors

        public SerializationManager()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Serialize the given serialization request object to xml
        /// </summary>
        /// <param name="serializationRequest"></param>
        /// <returns></returns>
        public SerializationResult SerializeToXML(SerializationRequest serializationRequest)
        {
            result = new SerializationResult();
            result.IsSerialized = false;
            try
            {
                var dto = Convert.ChangeType(serializationRequest.ObjectToSerialize, serializationRequest.ObjectType);
                XmlSerializer serializer = new XmlSerializer(dto.GetType());
                if ((SerializeToConsoleOutput(serializer, dto)) && SerializeToTextWriter(serializer, dto, serializationRequest.OutputFilePath))
                {
                    result.IsSerialized = true;
                }
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                return result;
            }
        }

        /// <summary>
        /// Deserialize the given deserialization request object to an object of the sepcified
        /// </summary>
        /// <param name="serializationRequest"></param>
        /// <returns></returns>
        public SerializationResult DeserializeToObject(DeserializationRequest deserializationRequest)
        {
            result = new SerializationResult();
            result.IsDeserialized = false;
            try
            {
                XmlSerializer serializer = new XmlSerializer(deserializationRequest.DeserializationType);
                if (File.Exists(deserializationRequest.InputFilePath))
                {
                    using (FileManager fileManager = new FileManager())
                    {
                        FileManagerResult fileManagerResult = fileManager.GetFileStream(deserializationRequest.InputFilePath);
                        if ((fileManagerResult != null) && (fileManagerResult.IsFileStreamValid))
                        {
                            using (fileManagerResult.FileStreamObj)
                            {
                                result.DeserializedObject = serializer.Deserialize(fileManagerResult.FileStreamObj);
                                result.IsDeserialized = true;
                            }
                        }
                        else
                        {
                            result.Errors.Add(FileManagerServiceConstants.InvalidFileStreamObject);
                        }
                    }
                }
                else
                {
                    result.Errors.Add(FileManagerServiceConstants.FilePathDoesNotExist);
                }
                return result;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                return result;
            }
        }

        private bool SerializeToConsoleOutput(XmlSerializer serializer, object dto)
        {
            try
            {
                serializer.Serialize(Console.Out, dto);
                return true;
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                return false;
            }
        }

        private bool SerializeToTextWriter(XmlSerializer serializer, object dto, string outputPath)
        {
            try
            {
                if (File.Exists(outputPath))
                {
                    using (TextWriter writer = new StreamWriter(outputPath))
                    {
                        serializer.Serialize(writer, dto);
                    }
                    return true;
                }
                else
                {
                    result.Errors.Add(Constants.OutputFilePathDoesNotExist);
                    return false;
                }
            }
            catch (Exception e)
            {
                result.Exceptions.Add(e);
                return false;
            }
        }

        #endregion Methods
    }
}