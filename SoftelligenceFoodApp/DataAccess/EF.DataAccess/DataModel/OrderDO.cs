using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.DataAccess.DataModel
{
    [Table("Orders")]
    public class OrderDO
    {
        public int Id { get; set; }
        public UserDO User { get; set; }
        public StoreDO Store { get; set; }
        public SessionDO Session { get; set; }
        public string Details { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsActive { get; set; }
    }
}
