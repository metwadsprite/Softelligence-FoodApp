using System.ComponentModel.DataAnnotations.Schema;

namespace EF.DataAccess.DataModel
{
    public class MenuItemDO
    {
        public int Id { get; set; }
        public string Details { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
