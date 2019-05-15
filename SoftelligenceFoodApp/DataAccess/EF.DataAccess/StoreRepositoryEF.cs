using Abstractions;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF.DataAccess
{
    class StoreRepositoryEF: IStoresRepository
    {
        private readonly ApplicationDbContext dbContext;
        public StoreRepositoryEF(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Store GetById(int id)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<Store> GetAll()
        {
            throw new System.NotImplementedException();
        }
        public void Add(Store storeToAdd)
        {
            throw new System.NotImplementedException();
        }
        public void Remove(Store storeToRemove)
        {
            throw new System.NotImplementedException();
        }
        public void Update(Store storeToUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}
