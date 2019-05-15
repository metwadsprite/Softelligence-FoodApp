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
        public void TestMapUserToUserDO()
        {

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

        [TestMethod]
        public void TestMapUserToUserDO2()
        {

            UserDO baseUser = new UserDO();
            baseUser.Email = "titlu@yahoo.com";
            baseUser.Name = "Prenume";
            baseUser.Id = 2;


            User destUser = new User();
            destUser = mapper.MapData<User, UserDO>(baseUser);

            Assert.AreEqual(destUser.Email, baseUser.Email);
            Assert.AreEqual(destUser.Name, baseUser.Name);
            Assert.AreEqual(destUser.Id, baseUser.Id);
        }

        [TestMethod]
        public void TestMapDifferentEmail()
        {
            UserDO baseUser = new UserDO();
            baseUser.Email = "titlu@yahoo.com";
            baseUser.Name = "Prenume";
            baseUser.Id = 2;


            User destUser = new User();
            destUser.Email = "ceva@yahoo.com";
            destUser.Name = "Prenume";
            destUser.Id = 2;

            Assert.AreNotEqual(destUser.Email, baseUser.Email);
            Assert.AreEqual(destUser.Name, baseUser.Name);
            Assert.AreEqual(destUser.Id, baseUser.Id);

        }

        [TestMethod]
        public void TestMapDifferentName()
        {
            UserDO baseUser = new UserDO();
            baseUser.Email = "ceva@yahoo.com";
            baseUser.Name = "Prenume";
            baseUser.Id = 2;


            User destUser = new User();
            destUser.Email = "ceva@yahoo.com";
            destUser.Name = "Prenumele";
            destUser.Id = 2;

            Assert.AreEqual(destUser.Email, baseUser.Email);
            Assert.AreNotEqual(destUser.Name, baseUser.Name);
            Assert.AreEqual(destUser.Id, baseUser.Id);

        }

        [TestMethod]
        public void TestMapDifferentId()
        {
            UserDO baseUser = new UserDO();
            baseUser.Email = "ceva@yahoo.com";
            baseUser.Name = "Prenume";
            baseUser.Id = 2;


            User destUser = new User();
            destUser.Email = "ceva@yahoo.com";
            destUser.Name = "Prenume";
            destUser.Id = 8;

            Assert.AreEqual(destUser.Email, baseUser.Email);
            Assert.AreEqual(destUser.Name, baseUser.Name);
            Assert.AreNotEqual(destUser.Id, baseUser.Id);

        }
    }
}
