using BusinessLogic.Abstractions;
using BusinessLogic;
using EF.DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.BusinessExceptions;

namespace EF.DataAccess
{
    public class StoreRepositoryEF : IStoresRepository
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

            foreach (var store in stores)
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
            StoreDO storeDO = dbContext.Stores.SingleOrDefault(store => storeToRemove.Id == store.Id);
            if (storeDO == null)
            {
                throw new EntryPointNotFoundException();
            }
            else if(storeDO.IsActive)
            {
                throw new StoreIsActiveException();
            }
            else
            {
                dbContext.Stores.Remove(storeDO);
            }
            dbContext.SaveChanges();
        }

        public void Update(Store storeToUpdate)
        {
            StoreDO storeDO = dbContext.Stores.SingleOrDefault(store => storeToUpdate.Id == store.Id);
            if (storeDO != null)
            {
                storeDO.Name = storeToUpdate.Name;
                storeDO.Menu.Hyperlink = storeToUpdate.Menu.Hyperlink;
                storeDO.Menu.Image = storeToUpdate.Menu.Image;
                storeDO.IsActive = storeToUpdate.IsActive;
                dbContext.Stores.Update(storeDO);
            }
            else
            {
                throw new EntryPointNotFoundException();
            }
            dbContext.SaveChanges();
        }
       
        
    }
}
