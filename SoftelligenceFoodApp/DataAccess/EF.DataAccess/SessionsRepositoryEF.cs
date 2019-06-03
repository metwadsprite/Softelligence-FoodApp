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
                var sessionDO = mapper.MapData<SessionDO, Session>(sessionToCreate);

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
                var activeSession = mapper.MapData<Session, SessionDO>(session); 

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
                    // might not work when updating order
                    /// TODO: fix if that's the case
                }
            }

            dbContext.Sessions.Update(sessionDO);
            dbContext.SaveChanges();
        }

        public void DeleteOrder(Order orderToRemove)
        {
            OrderDO orderDO = dbContext.Orders.SingleOrDefault(order => orderToRemove.Id == order.Id);
            if (orderDO != null)
            {
                dbContext.Orders.Remove(orderDO);
            }
            else
            {
                throw new EntryPointNotFoundException();
            }
            dbContext.SaveChanges();
        }

        public ICollection<Session> GetAll()
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
                .Include(tempSession => tempSession.Orders)
                    .ThenInclude(order => order.User)
                .Include(tempSession => tempSession.SessionStore)
                    .ThenInclude(sessstore => sessstore.Store)
                        .ThenInclude(store => store.Menu)
                .FirstOrDefault(a => a.Id == id);

            if (session != null)
            {
                var sessionToReturn = mapper.MapData<Session, SessionDO>(session);

                foreach (var sStore in session.SessionStore)
                {
                    sessionToReturn.Stores.Add(mapper.MapData<Store, StoreDO>(sStore.Store));
                }
                return sessionToReturn;
            }
            else
            {
                throw new SessionNotFoundException();
            }
        }

    }
}
