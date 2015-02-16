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
        DalMethodes methode = new DalMethodes();
        //
        // GET: /HR/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HrNieuweWerknemer()
        {
            if (ModelState.IsValid)
            {
                //TODO
            }
            return View();
        }

        public ActionResult HrSelecteerWerknemer()
        {
            
            List<Werknemer> werknemers = methode.VraagAlleWerknemersOp();
            return View(werknemers);
        }
        [HttpPost]
        public ActionResult HrWijzigWerknemer(string werknemerId)
        {
            Werknemer werknemer = methode.VraagWerknemerOp(werknemerId, "", "")[0]; // geef de 0 en normaal enige terug
            return View(werknemer);
        }

        public ActionResult HrWVerlofToevoegen()
        {
            return View();
        }

	}
}