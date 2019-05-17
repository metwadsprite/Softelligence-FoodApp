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
            if (storeToUpdate != null)
            {
                storeRepo.Update(storeToUpdate);
            }
            else
            {
                throw new StoreNotFoundException();
            }
        }

        public void Remove(Store storeToRemove)
        {
            var storeRepo = context.GetStoresRepository();
            storeRepo.Remove(storeToRemove);
        
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
            var sessionsRep = context.GetSessionsRepository();
            return sessionsRep.GetActiveSession();
        }

        public void UpdateSession(Session activeSession)
        {

        }
    }
}
