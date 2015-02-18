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
            Werknemer teamLeader;
            Team team;

            if (Session["teamleader"] != null)
            {
                teamLeader = (Werknemer)Session["teamleader"];
                team = teamLeader.Team;
                ViewBag.IngelogdAls = String.Format("{0} {1}",teamLeader.Naam, teamLeader.Voornaam);
            }
            else
            {
                return View("Login/Index");
            }

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

            // *******************************************************
            //TO DO: Teamleader bepalen op basis van ingelogde teamleader + in Session variabele steken (indien nodig)
            // nu wordt de geselecteerde werknemer als teamleader in de session variabele gestoken
            //Session["teamleader"] = werknemer;
            // *******************************************************

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
                return View("Login/Index");
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
                dal.WijzigBehandeldDoorVerlofaanvraag(v, teamLeader);
                werknemer = (dal.VraagWerknemerOp(werknemer.PersoneelsNr.ToString(), "", "")).FirstOrDefault();

                // ********************************************************************************
                // TO DO invullen van BehandeldDoor moet in de methode VraagWerknemerOp gebeuren !
                using(DbEindproject db = new DbEindproject()){
                foreach (VerlofAanvraag item in werknemer.Verlofaanvragen)
                {
                    item.BehandeldDoor = (from aanvraag in db.Verlofaanvragen
                                         where aanvraag.Id == item.Id
                                         select aanvraag.BehandeldDoor).FirstOrDefault();
                }
                // ********************************************************************************

                }

                Session["werknemer"] = werknemer;
            }
            else { throw new NullReferenceException("Session[werknemer] is null."); }

            return View("InfoForWerknemer", werknemer);
        }

    }
}