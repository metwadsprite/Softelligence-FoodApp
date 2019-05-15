
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Abstractions;
using BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using EF.DataAccess.DataModel;
using AutoMapper;
using DataMapper;

namespace EF.DataAccess
{
    public class UserRepositoryEF : IUsersRepository
    {

        private readonly ApplicationDbContext dbContext;
        private readonly EntitiesMapper mapper;
        //inject / parametru data mapper

         
        public async Task<User> GetByIdAsync(int id)
        {
            var userWithId = await dbContext.Users.SingleOrDefaultAsync( user => user.Id == id);

            return mapper.MapData<User, UserDO>(userWithId);
        }

        public string GetEmail(int id)
        {
            throw new System.NotImplementedException();
        }

        public string GetName(int id)
        {
            throw new System.NotImplementedException();
        }

        //add parameter
        public UserRepositoryEF(ApplicationDbContext dbContext, EntitiesMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }


    }
}
