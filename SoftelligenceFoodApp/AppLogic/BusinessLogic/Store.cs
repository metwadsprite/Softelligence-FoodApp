using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public List<MenuItem> MenuItems { get; set; }

        public void AddMenuItem(MenuItem newMenuItem)
        {
            MenuItems.Add(newMenuItem);
        }
        public void AddMenuItem(string details, decimal price)
        {
            MenuItems.Add(new MenuItem { Details = details, Price = price });
        }
    }
}
