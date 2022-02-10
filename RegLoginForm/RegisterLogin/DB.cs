using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace RegisterLogin
{
    class DB
    {
        static string _connectionStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\SYN\Desktop\RegisterLogin\RegisterLogin\Database1.mdf;Integrated Security=True";
        
        private SqlConnection connection = new SqlConnection(_connectionStr);
        //create a fuct to open the connection
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        //create a fuct to close the connection
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        //create a func to return the connection
        public SqlConnection getConnection()
        {
            return connection;
        }
    }
}
