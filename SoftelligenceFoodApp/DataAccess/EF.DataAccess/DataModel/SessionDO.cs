using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.DataAccess.DataModel
{
    [Table("Sessions")]
    public class SessionDO
    {
        public int Id { get; set; }
        public ICollection<OrderDO> Orders { get; set; }
        public ICollection<SessionStoreDO> SessionStore { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsActive { get; set; }
    }
}
