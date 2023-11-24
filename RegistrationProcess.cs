using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Serilog;
using Microsoft.SqlServer.Server;

namespace Lab3_Kapp
{
    public class RegistrationProcess
    {

        public List<string> existingLogins = new List<string> { "user1", "user2", "user3", "user4", "user5" };

        public (bool, string) ValidateUserRegistration(string login, string password, string confirmPassword)
        {
            if (login.Length < 5)
            {
                return (false, "Логин слишком короткий.");
            }

            bool isValidLogin = Regex.IsMatch(login, @"^(?:\+\d{1,3}-)?\d{1,4}-\d{1,4}-\d{1,4}$|^\w+@\w+\.\w+$");
            if (!isValidLogin)
            {
                return (false, "Логин не соответствует формату телефона или электронной почты.");
            }

            if (password != confirmPassword)
            {
                return (false, "Пароль и подтверждение пароля не совпадают.");
            }

            if (password.Length < 7)
            {
                return (false, "Пароль слишком короткий.");
            }

            if (!IsCyrillic(password))
            {
                return (false, "Пароль должен содержать только кириллические символы.");
            }

            bool hasLower = false;
            bool hasUpper = false;
            bool hasDigit = false;
            bool hasSpecialChar = false;

            foreach (char c in password)
            {
                if (char.IsLower(c))
                {
                    hasLower = true;
                }
                else if (char.IsUpper(c))
                {
                    hasUpper = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else if (char.IsSymbol(c) || char.IsPunctuation(c))
                {
                    hasSpecialChar = true;
                }

                if (!IsCyrillic(c))
                {
                    return (false, "Пароль содержит символы, которые не являются кириллицей.");
                }
            }

            if (!hasLower)
            {
                return (false, "Пароль не содержит буквы в нижнем регистре.");
            }

            if (!hasUpper)
            {
                return (false, "Пароль не содержит буквы в верхнем регистре.");
            }

            if (!hasDigit)
            {
                return (false, "Пароль не содержит цифры.");
            }

            if (!hasSpecialChar)
            {
                return (false, "Пароль не содержит спецсимвола.");
            }

            return (true, "");
        }

        static bool IsCyrillic(string input)
        {
            return Regex.IsMatch(input, @"^[А-Яа-я]+$");
        }

        static bool IsCyrillic(char c)
        {
            return (c >= 'А' && c <= 'я') || (c >= 'ё' && c <= 'ё');
        }

        static void LogInformation(string login, string password, string confirmPassword)
        {
            Log.Information("{DateTime}: Успешная регистрация - Логин: {Login}, Пароль: {Password}, Подтверждение: {ConfirmPassword}",
                DateTime.Now, login, MaskPassword(password), MaskPassword(confirmPassword));
        }

        static void LogError(string login, string password, string confirmPassword, string errorMessage)
        {
            Log.Error(
                "{DateTime}: Неуспешная регистрация - Логин: {Login}, Пароль: {Password}, Подтверждение: {ConfirmPassword}, Ошибка: {ErrorMessage}",
                DateTime.Now, login, MaskPassword(password), MaskPassword(confirmPassword), errorMessage);
        }

        static string MaskPassword(string password)
        {
            return new string('*', password.Length);
        }

    }
}
