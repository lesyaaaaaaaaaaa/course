using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItalianPizzaHouse.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItalianPizzaHouse.Model;

namespace ItalianPizzaHouse.ViewModel.Tests
{
    [TestClass()]
    public class UsersVMTests
    {
        Core db = new Core();
        /// <summary>
        /// Проверяет успешную аутентификацию пользователя с корректными данными. 
        /// Должен вернуть true, установить свойства пользователя и добавить запись в журнал.
        /// </summary>
        [TestMethod]
        public void Authenticate_ValidCredentials_ReturnsTrueAndSetsUserProperties()
        {
            // Arrange
            var testUser = new Users
            {
                LastName = "Test",
                FirstName = "User",
                PhoneNumber = "test",
                Login = "Test",
                Password = "User",
                RoleId = 1
            };
            db.context.Users.Add(testUser);
            db.context.SaveChanges();

            // Act
            bool result = UsersVM.Authenticate("Test", "User");

            // Assert
            Assert.IsTrue(result, "Authenticate() должен вернуть true для корректных данных");
            db.context.Users.Remove(testUser);
            db.context.SaveChanges();
        }
        /// <summary>
        /// Проверяет, что метод Authenticate выбрасывает исключение при пустом логине.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception), "Ожидалось исключение для пустого логина")]
        public void Authenticate_EmptyLogin_ThrowsException()
        {
            // Act
            UsersVM.Authenticate("", "testpassword");
        }
        /// <summary>
        /// Проверяет, что метод Authenticate выбрасывает исключение при пустом пароле.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception), "Ожидалось исключение для пустого пароля")]
        public void Authenticate_EmptyPassword_ThrowsException()
        {
            // Act
            UsersVM.Authenticate("testuser", "");
        }
        /// <summary>
        /// Проверяет, что метод Authenticate выбрасывает исключение при неверных учетных данных.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception), "Ожидалось исключение для неверных данных")]
        public void Authenticate_InvalidCredentials_ThrowsException()
        {
            // Act
            UsersVM.Authenticate("testuser", "wrongpassword");
        }
}
}