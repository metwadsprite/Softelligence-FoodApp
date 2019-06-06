using BusinessLogic.Abstractions;
using BusinessLogic;
using EF.DataAccess.DataModel;
using System.Linq;
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

        public bool FindUser(string email)
        {
            var userWithEmail = dbContext.Users.SingleOrDefault(user => user.Email == email);

            if(userWithEmail == null)
            {
                return false;
            }
            else
            {
                return true;
            }
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


        public void Create(User user)
        {
            if (user != null)
            {
                var userDO = mapper.MapData<UserDO, User>(user);

                dbContext.Add(userDO);
                dbContext.SaveChanges();
            }
            else
            {
                throw new UserNotFoundException();
            }
        }

    }
}
