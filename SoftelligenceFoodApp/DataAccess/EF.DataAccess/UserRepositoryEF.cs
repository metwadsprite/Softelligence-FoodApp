
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Abstractions;
using BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using EF.DataAccess.DataModel;
using AutoMapper;
using System.Linq;

namespace EF.DataAccess
{
    public class UserRepositoryEF : IUsersRepository
    {

        private readonly ApplicationDbContext dbContext;
        private readonly EntitiesMapper mapper;

        public User GetById(int id)
        {
            var userWithId = dbContext.Users.SingleOrDefault( user => user.Id == id);

            return mapper.MapData<User, UserDO>(userWithId); 
        }

        public string GetEmail(int id)
        {
            return GetById(id).Email;
        }

        public string GetName(int id)
        {
            return GetById(id).Name;
        }

        public UserRepositoryEF(ApplicationDbContext dbContext, EntitiesMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

    }
}
