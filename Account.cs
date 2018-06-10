using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRY_DB
{
    class Account
    {
        int m_ID;
        string m_Login;
        string m_Password;

        public int ID
        {
            get
            {
                return m_ID;
            }
            set
            {
                m_ID = value;
            }
        }
        public string Login
        {
            get
            {
                return m_Login;
            }
            set
            {
                m_Login = value;
            }
        }
        public string Password
        {
            get
            {
                return m_Password;
            }
            set
            {
                m_Password = value;
            }
        }

        public override string ToString()
        {
            return $"ID: {m_ID}\nLogin: {m_Login}\nPassword: {m_Password}";
        }

    }
}
