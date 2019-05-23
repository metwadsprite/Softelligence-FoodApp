using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Menu Menu { get; set; }

        public void AddMenuItem(MenuItem newMenuItem)
        {
            Menu.MenuItems.Add(newMenuItem);
        }
        public void AddMenuItem(string details, decimal price)
        {
            Menu.MenuItems.Add(new MenuItem { Details = details, Price = price });
        }
    }
}
