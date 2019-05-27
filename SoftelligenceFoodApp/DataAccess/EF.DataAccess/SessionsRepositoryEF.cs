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
            SessionDO session = (SessionDO)dbContext.Sessions
                .Include(tempSession => tempSession.Orders)
                .Include(tempSession => tempSession.SessionStore)
                .FirstOrDefault(s => s.IsActive == true);
            if (session != null)
            {
                Session activeSession = new Session();
                mapper.MapData<Session, SessionDO>(session);
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
            dbContext.Sessions.Update(sessionDO);

        }
        public IEnumerable<Session> GetAll()
        {
            List<Session> SessionsList = new List<Session>();
            var sessions = dbContext.Sessions.AsEnumerable();

            foreach (var session in sessions)
            {
                SessionsList.Add(mapper.MapData<Session, SessionDO>(session));
            }
            return SessionsList;
        }
    }
}
