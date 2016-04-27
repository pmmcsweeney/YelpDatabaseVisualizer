using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public class MySQL_Connection
    {
        private MySqlConnection connection;
        public MySQL_Connection()
        {
            try {
                Initialize();
            }
            catch(MySqlException ex)
            {

            }
        }

        private void Initialize()
        {
            string server;
            string database;
            string uid;
            string password;
            server = "localhost";
            database = "projectdb";
            uid = "root";
            password = "patrick1";
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);

        }
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch(MySqlException ex)
            {
                if (ex.Number == 0)
                    return false;
                else if (ex.Number == 1045)
                    return false; //incorrect usr/pass
            }
            return false;
        }
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                //handle exception
            }
            return false;
        }

        public List<String> SQLSELECTExec(string querySTR, string column_name)
        {
            List<String> qResult = new List<String>();
            if(this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(querySTR, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while( dataReader.Read())
                {
                    if (!qResult.Contains(dataReader.GetString(column_name)))
                        qResult.Add(dataReader.GetString(column_name));
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return qResult;
        }
    }
}
