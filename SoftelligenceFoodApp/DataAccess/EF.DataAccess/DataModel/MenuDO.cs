using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EF.DataAccess.DataModel
{
    [Table("Menus")]
    public class MenuDO
    {

        public int Id { get; set; }

    }
}
