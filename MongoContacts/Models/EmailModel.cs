using System.ComponentModel.DataAnnotations;

namespace MongoContacts.Models {

    public class EmailModel : MongoListModel {

        [Required]
        public string Name { get; set; }

        [Required]
        public string EmailAddress { get; set; }
    }
}