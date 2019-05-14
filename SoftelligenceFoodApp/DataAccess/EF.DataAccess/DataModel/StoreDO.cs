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
    }
}
