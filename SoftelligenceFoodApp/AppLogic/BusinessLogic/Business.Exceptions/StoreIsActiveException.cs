using System;
using System.Runtime.Serialization;

namespace BusinessLogic.BusinessExceptions
{
    [System.Serializable]
    public class StoreIsActiveException : Exception
    {
        public StoreIsActiveException()
        {
        }

        public StoreIsActiveException(string message) : base(message)
        {
        }

        public StoreIsActiveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StoreIsActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
