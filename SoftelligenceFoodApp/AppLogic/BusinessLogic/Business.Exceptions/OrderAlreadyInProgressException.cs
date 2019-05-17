using System;
using System.Runtime.Serialization;

namespace BusinessLogic.BusinessExceptions
{
    [System.Serializable]
    public class OrderAlreadyInProgressException : Exception
    {
        public OrderAlreadyInProgressException()
        {
        }
        public OrderAlreadyInProgressException(string message) : base(message)
        {
        }
        public OrderAlreadyInProgressException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected OrderAlreadyInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
