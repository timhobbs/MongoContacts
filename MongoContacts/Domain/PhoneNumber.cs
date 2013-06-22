namespace MongoContacts.Domain {

    public class PhoneNumber : MongoEntity {

        public string Name { get; set; }

        public string Number { get; set; }
    }
}