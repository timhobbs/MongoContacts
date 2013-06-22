using System.ComponentModel.DataAnnotations;

namespace MongoContacts.Models {

    public class GroupModel {

        [Required]
        public string Name { get; set; }
    }
}