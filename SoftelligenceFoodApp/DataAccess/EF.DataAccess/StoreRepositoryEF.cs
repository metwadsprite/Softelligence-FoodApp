using Abstractions;
using BusinessLogic;
using EF.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using DataMapper;

namespace EF.DataAccess
{
    public class StoreRepositoryEF: IStoresRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly EntitiesMapper mapper;
        public StoreRepositoryEF(ApplicationDbContext dbContext, EntitiesMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public Store GetById(int id)
        {
            StoreDO store = dbContext.Stores.FirstOrDefault(a => a.Id == id);
            return mapper.MapData<Store, StoreDO>(store);

        }
        public IEnumerable<Store> GetAll()
        {
            List<Store> StoresList = new List<Store>();
            foreach(StoreDO var in dbContext.Stores)
            {
                StoresList.Add(mapper.MapData<Store, StoreDO>(var));
            }
            return StoresList;
        }
        public void Add(Store storeToAdd)
        {
            dbContext.Stores.Add(mapper.MapData<StoreDO,Store>(storeToAdd));
        }
        public void Remove(Store storeToRemove)
        {
            dbContext.Stores.Remove(mapper.MapData<StoreDO, Store>(storeToRemove));
        }
        public void Update(Store storeToUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}
