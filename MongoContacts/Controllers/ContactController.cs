using System;
using System.Linq;
using System.Web.Mvc;
using MongoContacts.Helpers;
using MongoContacts.Models;
using MongoContacts.Services;

namespace MongoContacts.Controllers {

    public class ContactController : Controller {
        private readonly ContactService contactService;
        private readonly EmailService emailService;

        public ContactController() {
            contactService = new ContactService();
            emailService = new EmailService();
        }

        public ActionResult Index() {
            ViewBag.FilterMode = Request.QueryString["filter"] != null;

            var model = contactService.GetAllContacts().Select(x => x.ToModel()).ToList();
            return View(model);
        }

        public ActionResult Details(string id) {
            var oid = ControllerHelpers.GetObjectId(id);
            var model = contactService.GetContact(oid).ToModel();
            return View(model);
        }

        public ActionResult Create() {
            var model = new ContactModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ContactModel model) {
            if (ModelState.IsValid) {
                try {
                    CheckImageUrl(model);
                    contactService.CreateContact(model.ToEntity());
                    return RedirectToAction("Index");
                } catch (Exception ex) {
                    ViewBag.ErrorMessage = ex.Message;
                    return View(model);
                }
            }

            return View(model);
        }

        public ActionResult Edit(string id) {
            var oid = ControllerHelpers.GetObjectId(id);
            var model = contactService.GetContact(oid).ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ContactModel model) {
            if (ModelState.IsValid) {
                try {
                    CheckImageUrl(model);
                    contactService.UpdateContact(model.ToEntity());
                    return RedirectToAction("Index");
                } catch (Exception ex) {
                    ViewBag.ErrorMessage = ex.Message;
                    return View(model);
                }
            }

            return View(model);
        }

        public ActionResult Delete(string id) {
            var oid = ControllerHelpers.GetObjectId(id);
            var model = contactService.GetContact(oid).ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(ContactModel model) {
            try {
                contactService.DeleteContact(model.Id);
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        public ActionResult Search(string q) {
            var model = contactService.FilterContacts(q).Select(x => x.ToModel()).ToList();
            return View("Index", model);
        }

        /* Private methods
         * -------------------------------------------------*/

        private void CheckImageUrl(ContactModel model) {
            if (String.IsNullOrEmpty(model.ImageUrl)) {
                model.ImageUrl = GetUnknownImageUrl();
            }
        }

        private string GetUnknownImageUrl() {
            var domain = String.Format("{0}{1}{2}{3}", Request.Url.Scheme,
                                        System.Uri.SchemeDelimiter,
                                        Request.Url.Host,
                                        (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port));
            var imgUrl = String.Format("{0}/Content/images/unknown.png", domain);
            return imgUrl;
        }
    }
}