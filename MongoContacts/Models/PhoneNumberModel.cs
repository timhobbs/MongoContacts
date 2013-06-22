using System.ComponentModel.DataAnnotations;

namespace MongoContacts.Models {

    public class PhoneNumberModel : MongoListModel {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }
    }
}