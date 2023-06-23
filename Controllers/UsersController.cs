using Microsoft.AspNetCore.Mvc;
using progPart2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace progPart2.Controllers
{
	public class UsersController : Controller
	{
		private readonly farmShopContext _context;

		public UsersController(farmShopContext context)
		{
			_context = context;
		}
		public ActionResult Login()
		{

			if (HttpContext.Session.GetString("lCheck") as string == "failed")
			{
				ViewBag.Error = "Error please try again";
			}

			return View();
		}
		[HttpPost]//need to add this
		public ActionResult login(String tbxUserName, String tbxPassword)
		{
			bool found = false;
			//process the user
			foreach (Users c in _context.Users)//user is the table model  
			{
				if (c.Username.Equals(tbxUserName) && c.Password.Equals(tbxPassword))
				{
					HttpContext.Session.SetString("lCheck", "Passed");
					HttpContext.Session.SetString("UserName", c.Username);
					int UserLevel = Int32.Parse(c.Level.ToString());
					HttpContext.Session.SetInt32("UserLevel", UserLevel);


					found = true;
					return RedirectToAction("Success");//redirect to a succes page

				}
			}

			if (!found)//IF USERNAME ISNT IN the table
			{

				return RedirectToAction("Success");
			}

			return View();
		}

		public ActionResult Success()
		{
			if (HttpContext.Session.GetString("UserName") == "" || HttpContext.Session.GetString("UserName") == null)//so the user cant skip between tabs
			{
				HttpContext.Session.SetString("lCheck", "failed");
				return RedirectToAction("Login");//redirect to a succes page
			}
			else
			{
				ViewBag.UserName = HttpContext.Session.GetString("UserName");
				ViewBag.UserLevel = HttpContext.Session.GetInt32("UserLevel");
				return View();
			}

		}

		public ActionResult Logout()
		{
			HttpContext.Session.SetString("lCheck", "");
			HttpContext.Session.SetString("UserName", "");
			HttpContext.Session.SetInt32("UserLevel", 0);
			HttpContext.Session.SetInt32("CID", 0);
			ViewBag.UserLevel = null;
			ViewBag.UserName = null;
			return View();
		}

		private bool UsersExists(string id)
		{
			return _context.Users.Any(e => e.Username == id);
		}






		public IActionResult RegFarmer()
		{
			return View();
		}

		// POST: Users/Register
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RegFarmer([Bind("Username,Password,Level")] Users users)
		{
			users.Level = 2;
			_context.Add(users);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Login));
		}


		public IActionResult Register()
		{
			return View();
		}

		// POST: Users/Register
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register([Bind("Username,Password,Level")] Users users)
		{

			_context.Add(users);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Login));
		}


	}
}
