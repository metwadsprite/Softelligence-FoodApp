using Abstractions;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Implementations
{
    public class AdminService
    {
        private readonly IPersistenceContext dataContext;
        private Administrator administrator;

        public AdminService(IPersistenceContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<Store> GetAllStores()
        {
            var storeRepo = dataContext.GetStoresRepository();
            return storeRepo.GetAll();
        }

        public void AddStore(Store storeToAdd)
        {

        }

        public void UpdateStore(Store storeToUpdate)
        {

        }

        public void RemoveStore(Store storeToRemove)
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
