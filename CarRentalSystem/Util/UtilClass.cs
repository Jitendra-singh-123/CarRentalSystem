using System.Configuration;
using System.Data.SqlClient;

namespace CarRentalSystem.Util
{
    static class UtilClass
    {
        static SqlConnection connection = null;
        //public static SqlConnection GetConnection(string connectionString)
        //{
        //    connection.ConnectionString = connectionString;
        //    return connection;
        //}
        //(or)
        public static SqlConnection GetConnection()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;

            // 2. Establish connection
            connection = new SqlConnection(ConnectionString);
            // connection.ConnectionString = @"DataSource=(localdb)\MSSQLLocalDB;Initial Catalog=Hexaware_Dec_23;Integrated Security=True;";
            return connection;
        }
    }
}
