using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public ICollection<MenuItem> menuItems { get; set; }
    }
}
