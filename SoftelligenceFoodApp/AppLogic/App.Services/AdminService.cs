using BusinessLogic;
using BusinessLogic.Abstractions;
using System.Collections.Generic;


namespace Logic.Implementations
{
    public class AdminService
    {
        private readonly IPersistenceContext dataContext;
        private readonly Administrator administrator;

        public AdminService(IPersistenceContext dataContext)
        {
            this.dataContext = dataContext;
            this.administrator = new Administrator(dataContext);
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

        public void UpdateStore(Store storeToUpdate)
        {
            administrator.Update(storeToUpdate);
        }

        public void RemoveStore(Store storeToRemove)
        {
            administrator.Remove(storeToRemove);
        }

        public void StartSession(Session sessionToStart)
        {
            administrator.CreateSession(sessionToStart);
        }

        public Session GetActiveSession()
        {
            return administrator.GetActiveSession();
        }
        public IEnumerable<Session> GetAllSessions()
        {
            return administrator.GetAllSessions();
        }

        public Store GetStoreById(int id)
        {
            return administrator.GetStoreById(id);
        }

        public Session GetSessionById(int id)
        {
            return administrator.GetSessionById(id);
        }
        public void CloseSession(Session sessionToClose)
        {
            administrator.CloseSession(sessionToClose);
        }
    }
}
