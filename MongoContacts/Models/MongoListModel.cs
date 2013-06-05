using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoContacts.Models {

    public class MongoListModel : MongoModel {

        [UIHint("ObjectId")]
        [HiddenInput]
        [Required]
        public ObjectId ContactId { get; set; }
    }
}