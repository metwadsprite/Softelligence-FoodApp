using BusinessLogic;
using System.Collections.Generic;

namespace UserInterface.Models
{
    public class SessionVM
    {
        public List<Store> Stores { get; set; }
        public Session Session { get; set; }
        public List<User> Users { get; set; }
        public bool HasActiveSession { get; set; }
        public decimal Price { get; set; }        
        public List<bool> SelectedStores { get; set; }
    }
}
