using Abstractions;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Implementations
{
    public class AdminService
    {
        private IPersistenceContext dataContext;

        public AdminService(IPersistenceContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<Store> GetAllStores()
        {
            return null;
        }

        public void AddStore()
        {

        }

        public void UpdateStore()
        {

        }

        public void RemoveStore()
        {

        }


        public void CreateSession()
        {

        }

        public Session GetCurrentSession()
        {
            return null;
        }
    }
}
