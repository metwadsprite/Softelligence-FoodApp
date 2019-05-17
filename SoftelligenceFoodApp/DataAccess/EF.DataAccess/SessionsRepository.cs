using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic;
using BusinessLogic.Abstractions;

namespace EF.DataAccess
{
    class SessionsRepository : ISessionsRepository
    {
        public void Create(Session sessionToCreate)
        {
            throw new NotImplementedException();
        }

        public Session GetActiveSession()
        {
            throw new NotImplementedException();
        }

        public void Update(Session sessionToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
