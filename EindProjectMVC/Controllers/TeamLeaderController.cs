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
                      select new SelectListItem
                      {
                          Text = String.Format("{0} {1} {2}", w.PersoneelsNr.ToString(), w.Naam, w.Voornaam),
                          Value = w.PersoneelsNr.ToString()
                      };
            //ViewBag.ddlTeamLeden = new SelectList(wnList, "PersoneelsNr", "PersoneelsNr");
            ViewBag.ddlTeamLeden = qry.ToList();
            return View();

        }

        public ActionResult InfoForWerknemer(int? ddlTeamLeden)
        {
            if (ddlTeamLeden == null)
            {
                if (Session["personeelsNr"] != null) ddlTeamLeden = (int)Session["personeelsNr"];
            }
            DalMethodes dal = new DalMethodes();
            Werknemer werknemer = (dal.VraagWerknemerOp(ddlTeamLeden.ToString(), "", "")).FirstOrDefault();
            Session["werknemer"] = werknemer;
            //TO DO: opvragen Teamleader + in Session variabele steken
            Session["teamleader"] = new Werknemer
            {
                TeamLeader = true,
                Naam = "",
                Voornaam = "",
                Email = "lelijke@smurf.com",
                Geboortedatum = new DateTime(1892, 09, 22),
                Adres = "",
                Postcode = "",
                Gemeente = "",
                Paswoord = "Hash",
            };
            return View(werknemer);
        }

        public ActionResult UpdateStatusVerlofAanvraag(VerlofAanvraag v)
        {
            // Id is de id van de verlofaanvraag.
            Werknemer werknemer;
            Werknemer teamLeader;
            DalMethodes dal = new DalMethodes();
            if (Session["teamleader"] == null)
            {
                teamLeader = new Werknemer { Naam = "", Voornaam = "" };
            }
            else
            {
                teamLeader = (Werknemer)Session["teamleader"];
            }
            if (Session["werknemer"] != null)
            {
                werknemer = (Werknemer)Session["werknemer"];
                dal.WijzigStatusVerlofaanvraag(v, Aanvraagstatus.Goedgekeurd);
                dal.WijzigBehandelDatumVerlofaanvraag(v);
                //dal.WijzigBehandeldDoorVerlofaanvraag(v, teamLeader);
                werknemer = (dal.VraagWerknemerOp(werknemer.PersoneelsNr.ToString(), "", "")).FirstOrDefault();
            }
            else { throw new NullReferenceException("Session[werknemer] is null."); }

            return View("InfoForWerknemer", werknemer);
        }

    }
}