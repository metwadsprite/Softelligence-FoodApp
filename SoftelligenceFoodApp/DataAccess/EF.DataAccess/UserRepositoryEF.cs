
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Abstractions;
using BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using EF.DataAccess.DataModel;
using AutoMapper;

namespace EF.DataAccess
{
    public class UserRepositoryEF : IUsersRepository
    {

        private readonly ApplicationDbContext dbContext;

        private User MapData(UserDO userDO)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDO>();
            });

            IMapper iMapper = config.CreateMapper();

            var destination = new User();

            destination = iMapper.Map<UserDO, User>(userDO);

            return destination;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var userWithId = await dbContext.Users.SingleOrDefaultAsync( user => user.Id == id);

            return MapData(userWithId);
        }

        public string GetEmail(int id)
        {
            throw new System.NotImplementedException();
        }

        public string GetName(int id)
        {
            throw new System.NotImplementedException();
        }

        public UserRepositoryEF(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


    }
}
