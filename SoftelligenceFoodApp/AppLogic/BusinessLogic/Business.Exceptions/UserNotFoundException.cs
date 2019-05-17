﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.Business.Exceptions
{
    [System.Serializable]
    public class UserNotFoundException : Exception
    {

        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message):base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
