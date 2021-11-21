using DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace EmployeeClientApp.Controllers
{
    public class LoginController : Controller
    {

        AppDbContext _db;
        public LoginController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// SK: Check user credentials
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(Login objUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var account = _db.Users.SingleOrDefault(x => x.Email == objUser.Email);
                    //  if (account == null || !BC.Verify(objUser.Password, account.Password))
                    var existingPassword = EncyptionDecryption.Decrypt(account.Password);
                    if (account == null || existingPassword!=objUser.Password)

                    {
                        ModelState.AddModelError("Password", "Invalid username/password");
                        return View();
                    }
                    else
                    {
                        /* Update Login time */
                        account.ModifiedDate = DateTime.Now;
                        _db.SaveChanges();

                        //return RedirectToAction("EmployeeDetails", "Get", new { Username = account.FullName });
                        return RedirectToAction("Index", "EmployeeDetails");
                    }
                }
                return View();
            }
            catch (Exception e)
            {

                throw;
            }
        }




    }
}
