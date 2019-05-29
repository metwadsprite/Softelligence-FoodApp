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
                sessionDO = mapper.MapData<SessionDO, Session>(sessionToCreate);
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
                .Include(tempSession => tempSession.SessionStore)
                    .ThenInclude(sessstore => sessstore.Store)
                        .ThenInclude(store => store.Menu)
                .FirstOrDefault(s => s.IsActive == true);
            if (session != null)
            {
                Session activeSession = new Session();
                mapper.MapData<Session, SessionDO>(session);
                activeSession.Stores = new List<Store>();

                foreach (var sStore in session.SessionStore)
                {
                    activeSession.Stores.Add(mapper.MapData<Store, StoreDO>(sStore.Store));
                }

                return (activeSession);
            }
            else
            {
                throw new SessionNotFoundException();
            }
        }

        public void Update(Session sessionToUpdate)
        {
            SessionDO sessionDO = dbContext.Sessions.SingleOrDefault(session => sessionToUpdate.Id == session.Id);

            sessionDO = mapper.MapData<SessionDO, Session>(sessionToUpdate);
            sessionDO.SessionStore = new List<SessionStoreDO>();

            foreach (var store in sessionToUpdate.Stores)
            {
                sessionDO.SessionStore.Add(new SessionStoreDO
                {
                    Store = mapper.MapData<StoreDO, Store>(store),
                    Session = sessionDO
                });
            }

            dbContext.Sessions.Update(sessionDO);
            dbContext.SaveChanges();
        }
        public IEnumerable<Session> GetAll()
        {
            List<Session> SessionsList = new List<Session>();
            var sessions = dbContext.Sessions
                .Include(tempSession => tempSession.Orders)
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


    }
}
