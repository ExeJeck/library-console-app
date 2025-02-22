using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Library { 
    class ConnectToDB
    {
        string connection = "";
        public string GetConnection()
        {
            return connection;
        }
    }
}
