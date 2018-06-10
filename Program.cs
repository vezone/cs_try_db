using System;

using MySql.Data.MySqlClient;
using System.Collections.Generic;

using static System.Console;

namespace TRY_DB
{
    class Program
    {
        private DBConnect m_Connector;
        private Student m_Student;
        private Student[] m_StudentArray;
        private Account[] m_AccountArray;
        static void Main(string[] args)
        {
            //it's like that only for now
            new Program().StudentsListMenu();
        }

        private static void WL(string str, int color)
        {
            if (color >= 0 && color <= 15)
                ForegroundColor = (ConsoleColor)color;
            WriteLine(str);
            ResetColor();
        }

        private static void RL(string str)
        {
            WriteLine(str);
            ReadLine();
        }

        private static void RL(string str, int color)
        {
            if (color >= 0 && color <= 15)
                ForegroundColor = (ConsoleColor)color;
            Write(str);
            ReadLine();
            ResetColor();
        }

        private static void RL(string str, out string value)
        {
            Write(str);
            value = ReadLine();
        }

        private static void RL(string str, out int value, int color)
        {
            if (color >= 0 && color <= 15)
                ForegroundColor = (ConsoleColor)color;
            Write(str);
            value = Int32.Parse(ReadLine());
            ResetColor();
        }

        private static void RL(string str, out string value, int color)
        {
            if (color >= 0 && color <= 15)
                ForegroundColor = (ConsoleColor)color;
            Write(str);
            value = ReadLine();
            ResetColor();
        }

        private void UpdateStudentArray(int val = 0)
        {
            List<string>[] allInfo = m_Connector.GetAllInformation();
            m_StudentArray = new Student[allInfo[0].Count];
            string[] idArray = allInfo[0].ToArray(),
                     nameArray = allInfo[1].ToArray(),
                     groupArray = allInfo[2].ToArray();

            WL("Student list\n", 10);
            for (int i = 0; i < idArray.Length; i++)
            {
                m_StudentArray[i] =
                    new Student(Int32.Parse(idArray[i]), nameArray[i], groupArray[i]);
                WriteLine(m_StudentArray[i] + "\n");
            }
            if (val != 0)
                RL("Press enter", 10);
        }

        private bool IsThereSeparation(out int id)
        {
            Student First = null, Second = null;
            for (int i = 1; i < m_StudentArray.Length-1; i++)
            {
                First = m_StudentArray[i];
                Second = m_StudentArray[i+1];

                if ((Second.ID - First.ID) > 1)
                {
                    id = First.ID + 1;
                    return true;
                } 
            }

            id = 0;
            return false;
        }

        private void StartMenu()
        {
            int choice;
            m_Connector = new DBConnect();
            RL("1.Login\n2.Registration", out choice, 10);
            switch (choice)
            {
                case 1:
                    LoginMenu();
                    break;
                case 2:
                    RegistrationMenu();
                    break;
                default:
                    WL("Default", 10);
                    break;
            }

        }

        private void LoginMenu()
        {
            WL("Login menu", 10);
            string login, password;
            RL("Write login:", out login, 10);
            RL("Write password: ", out password, 10);
            if (Login(login, password))
            {
                AppMenu();
            }
            else
            {
                WL("Error: wrong login or password!", 10);
            }
        }

        private bool Login(string login, string password)
        {
            
            return true;
        }

        private void AppMenu()
        {
            //
            StudentsListMenu();
        }

        private void RegistrationMenu()
        {
            WL("Registration menu", 10);
        }

        private void StudentsListMenu()
        {
            bool run = true;
            
            while (run)
            {
                Clear();
                WL("Menu", 10);

                int choice;
                RL("1.Show\n2.Add student\n3.Delete student\n0.Exit", out choice, 6);

                switch (choice)
                {
                    case 0:
                        run = false;
                        break;
                    case 1:
                        {
                            Clear();
                            UpdateStudentArray(1);
                        }
                        break;
                    case 2:
                        {
                            Clear();
                            UpdateStudentArray();
                            string name, group;
                            RL("Write student name: ", out name);
                            if (name.Length == 0 || name == "d") break;
                            RL("Write student group: ", out group);
                            int id;
                            if (IsThereSeparation(out id))
                            {
                                m_Student = new Student(id, name, group);
                                m_Connector.RunQuery(DBConnect.InsertIDInStudentsList(m_Student));
                            }
                            else
                            {
                                m_Student = new Student(name, group);
                                m_Connector.RunQuery(DBConnect.InsertInStudentsList(m_Student));
                            }
                        }
                        break;
                    case 3:
                        {
                            Clear();
                            UpdateStudentArray();
                            string id;
                            RL("Write id to delete student: ", out id, 10);
                            if (id.Length == 0 || id == "d") break;
                            m_Connector.RunQuery(DBConnect.DeleteFromStudentsList(id));
                            UpdateStudentArray();
                        }
                        break;

                    default:
                        Clear();
                        WL("Default", 10);
                        RL("Press enter to return.", 10);
                        break;

                }
            }
        }

    }
}
