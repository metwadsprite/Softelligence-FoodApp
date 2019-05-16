using BusinessLogic.Abstractions;
using BusinessLogic.BusinessExceptions;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class Administrator
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        private IPersistenceContext context;

        public Administrator(IPersistenceContext context )
        {
            this.context = context;
        }

        public void AddStore(Store storeToAdd)
        {
            var storeRepo = context.GetStoresRepository();
            if(storeToAdd != null)
            {
                storeRepo.Add(storeToAdd);
            }
            else
            {
                throw new StoreNotFoundException();
            }
        }

        public void Update(Store storeToUpdate, Store newStore)
        {
            var storeRepo = context.GetStoresRepository();
            List<Store> stores = storeRepo.GetAll().ToList();
            if( storeToUpdate != null && newStore != null)
            {
                foreach(Store var in storeRepo.GetAll())
                {
                    if (var.Equals(storeToUpdate))
                    {
                        stores.RemoveAt(storeToUpdate.Id - 1);
                        stores.Insert(storeToUpdate.Id - 1,newStore);
                    }
                }
            }
            else
            {
                throw new StoreNotFoundException();
            }
        }

        public void Remove(int storeId)
        {
            var storeRepo = context.GetStoresRepository();
            List<Store> stores = storeRepo.GetAll().ToList();

            foreach (Store var in stores)
            {
                if(var.Id == storeId)
                {
                    stores.RemoveAt(var.Id);
                    break;
                }
            }
           
        }

        public List<Store> GetStoresList()
        {
            return context.GetStoresRepository().GetAll().ToList();
        }

        public void CreateSession()
        {

        }

        public Session GetActiveSession()
        {
            return null;
        }

        public void UpdateSession(Session activeSession)
        {

        }
    }
}
