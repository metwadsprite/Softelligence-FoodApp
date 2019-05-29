using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Abstractions
{
    public interface ISessionsRepository
    {
        void Create(Session sessionToCreate);
        IEnumerable<Session> GetAll();
        Session GetActiveSession();
        void Update(Session sessionToUpdate);
        Session GetById(int id);
    }
}
