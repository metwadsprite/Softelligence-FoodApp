using BusinessLogic;

namespace UserInterface.Models
{
    public class OrderActiveVM
    {
        public Session Session { get; set; }
        public bool OrderIsActive { get; set; }
        public Order Order { get; set; }
    }
}
