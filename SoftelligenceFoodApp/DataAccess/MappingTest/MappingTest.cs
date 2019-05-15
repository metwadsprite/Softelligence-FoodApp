using Microsoft.VisualStudio.TestTools.UnitTesting;
using EF.DataAccess.DataModel;
using BusinessLogic;
using AutoMapper;
using DataMapper;

namespace MappingTest
{
    [TestClass]
    public class MappingTest
    {
        private readonly EntitiesMapper mapper = new EntitiesMapper();
        [TestMethod]
        public void TestMapUserToUserDO1()
        {
            //new userDO (predefinit) -> user
            //map to user
            //assert user.id = id , etc

            UserDO baseUser = new UserDO();
            baseUser.Email = "ceva@yahoo.com";
            baseUser.Name = "Nume";
            baseUser.Id = 5;


            User destUser = new User();

            destUser = mapper.MapData<User, UserDO>(baseUser);

            Assert.AreEqual(destUser.Email, "ceva@yahoo.com");
            Assert.AreEqual(destUser.Name, "Nume");
            Assert.AreEqual(destUser.Id, 5);
        }
    }
}
