﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class Session
    {
        public int Id { get; set; }
        //private bool IsActive = false;
        private List<Store> Stores;
        private List<Order> Orders;

        public bool IsActive
        {
            get; private set;
        }
        public void AddStore()
        {

        }

        public void AddOrder()
        {

        }

        public void UpdateOrder()
        {

        }

        public void CancelOrder()
        {

        }

        public void FinalizeSession()
        {

        }

        public void DeactivateStore()
        {

        }
    }
}
