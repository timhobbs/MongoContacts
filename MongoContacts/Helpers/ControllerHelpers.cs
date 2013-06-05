using MongoDB.Bson;

namespace MongoContacts.Helpers {

    public class ControllerHelpers {

        public static ObjectId GetObjectId(string id) {
            return new ObjectId(id);
        }
    }
}