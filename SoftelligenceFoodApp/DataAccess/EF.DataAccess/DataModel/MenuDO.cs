using BusinessLogic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF.DataAccess.DataModel
{
    [Table("Menus")]
    public class MenuDO
    {

        public int Id { get; set; }
        public string Hyperlink { get; set; }
        public string Image { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
