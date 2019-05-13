using System.Collections.Generic;
using BusinessLogic;

namespace Logic.Abstractions
{
    public interface IStoreService
    {
        IEnumerable<Store> GetStores();
    }
}
