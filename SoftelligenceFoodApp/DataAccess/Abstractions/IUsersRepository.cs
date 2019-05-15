using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions
{
    public interface IUsersRepository
    {
        User GetById(int id);
        String GetEmail(int id);
        String GetName(int id);
    }
}
