using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions
{
    public interface ISessionsRepository
    {
        void Create(Session sessionToCreate);
        Session GetActiveSession();
    }
}
