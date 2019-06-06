using System.Collections.Generic;

namespace BusinessLogic.Abstractions
{
    public interface IStoresRepository
    {
        Store GetById(int id);
        ICollection<Store> GetAll();
        void Add(Store storeToAdd);
        void Remove(Store storeToRemove);
        void Update(Store storeToUpdate);
   
    }
}
