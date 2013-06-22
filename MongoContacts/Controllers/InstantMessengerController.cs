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

    public class InstantMessengerController : Controller {
        private readonly ContactService contactService;
        private readonly InstantMessengerService instantMessengerService;

        public InstantMessengerController() {
            contactService = new ContactService();
            instantMessengerService = new InstantMessengerService();
        }

        public ActionResult Index(ObjectId id) {
            if (id == null) return RedirectToAction("Index", "Contact");

            var model = contactService.GetContact(id).ToModel();
            var instantMessengers = instantMessengerService.GetContactInstantMessengers(id);
            if (instantMessengers == null) instantMessengers = new List<InstantMessenger>();
            model.InstantMessengers = instantMessengers.Select(x => x.ToModel()).ToList();
            return View(model);
        }

        public ActionResult Create(string contactId) {
            var oid = ControllerHelpers.GetObjectId(contactId);
            var model = new InstantMessengerModel {
                ContactId = oid,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(InstantMessengerModel model) {
            if (ModelState.IsValid) {
                try {
                    instantMessengerService.AddInstantMessenger(model.ContactId, model.ToEntity());
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
            var model = instantMessengerService.GetContactInstantMessenger(cid, eid).ToModel();
            model.ContactId = cid;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(InstantMessengerModel model) {
            if (ModelState.IsValid) {
                try {
                    instantMessengerService.UpdateContactInstantMessenger(model.ContactId, model.ToEntity());
                    return RedirectToAction("Index", new { id = model.ContactId });
                } catch (Exception ex) {
                    ViewBag.ErrorMessage = ex.Message;
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(InstantMessengerModel model) {
            try {
                instantMessengerService.RemoveContactInstantMessenger(model.ContactId, model.Id);
                return RedirectToAction("Index", new { id = model.ContactId });
            } catch (Exception ex) {
                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
        }
    }
}