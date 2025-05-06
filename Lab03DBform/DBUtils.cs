using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Lab03DBform
{
    public class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string connection = "server=localhost;user=root;database=carordersdb;password=Torigarty1503";
            MySqlConnection conn = new MySqlConnection(connection);
            return conn;
        }
    }
}
