using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoContacts.Domain {

    public class Contact : MongoEntity {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public IList<PhoneNumber> PhoneNumbers { get; set; }

        public IList<Email> EmailAddresses { get; set; }

        public IList<InstantMessenger> InstantMessengers { get; set; }

        public IList<Website> Websites { get; set; }

        public IList<Group> Groups { get; set; }

        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime? Birthday { get; set; }

        public string Hometown { get; set; }
    }
}