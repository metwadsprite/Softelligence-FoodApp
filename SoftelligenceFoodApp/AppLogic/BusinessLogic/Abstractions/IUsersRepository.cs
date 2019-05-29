using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Abstractions
{
    public interface IUsersRepository
    {
        User GetByName(string name);
        User GetByEmail(string email);
        int GetId(string name);
    }
}
