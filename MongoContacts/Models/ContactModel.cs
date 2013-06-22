using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MongoContacts.Models {

    public class ContactModel : MongoModel {

        public ContactModel() {
            PhoneNumbers = new List<PhoneNumberModel>();
            EmailAddresses = new List<EmailModel>();
            InstantMessengers = new List<InstantMessengerModel>();
            Websites = new List<WebsiteModel>();
            Groups = new List<GroupModel>();
        }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public IList<PhoneNumberModel> PhoneNumbers { get; set; }

        public IList<EmailModel> EmailAddresses { get; set; }

        public IList<InstantMessengerModel> InstantMessengers { get; set; }

        public IList<WebsiteModel> Websites { get; set; }

        public IList<GroupModel> Groups { get; set; }

        public string Hometown { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Birthday { get; set; }

        [ScaffoldColumn(false)]
        public string FullName {
            get {
                return String.Format("{0} {1}", this.FirstName, this.LastName).Trim();
            }
        }
    }
}