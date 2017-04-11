using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SinExWebApp20309206.Models;
using SinExWebApp20309206.ViewModels;

namespace SinExWebApp20309206.Controllers
{
    public class ServicePackageFeesController : BaseController
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();

        public ActionResult CalculateFee(string ServiceType,string PackageType,string CurrencyCode,bool? isOverWeight,int? PackageNum) {
            var feeSearch = new FeeCalculatorViewModel();
            feeSearch.ServicePackageFee = new FeeSearchViewModel();
            feeSearch.FeeResult = new FeeResultViewModel();

            feeSearch.ServicePackageFee.ServiceTypes = PopulateServiceTypeDropdownList().ToList();
            feeSearch.ServicePackageFee.PackageTypes = PopulatePackageTypeDropdownList().ToList();
            feeSearch.ServicePackageFee.Origins = PopulatePlaceDropdownList().ToList();
            feeSearch.ServicePackageFee.Destinations = PopulatePlaceDropdownList().ToList();
            feeSearch.ServicePackageFee.CurrencyCodes = PopulateCurrencyDropdownList().ToList();
            PackageType tempPackage = db.PackageTypes.SingleOrDefault(s => s.Type == PackageType);
            feeSearch.ServicePackageFee.PackageSizes = PopulateSizeDropdownList(tempPackage).ToList();
            if (tempPackage != null) {
                
                feeSearch.ServicePackageFee.PackageSizes = PopulateSizeDropdownList(tempPackage).ToList();
            }
            
            var servicePackageFeeQuery= from s in db.ServicePackageFees
                                    select new FeeListViewModel
                                    {
                                        ServiceType = s.ServiceType.Type,
                                        PackageType = s.PackageType.Type
                                    };

            if (ServiceType != null && PackageType != null) {
                servicePackageFeeQuery = servicePackageFeeQuery.Where(s => s.ServiceType == ServiceType);
                servicePackageFeeQuery = servicePackageFeeQuery.Where(s => s.PackageType == PackageType);
                feeSearch.ServicePackageFees = servicePackageFeeQuery.ToList();
                ServicePackageFee temp = db.ServicePackageFees.SingleOrDefault(s => (s.ServiceType.Type == ServiceType&& s.PackageType.Type == PackageType));
                if (temp != null) {//never fail?
                    float totalFee= (float)temp.Fee;
                    if (isOverWeight.HasValue) {
                        if ((bool)isOverWeight)
                        {
                            totalFee += 500;
                        }
                    }
                    if (PackageNum.HasValue)
                    {
                        if (PackageNum >= 1) {
                            totalFee *= (int)PackageNum;
                        }
                        
                    }
                    Currency cur = db.Currencies.SingleOrDefault(s => s.CurrencyCode == CurrencyCode);
                    if (cur != null) {
                        totalFee = totalFee * (float)cur.ExchangeRate;
                        feeSearch.FeeResult.CurrencyCode = cur.CurrencyCode;
                    }
                    else
                        feeSearch.FeeResult.CurrencyCode = "CNY";

                    feeSearch.FeeResult.Cost = totalFee;
                }
                    
            }
            else
            {
                feeSearch.FeeResult.Cost = 0;
                feeSearch.FeeResult.CurrencyCode ="CNY";
                feeSearch.ServicePackageFees = new FeeListViewModel[0];
            }
            
            
            return View(feeSearch);
        }

        private SelectList PopulateServiceTypeDropdownList()
        {
            var serviceTypeQuery = db.ServiceTypes.Select(s => s.Type).Distinct().OrderBy(s => s);
            return new SelectList(serviceTypeQuery);
        }

        private SelectList PopulatePackageTypeDropdownList()
        {
            var packageTypeQuery = db.PackageTypes.Select(s => s.Type).Distinct().OrderBy(s => s);
            return new SelectList(packageTypeQuery);
        }

