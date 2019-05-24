using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class SessionStore
    {
        public int Id { get; set; }
        public Session Session { get; set; }
        public Store Store { get; set; }
    }
}
