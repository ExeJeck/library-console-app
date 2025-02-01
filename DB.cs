using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Library { 
    class ConnectToDB
    {
        string connection = "Server=db4free.net;port=3306;database=mylibraryqwert;username=romanb;password=_ir038hhFHpoifns";
        public string GetConnection()
        {
            return connection;
        }
    }
}
