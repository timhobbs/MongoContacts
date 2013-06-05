using System;
using System.Collections.Generic;
using System.Linq;
using MongoContacts.DataAccess;
using MongoContacts.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace MongoContacts.Services {

    public class ContactService {
        private readonly MongoHelper<Contact> contacts;

        public ContactService() {
            contacts = new MongoHelper<Contact>();
        }

        public IList<Contact> GetAllContacts() {
            return contacts.Collection.AsQueryable<Contact>().ToList();
        }

        public Contact GetContact(ObjectId id) {
            var contact = contacts.Collection.AsQueryable<Contact>()
                .Where(c => c.Id == id).FirstOrDefault();
            return contact;
        }

        public IList<Contact> FilterContacts(string filter) {
            filter = filter.ToLowerInvariant();
            var filtered = contacts.Collection.AsQueryable<Contact>()
                .Where(c => c.FirstName.ToLowerInvariant().Contains(filter) ||
                            c.LastName.ToLowerInvariant().Contains(filter) ||
                            c.EmailAddresses
                                .Any(e => e.EmailAddress.ToLowerInvariant().Contains(filter))
                      ).ToList();
            return filtered;
        }

        public void CreateContact(Contact contact) {
            var result = contacts.Collection.Save(contact, WriteConcern.Acknowledged);
            if (result.Ok == false) {
                throw new MongoException("Unable to insert object");
            }
        }

        public void UpdateContact(Contact contact) {
            var query = Query<Contact>.EQ(c => c.Id, contact.Id);
            var update = Update<Contact>.Set(c => c.FirstName, contact.FirstName)
                .Set(c => c.LastName, contact.LastName)
                .Set(c => c.ImageUrl, contact.ImageUrl)
                .Set(c => c.Birthday, contact.Birthday)
                .Set(c => c.Hometown, contact.Hometown);
            var result = contacts.Collection.Update(query,
                update, WriteConcern.Acknowledged);
            if (result.DocumentsAffected == 0) {
                throw new MongoException("Unable to update object");
            }
        }

        public void DeleteContact(ObjectId id) {
            var query = Query<Contact>.EQ(c => c.Id, id);
            var result = contacts.Collection.Remove(query, RemoveFlags.Single, WriteConcern.Acknowledged);
            if (result.DocumentsAffected != 1) {
                throw new MongoException("Error removing object");
            }
        }
    }
}