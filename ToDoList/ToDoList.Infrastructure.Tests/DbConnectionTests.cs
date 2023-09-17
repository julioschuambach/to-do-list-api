using Microsoft.Extensions.DependencyInjection;
using ToDoList.Infrastructure.Data;
using ToDoList.Infrastructure.Interfaces.Data;

namespace ToDoList.Infrastructure.Tests
{
    [TestClass]
    public class DbConnectionTests
    {
        private readonly IDbConnection _connection;
        private readonly string _connectionString;

        public DbConnectionTests()
        {
            var service = new ServiceCollection();
            service.AddScoped<IDbConnection, DbConnection>();

            var provider = service.BuildServiceProvider();
            _connection = provider.GetRequiredService<IDbConnection>();

            _connectionString = "Server = localhost, 1433; Database = ToDoListDatabase; User ID = sa; Password = 1q2w3e4r@#$; Trusted_Connection = False; TrustServerCertificate = True;";
        }

        [TestMethod]
        public void ConnectionTest()
        {
            using (var connection = _connection.GetConnection())
            {
                try
                {
                    connection.ConnectionString = _connectionString;

                    connection.Open();
                    Assert.IsTrue(connection.State == System.Data.ConnectionState.Open,
                        "Failed to open the connection to database.");

                    connection.Close();
                    Assert.IsTrue(connection.State == System.Data.ConnectionState.Closed,
                        "Failed to close the connection to database.");
                }
                catch (Exception ex)
                {
                    Assert.Fail("Failed to connect to the database." + Environment.NewLine +
                        ex.Message);
                }
                finally
                {
                    connection.Dispose();
                }
            }
        }
    }
}