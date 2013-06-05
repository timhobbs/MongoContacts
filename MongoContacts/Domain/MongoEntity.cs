using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
namespace MongoContacts.Domain {

    public class MongoEntity {

        [BsonId]
        public MongoDB.Bson.ObjectId Id { get; set; }
    }
}