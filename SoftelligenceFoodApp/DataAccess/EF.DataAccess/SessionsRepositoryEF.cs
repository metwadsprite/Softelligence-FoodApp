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
                dbContext.Add(mapper.MapData<SessionDO, Session>(sessionToCreate));
            }
            else
            {
                throw new SessionNotFoundException();
            }
        }

        public Session GetActiveSession()
        {
            SessionDO session = dbContext.Sessions.SingleOrDefault(s => s.IsActive == true);
            if(session!= null)
                return (mapper.MapData<Session,SessionDO>(session));
            else
            {
                throw new SessionNotFoundException();
            }
        }

        public void Update(Session sessionToUpdate)
        {
           
            SessionDO sessionDO = dbContext.Sessions.SingleOrDefault(session => sessionToUpdate.Id == session.Id);
            sessionDO.IsActive = sessionToUpdate.IsActive;
            //sessionDO.Orders = sessionToUpdate;
            //new = mapper.MapData<SessionDO, Session>(newSession, sessionDo);
            

            throw new NotImplementedException();
        }

    }
}
