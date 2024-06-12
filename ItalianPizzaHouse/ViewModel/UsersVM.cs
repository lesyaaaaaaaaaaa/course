using ItalianPizzaHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPizzaHouse.ViewModel
{
    public class UsersVM
    {
        static Core db = new Core();
        public static int UserId { get; private set; }
        public static int UserRole { get; private set; }

        public static bool Authenticate(string login, string password)
        {
            if (String.IsNullOrEmpty(login))
            {
                throw new Exception("Вы не ввели логин.");
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new Exception("Вы не ввели пароль.");
            }

            var user = db.context.Users.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (user != null)
            {
                UserId = user.IdUser;
                UserRole = user.RoleId;
                HistoryVM.EventHappened(DateTime.Now, $"Авторизация пользователя {user.LastName} {user.FirstName[0]}.", UserId);
                return true;
            }

            else
            {
                throw new Exception("Пользователь не найден.");
            }

        }

        public static void Logout()
        {
            HistoryVM.EventHappened(DateTime.Now, $"Выход пользователя с ID {UserId}.", UserId);
            UserId = 0;
            UserRole = 0;
        }
    }
}
