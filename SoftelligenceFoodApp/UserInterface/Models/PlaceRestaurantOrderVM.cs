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
        public String Option { get; set; }
        public decimal Price { get; set; }
        public String StoreName { get; set; }
        public String Image { get; set; }
        public String Hyperlink { get; set; }
    }
}
