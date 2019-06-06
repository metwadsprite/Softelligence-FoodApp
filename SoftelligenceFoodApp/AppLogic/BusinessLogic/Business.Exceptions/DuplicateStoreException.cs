using System;
using System.Runtime.Serialization;

namespace BusinessLogic.BusinessExceptions
{
    [System.Serializable]
    public class DuplicateStoreException : Exception
    {
        public DuplicateStoreException()
        {
        }

        public DuplicateStoreException(string message) : base(message)
        {
        }

        public DuplicateStoreException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateStoreException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
