using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace MongoContacts.DataAccess {
    public class MongoHelper<T> where T : class {

        public MongoCollection<T> Collection { get; private set; }

        public MongoHelper() {
            var connString = ConfigurationManager.AppSettings["MONGOLAB_URI"] ??
                ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
            var conn = new MongoConnectionStringBuilder(connString);

            var server = MongoServer.Create(conn);
            var db = server.GetDatabase(conn.DatabaseName);
            Collection = db.GetCollection<T>(typeof(T).Name);
        }
    }
}