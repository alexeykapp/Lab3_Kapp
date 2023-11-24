using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Kapp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите логин:");
            string login = Console.ReadLine();
            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();
            Console.WriteLine("Подтвердите пароль:");
            string confirmPassword = Console.ReadLine();
            (string Inforamtion, string Login, string Password, string Email) = new Controller().RegistrationController(login, password, confirmPassword);
            //вывод в консоль
            Console.WriteLine($"{Inforamtion}, {Login}, {Password}");
        }
    }
}
