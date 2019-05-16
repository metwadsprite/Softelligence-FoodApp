using BusinessLogic.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class Session
    {
        private IStoresRepository storesRepository;
        private IUsersRepository usersRepository;

        public Session(IPersistenceContext persistenceContext)
        {
            storesRepository = persistenceContext.GetStoresRepository();
            usersRepository = persistenceContext.GetUsersRepository();
        }
    }
}
