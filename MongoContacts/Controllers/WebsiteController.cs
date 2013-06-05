using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using MongoContacts.Domain;
using MongoContacts.Helpers;
using MongoContacts.Models;
using MongoContacts.Services;
using MongoDB.Bson;

namespace MongoContacts.Controllers {

    public class WebsiteController : Controller {
        private readonly ContactService contactService;
        private readonly WebsiteService websiteService;

        public WebsiteController() {
            contactService = new ContactService();
            websiteService = new WebsiteService();
        }

        public ActionResult Index(ObjectId id) {
            if (id == null) return RedirectToAction("Index", "Contact");

            var model = contactService.GetContact(id).ToModel();
            var websites = websiteService.GetContactWebsites(id);
            if (websites == null) websites = new List<Website>();
            model.Websites = websites.Select(x => x.ToModel()).ToList();
            return View(model);
        }

        public ActionResult Create(string contactId) {
            var oid = ControllerHelpers.GetObjectId(contactId);
            var model = new WebsiteModel {
                ContactId = oid,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(WebsiteModel model) {
            if (ModelState.IsValid) {
                try {
                    if (!Regex.Match(model.Url.ToLowerInvariant(), "^http[s]?://").Success) {
                        model.Url = String.Format("http://{0}", model.Url);
                    }

                    websiteService.AddWebsite(model.ContactId, model.ToEntity());
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
            var wid = ControllerHelpers.GetObjectId(id);
            var model = websiteService.GetContactWebsite(cid, wid).ToModel();
            model.ContactId = cid;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(WebsiteModel model) {
            if (ModelState.IsValid) {
                try {
                    websiteService.UpdateContactWebsite(model.ContactId, model.ToEntity());
                    return RedirectToAction("Index", new { id = model.ContactId });
                } catch (Exception ex) {
                    ViewBag.ErrorMessage = ex.Message;
                    return View(model);

                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(WebsiteModel model) {
            try {
                websiteService.RemoveContactWebsite(model.ContactId, model.Id);
                return RedirectToAction("Index", new { id = model.ContactId });
            } catch (Exception ex) {
                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
        }
    }
}