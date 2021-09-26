using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OMS.Models
{
    public class Connection
    {
        public SqlConnection con = new SqlConnection();

        public SqlConnection getConnection()
        {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["OMS"].ToString());
                con.Open();
                return con;
            
        }

        public void ExecuteQuery(string query)
        {
            getConnection();
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
        }
    }
}