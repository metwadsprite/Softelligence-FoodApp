using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class PlaceRestaurantOrderVM
    {
        public int OrderStoreId { get; set; }
        public string Option { get; set; }
        public decimal Price { get; set; }
        public string StoreName { get; set; }
        public string Image { get; set; }
        public string Hyperlink { get; set; }
        public string UserEmail { get; set; }
        public ICollection<Order> Suggestions { get; set; }
    }
}
