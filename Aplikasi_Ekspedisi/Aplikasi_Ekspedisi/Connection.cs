using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;


namespace Aplikasi_Ekspedisi
{
    public class Connection
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;initial catalog=kap;username=root;password=v1nagab1");
        public MySqlConnection ActiveCon()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;

        }
    }
}
