using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class Administrator
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }


        public IEnumerable<Store> GetAllStores()
        {
            return null;
        }
        public void AddStore(Store storeToAdd)
        {
        }

        public void Update(Store storeToUpdate)
        {

        }

        public void Remove(Store storeToRemove)
        {

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
