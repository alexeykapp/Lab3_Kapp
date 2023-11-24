using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Kapp
{
    public class Controller
    {
        public bool ConnectionRegistrationEmail;
        public (string, string, string,string) RegistrationController (string login, string pass1, string pass2)
        {
            //сначала проверка валидации
            //ты из RegistrationProcess должен получить строку Information, тип bool = true/false,
            //->RegistrationProcess структура:
            //главный метод RunRegistrationProcess (принимает string login, string pass1, string pass2), возвращает (string Inforamtion, bool Check)
            //<-

            
            (bool CheckRegist, string Inf) = new RegistrationProcess().ValidateUserRegistration(login,pass1,pass2);
            //если все проверки прошли, возвращай true
            if (CheckRegist == true)
            {
                var examination = new WorkingDataBase().Examination(login,pass1);
                if (examination == true)
                {
                    ConnectionRegistrationEmail = true;
                    string Email = new EmailServer().ConnectionEmail(ConnectionRegistrationEmail);
                    (string LoginDB, string PasswordDB, string InformationDB) = new WorkingDataBase().DataFromDataBase(login,pass1);
                    return (LoginDB, PasswordDB, InformationDB, Email);
                }
                else
                {
                    ConnectionRegistrationEmail = false;
                    string Email = new EmailServer().ConnectionEmail(ConnectionRegistrationEmail);
                    string Info = new WorkingDataBase().AddInDataBase(login,pass1, pass2, Inf);
                    return (Info, login, pass1,Email);

                }
            }
            else
            { 
                //если у нас ошибка валидации, то тогда мы возвращаем пользователю информацию о ошибке, и то что он ввел
                return (Inf, login, pass1,pass2);
            }

        }
    }
}
