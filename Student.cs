using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRY_DB
{
    class Student
    {
        private int m_ID;
        private string m_Name;
        private string m_Group;

        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }

        }
        public string Group
        {
            get
            {
                return m_Group;
            }
            set
            {
                m_Group = value;
            }
        }
        
        public Student(string name, string group)
        {
            m_Name = name;
            m_Group = group;
        }

        //use this ctor only for menu
        public Student (int id, string name, string group)
        {
            m_ID = id;
            m_Name = name;
            m_Group = group;
        }

        override 
        public string ToString()
        {
            return $"ID: {m_ID}\nName: {m_Name}\nGroup: {m_Group}");
        }

    }
}
