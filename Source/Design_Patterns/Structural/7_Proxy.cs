
using Demo.Common.Library;

namespace Design_Patterns.StructuralPatterns
{
    public class ProxyDemo : DemoBase
    {
        public override string Name => "Structure.Proxy";

        public override string ShortSummary => "provides an object that acts as a substitute for a real service object used by a client. A proxy receives client requests, does some work (access control, caching, etc.) and then passes the request to a service object.";

        public override Task Run()
        {
            IDatabase database = new DatabaseProxy("admin", "password");
            database.QueryData("SELECT * FROM Customers");

            return Task.CompletedTask;
        }
    }

    interface IDatabase
    {
        void QueryData(string query);
    }

    class RealDatabase : IDatabase
    {
        public void QueryData(string query) => Console.WriteLine($"Executing query: {query}");
    }

    class DatabaseProxy : IDatabase
    {
        private RealDatabase realDatabase;
        private readonly string username;
        private readonly string password;

        public DatabaseProxy(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        private bool Authenticate() => username == "admin" && password == "password";

        public void QueryData(string query)
        {
            if (!Authenticate())
            {
                Console.WriteLine("Authentication failed. Access denied.");
                return;
            }
            realDatabase ??= new RealDatabase();

            realDatabase.QueryData(query);
        }
    }
}
