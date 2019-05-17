using System;
using System.Runtime.Serialization;

namespace BusinessLogic.BusinessExceptions
{
    [System.Serializable]
    public class OrderHistoryAccessException : Exception
    {
        public OrderHistoryAccessException()
        {
        }
        public OrderHistoryAccessException(string message) : base(message)
        {
        }
        public OrderHistoryAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected OrderHistoryAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
