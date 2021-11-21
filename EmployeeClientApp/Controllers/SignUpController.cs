using DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
namespace EmployeeClientApp.Controllers
{
    public class SignUpController : Controller
    {
        AppDbContext _db;
        public SignUpController(AppDbContext db)
        {
            _db = db;
        }


        public IActionResult CustomerRegistration()
        {
            
            return View();
        }

        /// <summary>
        /// Sk: Create new User
        /// </summary>
        /// <param name="objCust"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateCustomer(User objCust)
        {
            bool IsAuthenticationValid = false;
            ModelState.Remove("UserId");
        
            if (ModelState.IsValid)
            {
                var account = _db.Users.SingleOrDefault(x => x.Email == objCust.Email); ;
                if (account == null)
                {
                    //objCust.Password = BC.HashPassword(objCust.Password);
                    objCust.Password = EncyptionDecryption.Encrypt(objCust.Password);
                    objCust.ModifiedDate = DateTime.Now;
                    objCust.CreatedDate = DateTime.Now;
                    _db.Users.Add(objCust);
                    _db.SaveChanges();
                    IsAuthenticationValid = true;
                }
                else
                {
                    IsAuthenticationValid = false;
                    ModelState.AddModelError("Email", "Email already exist!");
                }
            }

            if (IsAuthenticationValid)
            {
                ModelState.Clear();

                ViewBag.SuccessMessage = "User created! successfully...";
                return View("CustomerRegistration");
            }
            else
            {
               
                return View("CustomerRegistration");
            }
        }


   

    }
}
