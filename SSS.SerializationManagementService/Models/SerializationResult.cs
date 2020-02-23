using System;
using System.Collections.Generic;

namespace SSS.SerializationManagementService.Models
{
    public class SerializationResult
    {
        #region Constructors

        public SerializationResult()
        {
            IsSerialized = false;
            IsDeserialized = false;
            Errors = new List<string>();
            Exceptions = new List<Exception>();
        }

        #endregion Constructors

        #region Properties

        public List<string> Errors { get; }
        public List<Exception> Exceptions { get; }
        public string OutputFilePath { get; set; }
        public object DeserializedObject { get; set; }
        public bool IsSerialized { get; set; }
        public bool IsDeserialized { get; set; }

        public bool IsSerizationValid => (IsSerialized) &&
                                         (Errors != null) &&
                                         (Errors.Count == 0) &&
                                         (Exceptions != null) &&
                                         (Exceptions.Count == 0);

        public bool IsDeserizationValid => (IsDeserialized) &&
                                           (Errors != null) &&
                                           (Errors.Count == 0) &&
                                           (Exceptions != null) &&
                                           (Exceptions.Count == 0);

        #endregion Properties
    }
}