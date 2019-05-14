using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EF.DataAccess.DataModel
{
    [Table("Sessions")]
    public class SessionDO
    {
        public int Id { get; set; }
        public ICollection<StoreDO> ActiveStores { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsActive { get; set; }
    }
}
