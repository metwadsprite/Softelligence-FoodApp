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
            storeRepo.Add(storeToAdd);
        }

        public void Update(Store storeToUpdate)
        {
            var storeRepo = context.GetStoresRepository();
            storeRepo.Update(storeToUpdate);
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

        public void CreateSession(Session sessionToCreate)
        {
            var sessionsRepo = context.GetSessionsRepository();
            sessionsRepo.Create(sessionToCreate);
        }

        public Session GetActiveSession()
        {
            var sessionsRep = context.GetSessionsRepository();
            return sessionsRep.GetActiveSession();
        }

        public void UpdateSession(Session activeSession)
        {
            var sessionsRepo = context.GetSessionsRepository();
            sessionsRepo.Update(activeSession);
        }

        public Store GetById(int id)
        {
            var storeRepo = context.GetStoresRepository();
            return storeRepo.GetById(id);
        }
    }
}
