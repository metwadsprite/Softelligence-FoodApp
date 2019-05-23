using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic;
using BusinessLogic.Abstractions;
using BusinessLogic.BusinessExceptions;
using EF.DataAccess.DataModel;

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
                //mapper.MapToSessionsDO(sessionToCreate, sessionDO);
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
            SessionDO session = (SessionDO)dbContext.Sessions.FirstOrDefault(s => s.IsActive == true);
            if (session != null)
            {
                Session activeSession = new Session();
                //mapper.MapToSession(session, activeSession);
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
            mapper.MapToSessionsDO(sessionToUpdate, sessionDO);
            dbContext.Sessions.Update(sessionDO);

        }

    }
}
