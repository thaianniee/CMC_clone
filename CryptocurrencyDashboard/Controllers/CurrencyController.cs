using CryptocurrencyDashboard.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CryptocurrencyDashboard.Controllers
{
    public class CurrencyController : Controller
    {
        CurrencyDAO currencyDAO = new CurrencyDAO();
        // GET: Currency
        public ActionResult Index(int? page)
        {
            var currencies = currencyDAO.getList();
            ViewBag.result = currencies;
            if (page == null) 
                page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            return View(currencies.ToPagedList(pageNumber, pageSize));
        }

        
        
        public ActionResult Details(string slug)
        {
            if(slug == null)
            {
                return RedirectToAction("Index");
            }
            var currencies = currencyDAO.getList();
            ViewBag.result = currencies;
            return View(currencyDAO.getInform(slug));
        }

       
       
    }
}
