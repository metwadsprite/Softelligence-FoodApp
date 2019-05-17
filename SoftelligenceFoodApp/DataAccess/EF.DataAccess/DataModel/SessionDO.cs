using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.DataAccess.DataModel
{
    [Table("Sessions")]
    public class SessionDO
    {
        public int Id { get; set; }
        public List<OrderDO> Orders { get; set; }
        public List<SessionStoreDO> SessionStore { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsActive { get; set; }
    }
}
