﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EF.DataAccess.DataModel
{
    [Table("Orders")]
    public class OrderDO
    {
        public int Id { get; set; }

    }
}
