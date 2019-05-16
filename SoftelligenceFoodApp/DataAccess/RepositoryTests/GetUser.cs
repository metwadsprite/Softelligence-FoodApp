using Microsoft.VisualStudio.TestTools.UnitTesting;
using EF.DataAccess.DataModel;
using BusinessLogic;
using AutoMapper;
using DataMapper;
using EF.DataAccess;

namespace RepositoryTests
{

    [TestClass]
    public class GetUserTests
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        EntitiesMapper mapper = new EntitiesMapper();
        UserRepositoryEF repository;

        public GetUserTests()
        {
            repository = new UserRepositoryEF(dbContext, mapper);
        }

        [TestMethod]
        public void GetUserById()
        {
            UserDO baseUser = new UserDO();
            baseUser.Email = "ceva@yahoo.com";
            baseUser.Name = "Nume";
            baseUser.Id = 5;

            User user = repository.GetById(5);

            Assert.AreEqual("ceva@yahoo.com", user.Email);
            Assert.AreEqual("Nume", user.Name);
            Assert.AreEqual(5, user.Id);
        }
    }
}
