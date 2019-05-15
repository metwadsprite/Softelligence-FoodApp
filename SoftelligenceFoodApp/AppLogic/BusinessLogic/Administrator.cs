using BusinessLogic.BusinessExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class Administrator
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        private List<Store> Stores = new List<Store>();

        private List<Store> AvailableStores = new List<Store>();


        public Store FindByStoreId(int storeId)
        {
            var store = Stores.Where(storeIt => storeIt.Id == storeId).SingleOrDefault();
            if(store == null)
            {
                throw new StoreNotFoundException();
            }
            return store;
        }

        public IEnumerable<Store> GetAllStores()
        {
            return Stores;
        }

        public void AddStore(Store storeToAdd)
        {
            var existingStore = FindByStoreId(storeToAdd.Id);
            if(existingStore == storeToAdd)
            {
                throw new DuplicateStoreException();
            }

            Stores.Add(storeToAdd);
        }

        public void Update(int storeId)
        {
            var storeToUpdate = FindByStoreId(storeId);
            if(storeToUpdate == null)
            {
                throw new StoreNotFoundException();
            }
        }

        public void Remove(int storeId)
        {
            var storeToRemove = FindByStoreId(storeId);
            if(storeToRemove == null)
            {
                throw new StoreNotFoundException();
            }

            Stores.Remove(storeToRemove);
        }

        public void CreateSession()
        {

        }

        public Session GetCurrentSession()
        {
            return null;
        }

        public IEnumerable<Store> GetAvailableStores()
        {
            return null;
        }
    }
}
