using System;
using System.Collections.Generic;
using System.Text;
using Abstractions;
using BusinessLogic;
using Logic.Abstractions;

namespace Logic.Implementations
{
    public class StoreService : IStoreService
    {

        private IPersistenceContext DataContext;

        public StoreService(IPersistenceContext dataContext)
        {
            this.DataContext = dataContext;
        }
        
        public IEnumerable<Store> GetStores()
        {
            /// TODO :
            throw new NotImplementedException();
        }
    }
}
