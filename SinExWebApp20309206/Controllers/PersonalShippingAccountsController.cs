using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SinExWebApp20309206.Models;

namespace SinExWebApp20309206.Controllers
{
    public class PersonalShippingAccountsController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();


        // GET: PersonalShippingAccounts/Details/5
        

        // GET: PersonalShippingAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonalShippingAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShippingAccountId,phoneNumber,emailAddress,buildingInformation,streetInformation,city,provinceCode,postalCode,cardType,cardNumber,securityNumber,cardholderName,expiryMonth,expiryYear,firstName,lastName")] PersonalShippingAccount personalShippingAccount)
        {
            if (ModelState.IsValid)
            {
                //db.ShippingAccounts.Add(personalShippingAccount);
                //db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(personalShippingAccount);
        }

        // GET: PersonalShippingAccounts/Edit/5
        public ActionResult Edit()
        {
            if (System.Web.HttpContext.Current.User.IsInRole("Customer"))
            {
                string userName = System.Web.HttpContext.Current.User.Identity.Name;
                if (userName == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ShippingAccount tempshippingAccount = db.ShippingAccounts.SingleOrDefault(s => s.UserName == userName);
                if (tempshippingAccount == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int tempId = tempshippingAccount.ShippingAccountId;
                PersonalShippingAccount personalShippingAccount = (PersonalShippingAccount)db.ShippingAccounts.Find(tempId);

                return View(personalShippingAccount);
            }
            else { return RedirectToAction("Index", "Home"); }

            return HttpNotFound();
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalShippingAccount personalShippingAccount = (PersonalShippingAccount)db.ShippingAccounts.Find(id);
            if (personalShippingAccount == null)
            {
                return HttpNotFound();
            }
            return View(personalShippingAccount);*/
        }

        // POST: PersonalShippingAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShippingAccountId,phoneNumber,emailAddress,buildingInformation,streetInformation,city,provinceCode,postalCode,cardType,cardNumber,securityNumber,cardholderName,expiryMonth,expiryYear,firstName,lastName")] PersonalShippingAccount personalShippingAccount)
        {
            if (ModelState.IsValid)
            {
                personalShippingAccount.UserName= System.Web.HttpContext.Current.User.Identity.Name;
                db.Entry(personalShippingAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            
            return View(personalShippingAccount);
        }

        

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
