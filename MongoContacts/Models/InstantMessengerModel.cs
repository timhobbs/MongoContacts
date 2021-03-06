﻿using System.ComponentModel.DataAnnotations;

namespace MongoContacts.Models {

    public class InstantMessengerModel : MongoListModel {

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImAccount { get; set; }
    }
}