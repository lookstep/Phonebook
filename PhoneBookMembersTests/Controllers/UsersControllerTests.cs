using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneBookMembers.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookMembers.Controllers.Tests
{
    [TestClass()]
    public class UsersControllerTests
    {

        [TestMethod()]
        public void SetNewUsersDateTest()
        {
            //Arrange
            var random = new Random();
            var Name = Guid.NewGuid().ToString();
            var SecondName = Guid.NewGuid().ToString();
            var Gender = Guid.NewGuid().ToString();
            var randomBalance = random.Next(0, 100);

            //Act
            UsersController user = new(Name);
            user.SetNewUsersDate(Name, SecondName, Gender, randomBalance);

            //Assert
            Assert.AreEqual(user.CurrentUser.Name, Name);
            Assert.AreEqual(user.CurrentUser.SecondName, SecondName);
            Assert.AreEqual(user.CurrentUser.Gender, Gender);
            Assert.AreEqual(user.CurrentUser.Balance, randomBalance);
        }
        [TestMethod()]
        public void AddBalanceTest()
        {
            //Arrange
            var random = new Random();
            var randomNumber = random.Next(0, 100);
            var GUID = Guid.NewGuid().ToString();

            //Act
            UsersController user = new(GUID);
            user.SetNewUsersDate(GUID, GUID, GUID, randomNumber);
            var addBalance = user.AddBalance(randomNumber);

            //Assert
            Assert.AreNotEqual(user.CurrentUser.Balance, addBalance);

        }
    }
}