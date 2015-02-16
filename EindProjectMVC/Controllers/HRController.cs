using EindProjectBusinessModels;
using EindProjectDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EindProjectMVC.Controllers
{
    public class HRController : Controller
    {
        //
        // GET: /HR/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HrNieuweWerknemer()
        {
            
            return View();
        }

        public ActionResult HrWijzigWerknemer()
        {
            DalMethodes methode = new DalMethodes();
            return View(methode.VraagAlleWerknemersOp());
        }

        public ActionResult HrWVerlofToevoegen()
        {
            return View();
        }

	}
}