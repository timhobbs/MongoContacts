using System.Configuration;
using MongoDB.Driver;

namespace MongoContacts.DataAccess {

    public class MongoHelper<T> where T : class {

        public MongoCollection<T> Collection { get; private set; }

        public MongoHelper() {
            MongoServer server;
            MongoDatabase db;
            var connString = ConfigurationManager.AppSettings["MONGOLAB_URI"];

            if (connString != null) {
                var url = new MongoUrl(connString);
                var client = new MongoClient(url);
                server = client.GetServer();
                db = server.GetDatabase(url.DatabaseName);
            } else {
                connString = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
                var conn = new MongoConnectionStringBuilder(connString);
                server = MongoServer.Create(conn);
                db = server.GetDatabase(conn.DatabaseName);
            }

            Collection = db.GetCollection<T>(typeof(T).Name);
        }
    }
}