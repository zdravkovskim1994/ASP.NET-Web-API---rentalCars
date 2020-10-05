using System.Configuration;
using System.Data.SqlClient;

namespace WebCarRental.Core.DbHelper
{
    public class DbAccess
    {
        public static SqlConnection GetConnection()
        {
            string connetionString = ConfigurationManager.ConnectionStrings["CarRentalDB"].ConnectionString;

            SqlConnection conn = new SqlConnection(connetionString);

            conn.Open();

            return conn;
        }
    }
}
