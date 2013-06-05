using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
namespace MongoContacts.Domain {

    public class Email : MongoEntity {

        public string Name { get; set; }

        public string EmailAddress { get; set; }
    }
}