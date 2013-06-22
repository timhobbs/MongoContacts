using AutoMapper;

namespace MongoContacts.App_Start {

    public class AutoMapperConfig {

        public static void RegisterMappings() {
            Mapper.CreateMap<MongoContacts.Domain.Contact, MongoContacts.Models.ContactModel>();
            Mapper.CreateMap<MongoContacts.Models.ContactModel, MongoContacts.Domain.Contact>();

            Mapper.CreateMap<MongoContacts.Domain.Email, MongoContacts.Models.EmailModel>();
            Mapper.CreateMap<MongoContacts.Models.EmailModel, MongoContacts.Domain.Email>();

            Mapper.CreateMap<MongoContacts.Domain.Group, MongoContacts.Models.GroupModel>();
            Mapper.CreateMap<MongoContacts.Models.GroupModel, MongoContacts.Domain.Group>();

            Mapper.CreateMap<MongoContacts.Domain.InstantMessenger, MongoContacts.Models.InstantMessengerModel>();
            Mapper.CreateMap<MongoContacts.Models.InstantMessengerModel, MongoContacts.Domain.InstantMessenger>();

            Mapper.CreateMap<MongoContacts.Domain.PhoneNumber, MongoContacts.Models.PhoneNumberModel>();
            Mapper.CreateMap<MongoContacts.Models.PhoneNumberModel, MongoContacts.Domain.PhoneNumber>();

            Mapper.CreateMap<MongoContacts.Domain.Website, MongoContacts.Models.WebsiteModel>();
            Mapper.CreateMap<MongoContacts.Models.WebsiteModel, MongoContacts.Domain.Website>();
        }
    }
}