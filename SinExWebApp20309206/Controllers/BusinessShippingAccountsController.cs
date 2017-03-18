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
    public class BusinessShippingAccountsController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        

        

        // GET: BusinessShippingAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessShippingAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShippingAccountId,phoneNumber,emailAddress,buildingInformation,streetInformation,city,provinceCode,postalCode,cardType,cardNumber,securityNumber,cardholderName,expiryMonth,expiryYear,contactPersonName,companyName,departmentName")] BusinessShippingAccount businessShippingAccount)
        {
            if (ModelState.IsValid)
            {
                //db.ShippingAccounts.Add(businessShippingAccount);
                //db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(businessShippingAccount);
        }

        // GET: BusinessShippingAccounts/Edit/5
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
                BusinessShippingAccount businessShippingAccount = (BusinessShippingAccount)db.ShippingAccounts.Find(tempId);

                return View(businessShippingAccount);
            }
            else { return RedirectToAction("Index", "Home"); }
            /* if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             BusinessShippingAccount businessShippingAccount = (BusinessShippingAccount)db.ShippingAccounts.Find(id);
             if (businessShippingAccount == null)
             {
                 return HttpNotFound();
             }
             return View(businessShippingAccount);*/
        }

        // POST: BusinessShippingAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShippingAccountId,phoneNumber,emailAddress,buildingInformation,streetInformation,city,provinceCode,postalCode,cardType,cardNumber,securityNumber,cardholderName,expiryMonth,expiryYear,contactPersonName,companyName,departmentName")] BusinessShippingAccount businessShippingAccount)
        {
            if (ModelState.IsValid)
            {
                businessShippingAccount.UserName = System.Web.HttpContext.Current.User.Identity.Name;
                db.Entry(businessShippingAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(businessShippingAccount);
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
