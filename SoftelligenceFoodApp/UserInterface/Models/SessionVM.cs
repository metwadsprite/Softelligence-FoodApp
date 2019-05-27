using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class SessionVM
    {
        public IEnumerable<Store> Stores { get; set; }
        public IEnumerable<Session> Sessions { get; set; }

    }
}
