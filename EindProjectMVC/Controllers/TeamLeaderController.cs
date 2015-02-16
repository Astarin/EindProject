using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EindProjectDAL;
using EindProjectBusinessModels;

namespace EindProjectMVC.Controllers
{
    public class TeamLeaderController : Controller
    {
        //
        // GET: /TeamLeader/
        public ActionResult Index()
        {
            DalMethodes Dal = new DalMethodes();
            //Team team = new Team { Naam = "Smurfen", Code = 1 };
            Team team = new Team { Naam = "Nog Smurfen", Code = 2 };
            List<Werknemer> wnList = Dal.GeefTeamleden(team);

            var qry = from w in wnList
                      select new SelectListItem { 
                          Text = String.Format ("{0} {1} {2}",w.PersoneelsNr.ToString(),w.Naam,w.Voornaam), 
                          Value = w.PersoneelsNr.ToString() };
            //ViewBag.ddlTeamLeden = new SelectList(wnList, "PersoneelsNr", "PersoneelsNr");
            ViewBag.ddlTeamLeden = qry.ToList();
            return View();

        }

        public ActionResult InfoForWerknemer(int ddlTeamLeden)
        {
            DalMethodes dal = new DalMethodes();
            Werknemer werknemer = (dal.VraagWerknemerOp(ddlTeamLeden.ToString(), "", "")).FirstOrDefault();

            return View(werknemer);
        }

        public ActionResult UpdateStatusVerlofAanvraag(String Id)
        {
            // Id is de id van de verlofaanvraag.
            // To Do: 
            // 1) de werknemer mee ophalen waarbij de verlofaanvraag hoort
            // 2) de methode WijzigStatusVerlofaanvraag in DAL aanpassen zodat de wijziging van de verlofaanvraag
            //    ook de werknemer als input vraagt. (Twee verschillende werknemers kunnen beiden een verlofaanvraag
            //    met vb. Id=1 hebben !!
            return View("Index");
        }

    }
}