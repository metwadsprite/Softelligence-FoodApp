using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic;
using BusinessLogic.Abstractions;

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
            throw new NotImplementedException();
        }

        public Session GetActiveSession()
        {
            throw new NotImplementedException();
        }
    }
}
