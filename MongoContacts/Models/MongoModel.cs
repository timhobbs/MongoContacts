using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MongoContacts.Models {

    public class MongoModel {

        [UIHint("ObjectId")]
        [HiddenInput]
        public MongoDB.Bson.ObjectId Id { get; set; }
    }
}