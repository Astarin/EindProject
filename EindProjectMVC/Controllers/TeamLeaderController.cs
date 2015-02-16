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
            Team team = new Team { Naam = "Smurfen", Code = 1 };
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

    }
}