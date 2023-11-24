using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Kapp
{
    public class EmailServer
    {
        public string ConnectionEmail(bool Regist)
        {
            string MessageEmail;
            if (Regist == true)
            {
                MessageEmail = "Пользователь уже был ранее создан";
            }
            else
            {
                MessageEmail = "Пользователь зарегистрировался впервые";
            }
            return MessageEmail;
        }
    }
    
}
