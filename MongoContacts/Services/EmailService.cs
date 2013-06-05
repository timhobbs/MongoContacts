using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoContacts.DataAccess;
using MongoContacts.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace MongoContacts.Services {
    public class EmailService {

        private readonly MongoHelper<Contact> contacts;

        public EmailService() {
            contacts = new MongoHelper<Contact>();
        }

        public IList<Email> GetContactEmails(ObjectId contactId) {
            var emails = contacts.Collection.AsQueryable<Contact>()
                .Where(c => c.Id == contactId)
                .FirstOrDefault().EmailAddresses;
            return emails;
        }

        public Email GetContactEmail(ObjectId contactId, ObjectId emailId) {
            var emails = contacts.Collection.AsQueryable<Contact>()
                .Where(c => c.Id == contactId)
                .FirstOrDefault().EmailAddresses;
            var email = emails.Where(e => e.Id == emailId).FirstOrDefault();

            return email;
        }

        public void AddEmail(ObjectId contactId, Email email) {
            email.Id = ObjectId.GenerateNewId();
            var query = Query<Contact>.EQ(c => c.Id, contactId);
            var update = Update<Contact>.Push(c => c.EmailAddresses, email);
            var result = contacts.Collection.Update(query, update, UpdateFlags.None, WriteConcern.Acknowledged);
            if (result.Ok == false) {
                throw new MongoException("Unable to insert object");
            }
        }

        public void UpdateContactEmail(ObjectId contactId, Email email) {
            var query = Query<Contact>.EQ(c => c.Id, contactId);
            var contact = contacts.Collection.AsQueryable<Contact>().Where(c => c.Id == contactId).First();
            var originalEmail = contact.EmailAddresses.Where(w => w.Id == email.Id).First();
            var index = contact.EmailAddresses.IndexOf(originalEmail);
            var update = Update<Contact>.Set(c => c.EmailAddresses[index], email);
            var result = contacts.Collection.Update(query, update, UpdateFlags.None, WriteConcern.Acknowledged);
            if (result.DocumentsAffected == 0) {
                throw new MongoException("Unable to update object");
            }
        }
        public void RemoveContactEmail(ObjectId contactId, ObjectId emailId) {
            var query = Query<Contact>.EQ(c => c.Id, contactId);
            var contact = contacts.Collection.AsQueryable<Contact>().Where(c => c.Id == contactId).First();
            var email = GetContactEmail(contactId, emailId);
            var index = contact.EmailAddresses.IndexOf(email);
            var update = Update<Contact>.Pull(c => c.EmailAddresses, email);
            var result = contacts.Collection.Update(query, update, UpdateFlags.None, WriteConcern.Acknowledged);
            if (result.DocumentsAffected == 0) {
                throw new MongoException("Unable to update object");
            }
        }
    }
}