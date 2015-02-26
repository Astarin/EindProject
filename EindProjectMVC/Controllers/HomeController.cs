using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EindProjectBusinessModels;
using EindProjectDAL;

namespace EindProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["currentUser"] != null)
            {
                //// De werknemer wordt gerefreshed
                //Werknemer wn = (Werknemer)Session["currentUser"];
                //wn = new DalMethodes().VraagWerknemerOp(wn.PersoneelsNr.ToString());
                //Session["currentUser"] = wn;

                return View((Werknemer)Session["currentUser"]);
            }
            else
            {
                return RedirectToAction("Index", "Login"); ;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}