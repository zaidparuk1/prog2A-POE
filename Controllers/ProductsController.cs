using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using progPart2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace progPart2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly farmShopContext _context;

        public ProductsController(farmShopContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Items()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            return View(await _context.Products.ToListAsync());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Items(string searchString)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            var product = from p in _context.Products
                          select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.Productcategory.Contains(searchString));
            }


            return View(await product.ToListAsync());
        }


        public IActionResult AddToCart(int? id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            int CID = Int32.Parse(id.ToString());
            HttpContext.Session.SetInt32("CID", CID);

            return RedirectToAction("Items");




        }
        public IActionResult Purchase()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Purchase(int? id)
        {
            //check to see if user bought the product before if they did add if not make a new one in the db

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            string UID = HttpContext.Session.GetString("UserName");

            int CID = Int32.Parse(HttpContext.Session.GetInt32("CID").ToString());


            var sale = from s in _context.Sales
                       where s.ProductId == CID && s.Username == UID
                       select s;

            if (!sale.Any())//need works u need to see if this inserts and u need to edit 
            {
                Sales p = new Sales();
                p.ProductId = CID;
                p.Username = UID;
                p.TotalSales = 1;
                _context.Sales.Add(p);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("CID", 0);
            }
            else
            {

                Sales updatedsales = (from p in _context.Sales
                                      where p.ProductId == CID && p.Username == UID
                                      select p).FirstOrDefault();
                updatedsales.TotalSales = updatedsales.TotalSales + 1;
                _context.Update(updatedsales);
                await _context.SaveChangesAsync();



            }

            return View();

        }

        public IActionResult Error()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            return View();
        }

        public async Task<IActionResult> Cart()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            if (HttpContext.Session.GetInt32("CID") == null || HttpContext.Session.GetInt32("CID") == 0)
            {
                return RedirectToAction("Error");
            }
            int id = Int32.Parse(HttpContext.Session.GetInt32("CID").ToString());
            var product = await _context.Products
                  .FirstOrDefaultAsync(p => p.ProductId == id);


            return View(product);
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
