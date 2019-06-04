using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class OrderVM
    {
        public string Option { get; set; }
        public decimal Price { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            var other = (OrderVM)obj;
            return (Option == other.Option) && (Price == other.Price);
        }
        public override int GetHashCode()
        {
            return Option.GetHashCode() ^ Price.GetHashCode();
        }

        public override string ToString()
        {
            return Option + " - " + Price.ToString();
        }
    }
}
