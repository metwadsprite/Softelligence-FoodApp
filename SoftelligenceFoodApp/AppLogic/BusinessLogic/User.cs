﻿using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public void ViewActiveSession() { }
        public void PlaceOrder() { }
        public void ChangeOrder() { }
        public void CancelOrder() { }
        public void ViewOrder() { }
    }
}
