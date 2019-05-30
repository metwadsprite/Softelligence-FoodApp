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
        public Session Session { get; set; }
        public IEnumerable<User> Users { get; set; }
        public bool HasActiveSession { get; set; }
        public decimal Price { get; set; }
        public ICollection<bool> SelectedStores { get; set; }
    }
}
