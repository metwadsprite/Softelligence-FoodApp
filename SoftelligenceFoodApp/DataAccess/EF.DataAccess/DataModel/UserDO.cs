using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EF.DataAccess.DataModel
{
    [Table("Users")]
    public class UserDO
    {
        public int Id { get; set; }
    }
}
