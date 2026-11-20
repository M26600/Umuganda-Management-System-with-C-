using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmugandaManagementSystem
{
    public class DatabaseConnection
    {
        private string connectionString =
    "Server=127.0.0.1;Port=3306;Database=umuganda_management_system;Uid=root;Pwd=;";
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
