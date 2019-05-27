using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class StoresOrders
    {

        public Session session { get; set; }
        public IEnumerable<Order> orders { get; set; }


    }
}
