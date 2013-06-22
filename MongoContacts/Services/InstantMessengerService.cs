using System.Collections.Generic;
using System.Linq;
using MongoContacts.DataAccess;
using MongoContacts.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace MongoContacts.Services {

    public class InstantMessengerService {
        private readonly MongoHelper<Contact> contacts;

        public InstantMessengerService() {
            contacts = new MongoHelper<Contact>();
        }

        public IList<InstantMessenger> GetContactInstantMessengers(ObjectId contactId) {
            var instantMessengers = contacts.Collection.AsQueryable<Contact>()
                .Where(c => c.Id == contactId)
                .FirstOrDefault().InstantMessengers;
            return instantMessengers;
        }

        public InstantMessenger GetContactInstantMessenger(ObjectId contactId, ObjectId instantMessengerId) {
            var instantMessengers = contacts.Collection.AsQueryable<Contact>()
                .Where(c => c.Id == contactId)
                .FirstOrDefault().InstantMessengers;
            var instantMessenger = instantMessengers.Where(e => e.Id == instantMessengerId).FirstOrDefault();

            return instantMessenger;
        }

        public void AddInstantMessenger(ObjectId contactId, InstantMessenger instantMessenger) {
            instantMessenger.Id = ObjectId.GenerateNewId();
            var query = Query<Contact>.EQ(c => c.Id, contactId);
            var update = Update<Contact>.Push(c => c.InstantMessengers, instantMessenger);
            var result = contacts.Collection.Update(query, update, UpdateFlags.None, WriteConcern.Acknowledged);
            if (result.Ok == false) {
                throw new MongoException("Unable to insert object");
            }
        }

        public void UpdateContactInstantMessenger(ObjectId contactId, InstantMessenger instantMessenger) {
            var query = Query<Contact>.EQ(c => c.Id, contactId);
            var contact = contacts.Collection.AsQueryable<Contact>().Where(c => c.Id == contactId).First();
            var originalInstantMessenger = contact.InstantMessengers.Where(w => w.Id == instantMessenger.Id).First();
            var index = contact.InstantMessengers.IndexOf(originalInstantMessenger);
            var update = Update<Contact>.Set(c => c.InstantMessengers[index], instantMessenger);
            var result = contacts.Collection.Update(query, update, UpdateFlags.None, WriteConcern.Acknowledged);
            if (result.DocumentsAffected == 0) {
                throw new MongoException("Unable to update object");
            }
        }

        public void RemoveContactInstantMessenger(ObjectId contactId, ObjectId instantMessengerId) {
            var query = Query<Contact>.EQ(c => c.Id, contactId);
            var contact = contacts.Collection.AsQueryable<Contact>().Where(c => c.Id == contactId).First();
            var instantMessenger = GetContactInstantMessenger(contactId, instantMessengerId);
            var index = contact.InstantMessengers.IndexOf(instantMessenger);
            var update = Update<Contact>.Pull(c => c.InstantMessengers, instantMessenger);
            var result = contacts.Collection.Update(query, update, UpdateFlags.None, WriteConcern.Acknowledged);
            if (result.DocumentsAffected == 0) {
                throw new MongoException("Unable to update object");
            }
        }
    }
}