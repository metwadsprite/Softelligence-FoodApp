using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EF.DataAccess.DataModel
{
    [Table("Stores")]
    public class StoreDO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MenuDO Menu { get; set; }
        public ICollection<SessionDO> Sessions { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
    }
}
