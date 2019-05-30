using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic;
using BusinessLogic.Abstractions;
using BusinessLogic.BusinessExceptions;
using EF.DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;

namespace EF.DataAccess
{
    public class SessionsRepositoryEF : ISessionsRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly EntitiesMapper mapper;
        public SessionsRepositoryEF(ApplicationDbContext dbContext, EntitiesMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void Create(Session sessionToCreate)
        {
            if (sessionToCreate != null)
            {
                SessionDO sessionDO = new SessionDO();
                //sessionDO = mapper.MapData<SessionDO, Session>(sessionToCreate);

                sessionDO.StartTime = sessionToCreate.StartTime;
                sessionDO.IsActive = sessionToCreate.IsActive;
                sessionDO.SessionStore = new List<SessionStoreDO>();
                foreach(var item in sessionToCreate.Orders)
                {
                    var sessionVar = mapper.MapData<OrderDO, Order>(item);
                    sessionDO.Orders.Add(sessionVar);
                }

                foreach(var item in sessionToCreate.Stores)
                {
                    var storeDoVar = mapper.MapData<StoreDO,Store>(item);
                    var sessionDoVar = mapper.MapData<SessionDO, Session>(sessionToCreate);
                    SessionStoreDO sessionStoreDo = new SessionStoreDO();
                    sessionStoreDo.Session = sessionDoVar;
                    sessionStoreDo.Store = storeDoVar;
                    sessionDO.SessionStore.Add(sessionStoreDo);

                }

                sessionDO.IsActive = true;
                dbContext.Add(sessionDO);
                dbContext.SaveChanges();
            }
            else
            {
                throw new SessionNotFoundException();
            }
        }

        public Session GetActiveSession()
        {
            SessionDO session = dbContext.Sessions
                .Include(tempSession => tempSession.Orders)
                    .ThenInclude(order => order.User)
                .Include(tempSession => tempSession.SessionStore)
                    .ThenInclude(sessstore => sessstore.Store)
                        .ThenInclude(store => store.Menu)
                .FirstOrDefault(s => s.IsActive == true);

            if (session != null)
            {
                Session activeSession;
                activeSession = mapper.MapData<Session, SessionDO>(session); 

                foreach (var sStore in session.SessionStore)
                {
                    activeSession.Stores.Add(mapper.MapData<Store, StoreDO>(sStore.Store));
                }
                
                return activeSession;
                
            }
            else
            {
                throw new SessionNotFoundException();
            }
        }

        public void Update(Session sessionToUpdate)
        {
            var sessionDO = dbContext.Sessions.FirstOrDefault(session => sessionToUpdate.Id == session.Id);

            var sessionToUpdateDO = mapper.MapData<SessionDO, Session>(sessionToUpdate);
            
            dbContext.Entry(sessionDO).CurrentValues.SetValues(sessionToUpdateDO);

            foreach (var orderToUpdate in sessionToUpdate.Orders)
            {
                var orderToUpdateDO = mapper.MapData<OrderDO, Order>(orderToUpdate);
                var orderDO = dbContext.Orders.FirstOrDefault(order => order.Id == orderToUpdateDO.Id);

                if (orderDO == null)
                {
                    sessionDO.Orders.Add(orderToUpdateDO);
                }
                else
                {
                    dbContext.Entry(orderDO).CurrentValues.SetValues(orderToUpdateDO);
                }
            }

            dbContext.Sessions.Update(sessionDO);
            dbContext.SaveChanges();
        }
        public IEnumerable<Session> GetAll()
        {
            List<Session> SessionsList = new List<Session>();
            var sessions = dbContext.Sessions
                .Include(tempSession => tempSession.Orders)
                    .ThenInclude(order => order.User)
                .Include(tempSession => tempSession.SessionStore)
                    .ThenInclude(sStore => sStore.Store)
                        .ThenInclude(store => store.Menu)
                .AsEnumerable();

            foreach (var session in sessions)
            {
                var sessionToAdd = mapper.MapData<Session, SessionDO>(session);
                sessionToAdd.Stores = new List<Store>();

                foreach (var sStore in session.SessionStore)
                {
                    sessionToAdd.Stores.Add(mapper.MapData<Store, StoreDO>(sStore.Store));
                }

                SessionsList.Add(sessionToAdd);
            }
            return SessionsList;
        }

        public Session GetById(int id)
        {
            var session = dbContext.Sessions
                .FirstOrDefault(a => a.Id == id);
                 var sessionToReturn = mapper.MapData<Session, SessionDO>(session);
            return sessionToReturn;
           
        }
    }
}
