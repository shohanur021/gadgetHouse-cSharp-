using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHouse.Models;

namespace GadgetHouse.Controllers
{
    public class LoginRegisterController : Controller
    {

        GADGETHOUSEEntities1 db = new GADGETHOUSEEntities1();

        public ActionResult userLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult userLogin(User user)
        {
            var checkLogin = db.Users.Where(x => x.userEmail.Equals(user.userEmail) && x.userPassword.Equals(user.userPassword)).FirstOrDefault();
            if(checkLogin != null)
            {
                Session["userId"] = user.userId.ToString();
                Session["userEmail"] = user.userEmail.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Nofication = "Wrong email or password";
            }
            return View();
        }

        public ActionResult userRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult userRegistration(User user)
        {
            if (db.Users.Any(x => x.userEmail == user.userEmail))
            {
                ViewBag.Nofication = "This account has already existed.";
                return View();
            }
            else
            {
                db.Users.Add(user);
                db.SaveChanges();

                Session["userId"] = user.userId.ToString();
                Session["userEmail"] = user.userEmail.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

       
        public ActionResult userLogout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}