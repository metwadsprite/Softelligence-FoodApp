using System;
using System.Runtime.Serialization;

namespace BusinessLogic.BusinessExceptions
{
    [System.Serializable]
    public class SessionNotFoundException: Exception
    {
        public SessionNotFoundException()
        {
        }

        public SessionNotFoundException(string message) : base(message)
        {
        }

        public SessionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SessionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
