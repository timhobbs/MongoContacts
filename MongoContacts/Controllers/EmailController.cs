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

    public class EmailController : Controller {
        private readonly ContactService contactService;
        private readonly EmailService emailService;

        public EmailController() {
            contactService = new ContactService();
            emailService = new EmailService();
        }

        public ActionResult Index(ObjectId id) {
            if (id == null) return RedirectToAction("Index", "Contact");

            var model = contactService.GetContact(id).ToModel();
            var emails = emailService.GetContactEmails(id);
            if (emails == null) emails = new List<Email>();
            model.EmailAddresses = emails.Select(x => x.ToModel()).ToList();
            return View(model);
        }

        public ActionResult Create(string contactId) {
            var oid = ControllerHelpers.GetObjectId(contactId);
            var model = new EmailModel {
                ContactId = oid,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EmailModel model) {
            if (ModelState.IsValid) {
                try {
                    emailService.AddEmail(model.ContactId, model.ToEntity());
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
            var model = emailService.GetContactEmail(cid, eid).ToModel();
            model.ContactId = cid;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EmailModel model) {
            if (ModelState.IsValid) {
                try {
                    emailService.UpdateContactEmail(model.ContactId, model.ToEntity());
                    return RedirectToAction("Index", new { id = model.ContactId });
                } catch (Exception ex) {
                    ViewBag.ErrorMessage = ex.Message;
                    return View(model);

                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(EmailModel model) {
            try {
                emailService.RemoveContactEmail(model.ContactId, model.Id);
                return RedirectToAction("Index", new { id = model.ContactId });
            } catch (Exception ex) {
                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
        }
    }
}