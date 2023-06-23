using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using progPart2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace progPart2.Controllers
{
    public class SalesController : Controller
    {
        private readonly farmShopContext _context;

        public SalesController(farmShopContext context)
        {
            _context = context;
        }

        // GET: Sales
        public IActionResult History()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            string UID = HttpContext.Session.GetString("UserName");


            var history = from s in _context.Sales
                          join p in _context.Products
                          on s.ProductId equals p.ProductId
                          select new SaleDetails { Sale = s, Product = p };

            history = history.Where(s => s.Sale.Username.Equals(UID));


            return View(history);
        }
        public IActionResult ViewSales()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            string UID = HttpContext.Session.GetString("UserName");


            var sale = from t in (
           from s in _context.Sales
           join p in _context.Products on s.ProductId equals p.ProductId
           select new { s, p }
           )
                       group t by new { t.p.ProductName } into g

                       select new SalesEdit { productName = g.Key.ProductName, SaleTotal = g.Sum(k => k.s.TotalSales) };




            return View(sale);
        }


        private bool SalesExists(string id)
        {
            return _context.Sales.Any(e => e.Username == id);
        }
    }
}
