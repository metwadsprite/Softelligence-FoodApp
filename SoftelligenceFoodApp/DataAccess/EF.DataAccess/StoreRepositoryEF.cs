using BusinessLogic.Abstractions;
using BusinessLogic;
using EF.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
            var store = dbContext.Stores
                .Include(tempStore => tempStore.Menu)
                .FirstOrDefault(a => a.Id == id);
            return mapper.MapData<Store, StoreDO>(store);

        }
        public ICollection<Store> GetAll()
        {
            List<Store> StoresList = new List<Store>();
            var stores = dbContext.Stores
                .Include(store => store.Menu)
                .AsEnumerable();

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
                var store = mapper.MapData<StoreDO, Store>(storeToAdd);
                dbContext.Stores.Add(store);
                dbContext.SaveChanges();
            }
            else
            {
                throw new EntryPointNotFoundException();
            }
        }
        public void Remove(Store storeToRemove)
        {
            StoreDO store = new StoreDO();
            store = mapper.MapData<StoreDO, Store>(storeToRemove);
            if (store != null)
            {
                dbContext.Stores.Remove(store);
            }
            else
            {
                throw new EntryPointNotFoundException();
            }
            dbContext.SaveChanges();
        }
        public void Update(Store storeToUpdate)
        {
            StoreDO storeDO = dbContext.Stores.SingleOrDefault(store => storeToUpdate.Id == store.Id);
            storeDO.Name = storeToUpdate.Name;
            storeDO.Menu.Hyperlink = storeToUpdate.Menu.Hyperlink;
            dbContext.Stores.Update(storeDO);
            dbContext.SaveChanges();

        }
    }
}