        private SelectList PopulatePlaceDropdownList()
        {
            var placeQuery = db.Destinations.Select(s => s.ProvinceCode).Distinct().OrderBy(s => s);
            return new SelectList(placeQuery);
        }
        private SelectList PopulateSizeDropdownList(PackageType type)
        {
            if (type != null) {
                var sizeQuery = db.PackageTypeSizes.Select(s => s.PackageTypeID == type.PackageTypeID).Distinct().OrderBy(s => s);
                return new SelectList(sizeQuery);
            }
            var sizeQuery2= db.PackageTypeSizes.Select(s => s.PackageTypeID == 0).Distinct().OrderBy(s => s);//return nothing
            return new SelectList(sizeQuery2);
        }
        private SelectList PopulateCurrencyDropdownList()
        {
            var currencyQuery = db.Currencies.Select(s => s.CurrencyCode).Distinct().OrderBy(s => s);
            return new SelectList(currencyQuery);
        }

        // GET: ServicePackageFees
        public ActionResult Index()
        {
            var servicePackageFees = db.ServicePackageFees.Include(s => s.PackageType).Include(s => s.ServiceType);
            return View(servicePackageFees.ToList());
        }
        public ActionResult IndexCur(string currencyCode)
        {
            var servicePackageFees = db.ServicePackageFees.Include(s => s.PackageType).Include(s => s.ServiceType);
            foreach (ServicePackageFee servicePackageFee in servicePackageFees) {
                Currency cur = db.Currencies.SingleOrDefault(s => s.CurrencyCode == currencyCode);
                if (cur != null) {
                    servicePackageFee.Fee = servicePackageFee.Fee * (decimal)cur.ExchangeRate;
                    servicePackageFee.MinimumFee = servicePackageFee.MinimumFee * (decimal)cur.ExchangeRate;
                }
            }
            return View(servicePackageFees.ToList());
        }
        public ActionResult Index2()
        {
            var servicePackageFees = db.ServicePackageFees.Include(s => s.PackageType).Include(s => s.ServiceType);
            return View(servicePackageFees.ToList());
        }
        public ActionResult Index3(string currencyCode) {
            var servicePackageFees = db.ServicePackageFees.Include(s => s.PackageType).Include(s => s.ServiceType);
            foreach (ServicePackageFee fee in servicePackageFees){
                fee.Fee = ConvertCurrency(fee.Fee, currencyCode);
                fee.MinimumFee = ConvertCurrency(fee.MinimumFee, currencyCode);
            }
            return View(servicePackageFees.ToList());
        }

        // GET: ServicePackageFees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            if (servicePackageFee == null)
            {
                return HttpNotFound();
            }
            return View(servicePackageFee);
        }

        // GET: ServicePackageFees/Create
        public ActionResult Create()
        {
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type");
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type");
            return View();
        }

        // POST: ServicePackageFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServicePackageFeeID,Fee,MinimumFee,PackageTypeID,ServiceTypeID")] ServicePackageFee servicePackageFee)
        {
            if (ModelState.IsValid)
            {
                db.ServicePackageFees.Add(servicePackageFee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", servicePackageFee.PackageTypeID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type", servicePackageFee.ServiceTypeID);
            return View(servicePackageFee);
        }

        // GET: ServicePackageFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            if (servicePackageFee == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", servicePackageFee.PackageTypeID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type", servicePackageFee.ServiceTypeID);
            return View(servicePackageFee);
        }

        // POST: ServicePackageFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServicePackageFeeID,Fee,MinimumFee,PackageTypeID,ServiceTypeID")] ServicePackageFee servicePackageFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicePackageFee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PackageTypeID = new SelectList(db.PackageTypes, "PackageTypeID", "Type", servicePackageFee.PackageTypeID);
            ViewBag.ServiceTypeID = new SelectList(db.ServiceTypes, "ServiceTypeID", "Type", servicePackageFee.ServiceTypeID);
            return View(servicePackageFee);
        }

        // GET: ServicePackageFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            if (servicePackageFee == null)
            {
                return HttpNotFound();
            }
            return View(servicePackageFee);
        }

        // POST: ServicePackageFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServicePackageFee servicePackageFee = db.ServicePackageFees.Find(id);
            db.ServicePackageFees.Remove(servicePackageFee);
            db.SaveChanges();
            return RedirectToAction("Index");
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
