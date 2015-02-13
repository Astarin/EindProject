using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EindProjectMVC.Controllers
{
    public class VerlofController : Controller
    {
        //
        // GET: /Verlof/
        public ActionResult Index(String Actie)
        {
            return View();
        }

        public ActionResult HrAction()
        {
            return View();
        }

        public ActionResult TeamLeaderAction()
        {
            return View();
        }

        public ActionResult WerknemerAction()
        {
            return View();
        }


        public ActionResult ActionHR()
        {
            return View("HrView");
        }
	}
}