using System.Configuration;
using System.Data.SqlClient;


namespace MusicRadio.Data
{
    public static class DbConnectionHelper
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MusicDbConnection"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}