using System;
using System.Runtime.Serialization;

namespace BusinessLogic.BusinessExceptions
{
    [System.Serializable]
    public class OrderInvalidIdException : Exception
    {
        public OrderInvalidIdException()
        {
        }
        public OrderInvalidIdException(string message) : base(message)
        {
        }
        public OrderInvalidIdException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected OrderInvalidIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
