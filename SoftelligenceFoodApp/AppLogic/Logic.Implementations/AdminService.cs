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
            administrator.AddStore(storeToAdd);
        }

        public void UpdateStore(Store storeToUpdate, Store newStore)
        {
            administrator.Update(storeToUpdate, newStore);
        }

        public void RemoveStore(int storeId)
        {
            administrator.Remove(storeId);
        }

        public void StartSession()
        {

        }

        public Session GetCurrentSession()
        {
            return null;
        }

        public void SyncDataContext()
        {
            

        }
    }
}
