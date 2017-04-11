using SinExWebApp20309206.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace SinExWebApp20309206.Controllers
{
    public class BaseController : Controller
    {
        private SinExDatabaseContext db = new SinExDatabaseContext();
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }

        public decimal ConvertCurrency(decimal value,string Currency)
        {
            if (Session[Currency] == null) {
                if (Currency == null)
                    Currency = "CNY";
                decimal curRate = (decimal)db.Currencies.SingleOrDefault(s => s.CurrencyCode == Currency).ExchangeRate;
                Session[Currency] = curRate;
            }
            
           
          
            decimal cvalue = value*((decimal)Session[Currency]);
            return cvalue;


        }
        
    }
}