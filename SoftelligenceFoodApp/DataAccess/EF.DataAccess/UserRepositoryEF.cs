
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.Abstractions;
using BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using EF.DataAccess.DataModel;
using AutoMapper;
using System.Linq;
using System;
using BusinessLogic.Business.Exceptions;

namespace EF.DataAccess
{
    public class UserRepositoryEF : IUsersRepository
    {

        private readonly ApplicationDbContext dbContext;
        private readonly EntitiesMapper mapper;

        public User GetByName(string name)
        {

            var userWithId = dbContext.Users.SingleOrDefault(user => user.Name == name);

            if (userWithId == null)
            {
                throw new UserNotFoundException("User not found");
            }

            return mapper.MapData<User, UserDO>(userWithId);
        }

        public User GetByEmail(string email)
        {
            var userWithEmail = dbContext.Users.SingleOrDefault(user => user.Email == email);

            if (userWithEmail == null)
            {
                throw new UserNotFoundException("User not found");
            }

            return mapper.MapData<User, UserDO>(userWithEmail);
        }

        public int GetId(string name)
        {
            return GetByName(name).Id;
        }

        public UserRepositoryEF(ApplicationDbContext dbContext, EntitiesMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

    }
}
