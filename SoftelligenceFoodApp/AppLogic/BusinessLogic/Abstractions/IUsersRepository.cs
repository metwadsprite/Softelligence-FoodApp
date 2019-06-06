
namespace BusinessLogic.Abstractions
{
    public interface IUsersRepository
    {
        User GetByName(string name);
        User GetByEmail(string email);
        bool FindUser(string email);
        void Create(User user);
        int GetId(string name);
    }
}
