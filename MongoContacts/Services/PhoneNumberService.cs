using System.Collections.Generic;
using System.Linq;
using MongoContacts.DataAccess;
using MongoContacts.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace MongoContacts.Services {

    public class PhoneNumberService {
        private readonly MongoHelper<Contact> contacts;

        public PhoneNumberService() {
            contacts = new MongoHelper<Contact>();
        }

        public IList<PhoneNumber> GetContactPhoneNumbers(ObjectId contactId) {
            var phoneNumbers = contacts.Collection.AsQueryable<Contact>()
                .Where(c => c.Id == contactId)
                .FirstOrDefault().PhoneNumbers;
            return phoneNumbers;
        }

        public PhoneNumber GetContactPhoneNumber(ObjectId contactId, ObjectId phoneNumberId) {
            var phoneNumbers = contacts.Collection.AsQueryable<Contact>()
                .Where(c => c.Id == contactId)
                .FirstOrDefault().PhoneNumbers;
            var phoneNumber = phoneNumbers.Where(e => e.Id == phoneNumberId).FirstOrDefault();

            return phoneNumber;
        }

        public void AddPhoneNumber(ObjectId contactId, PhoneNumber phoneNumber) {
            phoneNumber.Id = ObjectId.GenerateNewId();
            var query = Query<Contact>.EQ(c => c.Id, contactId);
            var update = Update<Contact>.Push(c => c.PhoneNumbers, phoneNumber);
            var result = contacts.Collection.Update(query, update, UpdateFlags.None, WriteConcern.Acknowledged);
            if (result.Ok == false) {
                throw new MongoException("Unable to insert object");
            }
        }

        public void UpdateContactPhoneNumber(ObjectId contactId, PhoneNumber phoneNumber) {
            var query = Query<Contact>.EQ(c => c.Id, contactId);
            var contact = contacts.Collection.AsQueryable<Contact>().Where(c => c.Id == contactId).First();
            var originalPhoneNumber = contact.PhoneNumbers.Where(w => w.Id == phoneNumber.Id).First();
            var index = contact.PhoneNumbers.IndexOf(originalPhoneNumber);
            var update = Update<Contact>.Set(c => c.PhoneNumbers[index], phoneNumber);
            var result = contacts.Collection.Update(query, update, UpdateFlags.None, WriteConcern.Acknowledged);
            if (result.DocumentsAffected == 0) {
                throw new MongoException("Unable to update object");
            }
        }

        public void RemoveContactPhoneNumber(ObjectId contactId, ObjectId phoneNumberId) {
            var query = Query<Contact>.EQ(c => c.Id, contactId);
            var contact = contacts.Collection.AsQueryable<Contact>().Where(c => c.Id == contactId).First();
            var phoneNumber = GetContactPhoneNumber(contactId, phoneNumberId);
            var index = contact.PhoneNumbers.IndexOf(phoneNumber);
            var update = Update<Contact>.Pull(c => c.PhoneNumbers, phoneNumber);
            var result = contacts.Collection.Update(query, update, UpdateFlags.None, WriteConcern.Acknowledged);
            if (result.DocumentsAffected == 0) {
                throw new MongoException("Unable to update object");
            }
        }
    }
}