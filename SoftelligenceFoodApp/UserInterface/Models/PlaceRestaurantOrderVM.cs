using System.Collections.Generic;

namespace UserInterface.Models
{
    public class PlaceRestaurantOrderVM
    {
        public int OrderStoreId { get; set; }
        public OrderVM Order { get; set; }
        public string StoreName { get; set; }
        public string Image { get; set; }
        public string Hyperlink { get; set; }
        public string UserEmail { get; set; }
        public ICollection<OrderVM> Suggestions { get; set; }
    }
}
