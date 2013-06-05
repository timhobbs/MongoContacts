namespace MongoContacts.Domain {

    public class Phone : MongoEntity {

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}