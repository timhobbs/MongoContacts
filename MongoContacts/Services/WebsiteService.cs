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
    public class WebsiteService {

        private readonly MongoHelper<Contact> contacts;

        public WebsiteService() {
            contacts = new MongoHelper<Contact>();
        }

        public IList<Website> GetContactWebsites(ObjectId contactId) {
            var emails = contacts.Collection.AsQueryable<Contact>()
                .Where(c => c.Id == contactId)
                .FirstOrDefault().Websites;
            return emails;
        }

        public Website GetContactWebsite(ObjectId contactId, ObjectId websiteId) {
            var websites = contacts.Collection.AsQueryable<Contact>()
                .Where(c => c.Id == contactId)
                .FirstOrDefault().Websites;
            var website = websites.Where(w => w.Id == websiteId).FirstOrDefault();

            return website;
        }

        public void AddWebsite(ObjectId contactId, Website website) {
            website.Id = ObjectId.GenerateNewId();
            var query = Query<Contact>.EQ(c => c.Id, contactId);
            var update = Update<Contact>.Push(c => c.Websites, website);
            var result = contacts.Collection.Update(query, update, UpdateFlags.None, WriteConcern.Acknowledged);
            if (result.Ok == false) {
                throw new MongoException("Unable to insert object");
            }
        }

        public void UpdateContactWebsite(ObjectId contactId, Website website) {
            var query = Query<Contact>.EQ(c => c.Id, contactId);
            var contact = contacts.Collection.AsQueryable<Contact>().Where(c => c.Id == contactId).First();
            var originalWebsite = contact.Websites.Where(w => w.Id == website.Id).First();
            var index = contact.Websites.IndexOf(originalWebsite);
            var update = Update<Contact>.Set(c => c.Websites[index], website);
            var result = contacts.Collection.Update(query, update, UpdateFlags.None, WriteConcern.Acknowledged);
            if (result.DocumentsAffected == 0) {
                throw new MongoException("Unable to update object");
            }
        }
        public void RemoveContactWebsite(ObjectId contactId, ObjectId websiteId) {
            var query = Query<Contact>.EQ(c => c.Id, contactId);
            var contact = contacts.Collection.AsQueryable<Contact>().Where(c => c.Id == contactId).First();
            var email = GetContactWebsite(contactId, websiteId);
            var index = contact.Websites.IndexOf(email);
            var update = Update<Contact>.Pull(c => c.Websites, email);
            var result = contacts.Collection.Update(query, update, UpdateFlags.None, WriteConcern.Acknowledged);
            if (result.DocumentsAffected == 0) {
                throw new MongoException("Unable to update object");
            }
        }
    }
}