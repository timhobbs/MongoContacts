using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MongoContacts.Domain;
using MongoContacts.Helpers;
using MongoContacts.Models;
using MongoContacts.Services;
using MongoDB.Bson;

namespace MongoContacts.Controllers {

    public class PhoneNumberController : Controller {
        private readonly ContactService contactService;
        private readonly PhoneNumberService phoneNumberService;

        public PhoneNumberController() {
            contactService = new ContactService();
            phoneNumberService = new PhoneNumberService();
        }

        public ActionResult Index(ObjectId id) {
            if (id == null) return RedirectToAction("Index", "Contact");

            var model = contactService.GetContact(id).ToModel();
            var phoneNumbers = phoneNumberService.GetContactPhoneNumbers(id);
            if (phoneNumbers == null) phoneNumbers = new List<PhoneNumber>();
            model.PhoneNumbers = phoneNumbers.Select(x => x.ToModel()).ToList();
            return View(model);
        }

        public ActionResult Create(string contactId) {
            var oid = ControllerHelpers.GetObjectId(contactId);
            var model = new PhoneNumberModel {
                ContactId = oid,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PhoneNumberModel model) {
            if (ModelState.IsValid) {
                try {
                    phoneNumberService.AddPhoneNumber(model.ContactId, model.ToEntity());
                    return RedirectToAction("Index", new { id = model.ContactId });
                } catch (Exception ex) {
                    ViewBag.ErrorMessage = ex.Message;
                    return View(model);
                }
            }

            return View(model);
        }

        public ActionResult Edit(string id, string contactId) {
            var cid = ControllerHelpers.GetObjectId(contactId);
            var eid = ControllerHelpers.GetObjectId(id);
            var model = phoneNumberService.GetContactPhoneNumber(cid, eid).ToModel();
            model.ContactId = cid;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PhoneNumberModel model) {
            if (ModelState.IsValid) {
                try {
                    phoneNumberService.UpdateContactPhoneNumber(model.ContactId, model.ToEntity());
                    return RedirectToAction("Index", new { id = model.ContactId });
                } catch (Exception ex) {
                    ViewBag.ErrorMessage = ex.Message;
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(PhoneNumberModel model) {
            try {
                phoneNumberService.RemoveContactPhoneNumber(model.ContactId, model.Id);
                return RedirectToAction("Index", new { id = model.ContactId });
            } catch (Exception ex) {
                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
        }
    }
}