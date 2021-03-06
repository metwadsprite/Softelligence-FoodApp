﻿using BusinessLogic.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class Administrator
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        private readonly IPersistenceContext context;

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

        public Store GetStoreById(int id)
        {
            var storeRepo = context.GetStoresRepository();
            return storeRepo.GetById(id);
        }

        public IEnumerable<Session> GetAllSessions()
        {
            var sessionRepo = context.GetSessionsRepository();
            return sessionRepo.GetAll();
        }

        public Session GetSessionById(int id)
        {
            var sessionRepo = context.GetSessionsRepository();
            return sessionRepo.GetById(id);
        }

        public void CloseSession(Session sessionToClose)
        {
            var sessionRepo = context.GetSessionsRepository();
            sessionToClose.IsActive = false;
            foreach (var order in sessionToClose.Orders)
            {
                order.IsActive = false;
                sessionToClose.UpdateOrder(order.Id, order);

            }
            foreach (var store in sessionToClose.Stores)
            {
                store.IsActive = false;
                Update(store);
            }
            sessionRepo.Update(sessionToClose);
        }

        public void CloseRestaurant(Store storeToClose, Session currentSession)
        {
            foreach (var order in currentSession.Orders)
            {
                order.IsActive = false;
                currentSession.UpdateOrder(order.Id, order);

            }
            storeToClose.IsActive = false;
            Update(storeToClose);
        }
    }
}
