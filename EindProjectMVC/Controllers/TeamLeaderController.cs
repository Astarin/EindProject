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
            Team team = new Team();
            //ViewBag.Teamleden = Dal.GeefTeamleden(team);

            // Provide Test Data for View
            List<Werknemer> wnList = new List<Werknemer>();
            wnList.Add(new Werknemer
            {
                Naam = "Jansen",
                Voornaam = "Jan",
                PersoneelsNr = 1,
                Adres = "AdresJansen",
                Gemeente = "GemeenteJansen",
                Postcode = "1000"
            });
            wnList.Add(new Werknemer
            {
                Naam = "Peeters",
                Voornaam = "Peter",
                PersoneelsNr = 2,
                Adres = "AdresPeeters",
                Gemeente = "GemeentePeeters",
                Postcode = "2000"
            });

            var qry = from w in wnList
                      select new SelectListItem { 
                          Text = String.Format ("{0} {1} {2}",w.PersoneelsNr.ToString(),w.Naam,w.Voornaam), 
                          Value = w.PersoneelsNr.ToString() };
            //ViewBag.ddlTeamLeden = new SelectList(wnList, "PersoneelsNr", "PersoneelsNr");
            ViewBag.ddlTeamLeden = qry.ToList();
            return View();

        }

        public ActionResult InfoForWerknemer(Werknemer werknemer)
        {
            return View();
        }

    }
}