using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using MySqlConnector;

namespace LMS.API.DataManagement
{
    public class DBMSConnection
    {
        public static IDbConnection GetConnection(IConfiguration config)
        {
            //return new SqlConnection(config.GetConnectionString("DefaultConnection"));
            return new MySqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        //public static IDbConnection GetSTETConnection(IConfiguration config)
        //{
        //    return new MySqlConnection(config.GetConnectionString("STETConnection"));
        //    //return new SqlConnection(config.GetConnectionString("DefaultConnection"));
        //}

        //public static IDbConnection GetStagingDBConnection(IConfiguration config)
        //{
        //    return new MySqlConnection(config.GetConnectionString("DefaultConnection"));
        //    //return new SqlConnection(config.GetConnectionString("DefaultStagingConnection"));
        //}

        public static IDbConnection GetConnection(object config)
        {
            throw new NotImplementedException();
        }
        public static IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}

