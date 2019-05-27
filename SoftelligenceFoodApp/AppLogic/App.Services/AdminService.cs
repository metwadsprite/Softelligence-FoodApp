
using BusinessLogic;
using BusinessLogic.Abstractions;
using System.Collections.Generic;


namespace Logic.Implementations
{
    public class AdminService
    {
        private readonly IPersistenceContext dataContext;
        private Administrator administrator;

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
            var storeRepo = dataContext.GetSessionsRepository();
            return storeRepo.GetAll();
        }

        public Store GetById(int id)
        {
            return administrator.GetById(id);
        }
    }
}
