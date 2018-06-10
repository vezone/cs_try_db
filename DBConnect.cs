using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TRY_DB
{
    //C:\ProgramData\MySQL\MySQL Server 5.7\Data\students
    class DBConnect
    {
        private MySqlConnection m_Connection;
        private string m_Server;
        private string m_DataBase;
        private string m_UID;
        private string m_Password;

        public DBConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_Server = "localhost";
            m_DataBase = "students";
            m_UID = "root";
            m_Password = "root";

            string ConnectionString = 
                "SERVER="   + m_Server   + ";" + 
                "DATABASE=" + m_DataBase + ";" + 
                "UID="      + m_UID      + ";" +
                "PASSWORD=" + m_Password + ";";
            m_Connection = new MySqlConnection(ConnectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                m_Connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, " +
                            "please try again.");
                        break;
                }
                return false;
            }

        }

        private bool CloseConnection()
        {
            try
            {
                m_Connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        
        public void RunQuery(string query)
        {
            if (OpenConnection())
            {
                new MySqlCommand(query, m_Connection).ExecuteNonQuery();

                CloseConnection();
            }
        }

        public static string InsertInStudentsList(Student student)
        {
            string query = 
                "INSERT INTO `students`.`students_list` (`Name`, `Group`)" +
                $"VALUES('{student.Name}', '{student.Group}');";
            return query;
        }

        public static string InsertIDInStudentsList(Student student)
        {
            string query =
                "INSERT INTO `students`.`students_list` (`ID`, `Name`, `Group`)" +
                $"VALUES('{student.ID}', '{student.Name}', '{student.Group}');";
            return query;
        }

        public static string DeleteFromStudentsList(string id)
        {
            return $"DELETE FROM `students`.`students_list` WHERE `ID`='{id}';";
        }

        public static string InsertInAccounts(Student student, string login, string password)
        {
            return "INSERT INTO `students`.`accounts` (`ID`, `Login`, `Password`)" +
                   $"VALUES('{student.ID}', '{login}', '{password}');";
        }

        public static string DeleteFromAccounts(string id)
        {
            return $"DELETE FROM `students`.`accounts` WHERE `ID`='{id}';";
        }

        //this not final version
        public List<string>[] GetAllInformation()
        {
            string query = "select * from `students`.`students_list`";
            List<string>[] all = new List<string>[3];
            all[0] = new List<string>();
            all[1] = new List<string>();
            all[2] = new List<string>();

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command &
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = new MySqlCommand(query, m_Connection).ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    all[0].Add(dataReader["ID"] + "");
                    all[1].Add(dataReader["Name"] + "");
                    all[2].Add(dataReader["Group"] + "");
                }

                dataReader.Close();
                CloseConnection();

                //return list to be displayed
                return all;
            }
            else
            {
                return all;
            }
        }

    }
}
