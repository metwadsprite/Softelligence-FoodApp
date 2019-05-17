using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class Order
    {
        public int Id { get; set; }
        public string RecipientName { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public Store Store { get; set; }

        public void Create(Store store, string details)
        {
            this.Store = store;
            this.Details = details;
        }
    }
}
