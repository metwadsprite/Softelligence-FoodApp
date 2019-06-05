using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class OrderActiveVM
    {
        public Session Session { get; set; }
        public bool OrderIsActive { get; set; }
        public Order Order { get; set; }
    }
}
