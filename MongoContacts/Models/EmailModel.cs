using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoContacts.Models {

    public class EmailModel : MongoListModel {

        [Required]
        public string Name { get; set; }

        [Required]
        public string EmailAddress { get; set; }
    }
}