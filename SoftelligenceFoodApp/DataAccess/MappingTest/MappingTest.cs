using Microsoft.VisualStudio.TestTools.UnitTesting;
using EF.DataAccess.DataModel;
using BusinessLogic;
using AutoMapper;
using EF.DataAccess;

namespace MappingTest
{
    [TestClass]
    public class MappingTest
    {

        private readonly EntitiesMapper mapper = new EntitiesMapper();

        [TestInitialize]
        public void Initialize()
        {
            mapper.InitializeMapper();
        }

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
            baseUser.Email = "ceva@yahoo.com";
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


        [TestMethod]
        public void TestSessionMapper()
        {
            Session session = new Session();
            //Element 0 of list
            Order order1 = new Order();
            order1.Price = 5;
            order1.Details = "Ceva";

            //Element 1 of list
            Order order2 = new Order();
            order1.Price = 2;
            order1.Details = "Yes";

            session.Orders.Add(order1);
            session.Orders.Add(order2);

            SessionDO sessionDO = new SessionDO();
            OrderDO orderDO1 = new OrderDO();
            orderDO1.Price = 7;
            orderDO1.Details = "3";
            sessionDO.Orders.Add(orderDO1);

            mapper.MapSessionsDO(session, sessionDO);

            Assert.AreEqual(session.Orders[0], sessionDO.Orders[0]);


        }
    }
}
