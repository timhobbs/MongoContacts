using System.Web.Mvc;
using MongoContacts.Helpers;

namespace MongoContacts.App_Start {

    public class ModelBinderConfig {

        public static void RegisterModelBinders() {
            ModelBinders.Binders.Add(typeof(MongoDB.Bson.ObjectId), new ObjectIdBinder());
        }
    }
}