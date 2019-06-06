using System;
using System.Runtime.Serialization;

namespace BusinessLogic.Business.Exceptions
{
    [Serializable]
    public class SessionIsEmptyException : Exception
    {
        public SessionIsEmptyException()
        {
        }

        public SessionIsEmptyException(string message) : base(message)
        {
        }

        public SessionIsEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SessionIsEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
