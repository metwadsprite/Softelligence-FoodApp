using System;
using System.Runtime.Serialization;

namespace BusinessLogic.BusinessExceptions
{
    [System.Serializable]
    public class StoreNotFoundException : Exception
    {
        public StoreNotFoundException()
        {
        }

        public StoreNotFoundException(string message) : base(message)
        {
        }

        public StoreNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StoreNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
