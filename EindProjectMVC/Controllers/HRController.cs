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
        public ActionResult HrNieuweWerknemer(Werknemer werknemer)
        {
            if (ModelState.IsValid)
            {
                //TODO
                methode.VoegWerknemerToeAanDb(werknemer);
            }

            return View();
        }


        public ActionResult HrSelecteerWerknemer()
        {
            
            List<Werknemer> werknemers = methode.VraagAlleWerknemersOp();
            return View(werknemers);
        }
       
        public ActionResult HrWijzigWerknemer(int? werknemerId)
        {
            if(werknemerId == null)
            {
                //TODO ERROR
            }
            Werknemer werknemer = methode.VraagWerknemerOp(werknemerId.ToString(), "", "")[0]; // geef de 0 en normaal enige terug
            return View(werknemer);
        }
        [HttpPost]
        public ActionResult HrWijzigWerknemer(Werknemer werknemer)
        {
            return View(werknemer);
        }

        public ActionResult HrWVerlofToevoegen()
        {
            return View();
        }

	}
}