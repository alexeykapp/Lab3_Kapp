using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Kapp
{
    public class WorkingDataBase
    {
        public static RegistrationEntities db = new RegistrationEntities();
        //строка подключения
        public bool Examination(string login, string pass1)
        {
            var isexist = WorkingDataBase.db.RegistrationUser.Where(x => x.Login == login && x.Password == pass1).FirstOrDefault();
            if (isexist == null)
            {
                return false;
            }
            return true;
        }
        public (string, string, string) DataFromDataBase(string login, string pass1)
        {
            //метод, который находит в базе данных логин и пароль из бд, три строки достаешь - логин, пароль, строчку подтверждения авторизации - проверка прошла
            var registration = WorkingDataBase.db.RegistrationUser.Where(x => x.Login == login && x.Password == pass1).FirstOrDefault();
            return (registration.Login, registration.Password, registration.ResultAutorizaton);
        }
        public string AddInDataBase(string login, string pass1, string pass2, string InfValidat)
        {
            //добавление в базу данных логин, пароль, строчка информации о авторизации (прошел), возвращаешь строчку - что данные добавлены в бд
            var registration = new RegistrationUser
            {
                Login = login,
                Password = pass1,
                ConfirmPassword = pass2,
                ResultAutorizaton = InfValidat
            };
            return "Успешно";
        }
    }
}
