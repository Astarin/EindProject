using EindProjectBusinessModels;
using EindProjectDAL;
using EindProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EindProjectMVC.Controllers
{
    public class LoginController : Controller
    {
        private DalMethodes methode = new DalMethodes();
        //
        // GET: /Login/
        public ActionResult Index()
        {
            if (Session["currentUser"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Login(string Username, string Paswoord)
        {
            Werknemer inlogger = methode.GetWerknemerWithUsernamePasw(Username, Paswoord);
            if (inlogger != null)
            {
                Session["currentUser"] = inlogger;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorMsg = "De combinatie wachtwoord en usernaam is niet gevonden.";
            return View("Index");

        }
        public ActionResult LogOut()
        {
            Session["currentUser"] = null;
            return View("Index");
        }
    }
}