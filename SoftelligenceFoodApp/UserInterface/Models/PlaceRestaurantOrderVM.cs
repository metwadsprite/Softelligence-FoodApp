using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class PlaceRestaurantOrderVM
    {
        public Store OrderStore { get; set; }
        public String Option { get; set; }
        public decimal Price { get; set; }
    }
}
