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

        public int Id { get; set; }
        public ICollection<Store> ActiveStores { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsActive { get; set; }

        public Session(IPersistenceContext persistenceContext)
        {
            storesRepository = persistenceContext.GetStoresRepository();
            usersRepository = persistenceContext.GetUsersRepository();
        }
    }
}
