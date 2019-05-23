using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class Menu
    {
        public int Id { get; set; }
        public string Hyperlink { get; set; }
        public string Image { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
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
