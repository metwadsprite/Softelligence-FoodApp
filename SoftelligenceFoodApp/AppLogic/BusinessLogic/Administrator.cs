using BusinessLogic.BusinessExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class Administrator
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        private List<Store> Stores = new List<Store>();

        private List<Store> AvailableStores = new List<Store>();


        public void AddStore(Store storeToAdd)
        {
            if(storeToAdd != null)
            {
                Stores.Add(storeToAdd);
            }
            else
            {
                throw new StoreNotFoundException();
            }
        }

        public void Update(Store storeToUpdate, Store newStore)
        {
            if( storeToUpdate != null && newStore != null)
            {
                foreach(Store var in Stores)
                {
                    if (var.Equals(storeToUpdate))
                    {
                        Stores.RemoveAt(storeToUpdate.Id - 1);
                        Stores.Insert(storeToUpdate.Id-1,newStore);
                    }
                }
            }
            else
            {
                throw new StoreNotFoundException();
            }
        }

        public void Remove(int storeId)
        {
            foreach(Store var in Stores)
            {
                if(var.Id == storeId)
                {
                    Stores.RemoveAt(var.Id);
                    break;
                }
            }
           
        }

        public void CreateSession()
        {

        }

        public Session GetCurrentSession()
        {
            return null;
        }

    }
}
