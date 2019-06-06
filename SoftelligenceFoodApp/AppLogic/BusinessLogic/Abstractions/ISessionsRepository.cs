using System.Collections.Generic;

namespace BusinessLogic.Abstractions
{
    public interface ISessionsRepository
    {
        void Create(Session sessionToCreate);
        ICollection<Session> GetAll();
        Session GetActiveSession();
        void Update(Session sessionToUpdate);
        void DeleteOrder(Order orderToDelete);
        Session GetById(int id);
        
    }
}
