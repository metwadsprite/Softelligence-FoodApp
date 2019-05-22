using BusinessLogic.Abstractions;
using BusinessLogic;
using EF.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;

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
            var store = dbContext.Stores.FirstOrDefault(a => a.Id == id);
            return mapper.MapData<Store, StoreDO>(store);

        }
        public IEnumerable<Store> GetAll()
        {
            List<Store> StoresList = new List<Store>();
            var stores = dbContext.Stores.AsEnumerable();

            foreach(var store in stores)
            {
                StoresList.Add(mapper.MapData<Store, StoreDO>(store));
            }

            return StoresList;
        }

        public void Add(Store storeToAdd)
        {
            if (storeToAdd != null)
            {
                dbContext.Stores.Add(mapper.MapData<StoreDO, Store>(storeToAdd));
            }
            else
            {
                throw new EntryPointNotFoundException();
            }
        }
        public void Remove(Store storeToRemove)
        {
            if (storeToRemove != null)
            {
                dbContext.Stores.Remove(mapper.MapData<StoreDO, Store>(storeToRemove));
            }
            else
            {
                throw new EntryPointNotFoundException();
            }
        }
        public void Update(Store storeToUpdate)
        {
            StoreDO storeDO = dbContext.Stores.SingleOrDefault(store => storeToUpdate.Id == store.Id);
            //mapper.MapStoresDO(storeToUpdate, storeDO);
            dbContext.Stores.Update(storeDO);

        }
    }
}
