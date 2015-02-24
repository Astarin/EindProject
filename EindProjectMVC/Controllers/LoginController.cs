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
            //DalMethodes Dal = new DalMethodes();

            //List<Werknemer> wnList = Dal.VraagAlleWerknemersOp();
            //var qry = from w in wnList
            //          select new SelectListItem
            //          {
            //              Text = String.Format("{0} {1} {2}", w.PersoneelsNr.ToString(), w.Naam, w.Voornaam),
            //              Value = w.PersoneelsNr.ToString()
            //          };
            ////ViewBag.ddlTeamLeden = new SelectList(wnList, "PersoneelsNr", "PersoneelsNr");
            //ViewBag.ddlTeamLeden = qry.ToList();
            if (Session["currentUser"] != null)
            {
                return RedirectToRoute("LoggedIn");
            }
            return View();
        }

        public ActionResult Login(string Username, string Paswoord)
        {
            Werknemer inlogger = methode.GetWerknemerWithUsernamePasw(Username, Paswoord);
            if (inlogger != null)
            {
                Session["currentUser"] = inlogger;
                return RedirectToAction("Index","Home");
            }
                return View("Index");
            
        }
	}
}