using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace DBL
{
    public abstract class DB
    {

        private const string MySqlConnSTR = @"server=localhost;
                                    user id=root;
                                    password=josh17rog;
                                    persistsecurityinfo=True;
                                    database=project12";

        protected DbConnection conn;
        protected DbCommand cmd;
        protected DbDataReader reader;

        protected DB()
        {
            if (conn == null)
            {
                conn = new MySqlConnection(MySqlConnSTR);
            }
            cmd = new MySqlCommand();
            cmd.Connection = conn;
            reader = null;
        }
    }
}
