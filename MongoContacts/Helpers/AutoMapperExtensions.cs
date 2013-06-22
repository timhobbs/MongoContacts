using AutoMapper;
using MongoContacts.Domain;
using MongoContacts.Models;

namespace MongoContacts.Helpers {

    public static class AutoMapperExtensions {

        public static ContactModel ToModel(this Contact contact) {
            return Mapper.Map<Contact, ContactModel>(contact);
        }

        public static Contact ToEntity(this ContactModel contactModel) {
            return Mapper.Map<ContactModel, Contact>(contactModel);
        }

        public static EmailModel ToModel(this Email contact) {
            return Mapper.Map<Email, EmailModel>(contact);
        }

        public static Email ToEntity(this EmailModel contactModel) {
            return Mapper.Map<EmailModel, Email>(contactModel);
        }

        public static GroupModel ToModel(this Group contact) {
            return Mapper.Map<Group, GroupModel>(contact);
        }

        public static Group ToEntity(this GroupModel contactModel) {
            return Mapper.Map<GroupModel, Group>(contactModel);
        }

        public static InstantMessengerModel ToModel(this InstantMessenger contact) {
            return Mapper.Map<InstantMessenger, InstantMessengerModel>(contact);
        }

        public static InstantMessenger ToEntity(this InstantMessengerModel contactModel) {
            return Mapper.Map<InstantMessengerModel, InstantMessenger>(contactModel);
        }

        public static PhoneNumberModel ToModel(this PhoneNumber contact) {
            return Mapper.Map<PhoneNumber, PhoneNumberModel>(contact);
        }

        public static PhoneNumber ToEntity(this PhoneNumberModel contactModel) {
            return Mapper.Map<PhoneNumberModel, PhoneNumber>(contactModel);
        }

        public static WebsiteModel ToModel(this Website contact) {
            return Mapper.Map<Website, WebsiteModel>(contact);
        }

        public static Website ToEntity(this WebsiteModel contactModel) {
            return Mapper.Map<WebsiteModel, Website>(contactModel);
        }
    }
}