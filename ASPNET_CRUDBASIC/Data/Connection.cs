using System.Data.SqlClient;
namespace ASPNET_CRUDBASIC.Data
{
    public class Connection
    {
        private readonly string SQLConnection;
        public Connection() {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            SQLConnection = builder.GetSection("ConnectionStrings:SQLConnection").Value;
        }
       public string GetSqlconnection()
        {
            return SQLConnection;
        }
    }
}
