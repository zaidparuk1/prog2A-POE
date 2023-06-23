using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using progPart2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace progPart2.Controllers
{
    public class Products1Controller : Controller
    {
        private farmShopContext _context;


        public Products1Controller(farmShopContext context)
        {
            _context = context;

        }

        // GET: Products1
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            string UID = HttpContext.Session.GetString("UserName");
            return View(await _context.Products.ToListAsync());
        }

        // GET: Products1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            string UID = HttpContext.Session.GetString("UserName");
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products1/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            string UID = HttpContext.Session.GetString("UserName");
            return View();
        }

        // POST: Products1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile theFile, [Bind("ProductId,ProductName,ProductDesc,ProductImage,ProductPrice,Productcategory")] Products products)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            string UID = HttpContext.Session.GetString("UserName");

            if (ModelState.IsValid)
            {

                theFile = HttpContext.Request.Form.Files[0];
                if (theFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        theFile.CopyTo(stream);
                        products.ProductImage = stream.ToArray();
                    }
                }

                _context.Products.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(products);
        }

        // GET: Products1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            string UID = HttpContext.Session.GetString("UserName");
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST: Products1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile theFile, [Bind("ProductId,ProductName,ProductDesc,ProductImage,ProductPrice,Productcategory")] Products products)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
            string UID = HttpContext.Session.GetString("UserName");
            if (id != products.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Products1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}