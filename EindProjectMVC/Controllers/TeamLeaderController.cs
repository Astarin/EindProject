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
                ViewBag.IngelogdAls = String.Format("{0} {1}", teamLeader.Naam, teamLeader.Voornaam);
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
            // Session["teamleader"] is al eerder gemaakt.
            if (ddlTeamLeden == null)
            {
                if (Session["personeelsNr"] != null) ddlTeamLeden = (int)Session["personeelsNr"];
            }
            DalMethodes dal = new DalMethodes();
            Werknemer werknemer = dal.VraagWerknemerOp(ddlTeamLeden.ToString());
            Session["werknemer"] = werknemer;
            return View(werknemer);
        }

        public ActionResult GoedkeurenStatusVerlofAanvraag(VerlofAanvraag v)
        {
            Werknemer werknemer = WijzigStatusVerlofAanvraag(v, Aanvraagstatus.Goedgekeurd);
            return View("InfoForWerknemer", werknemer);
        }

        public ActionResult AfkeurenStatusVerlofAanvraag(VerlofAanvraag v)
        {
            Werknemer werknemer = WijzigStatusVerlofAanvraag(v, Aanvraagstatus.Afgekeurd);
            //werknemer.Verlofaanvragen[0].RedenVoorAfkeuren = "test";
            return View("InfoForWerknemer", werknemer);
        }

        private Werknemer WijzigStatusVerlofAanvraag(VerlofAanvraag v, Aanvraagstatus status)
        {
            // Id is de id van de verlofaanvraag.
            Werknemer werknemer;
            Werknemer teamLeader;
            DalMethodes dal = new DalMethodes();
            if (Session["teamleader"] == null)
            {
                throw new ArgumentNullException("Session[teamLeader] is null in WijzigStatusVerlofAanvraag.");
            }
            else
            {
                teamLeader = (Werknemer)Session["teamleader"];
            }
            if (Session["werknemer"] != null)
            {
                werknemer = (Werknemer)Session["werknemer"];

                VerlofAanvraag vA = (from verl in werknemer.Verlofaanvragen
                                     where verl.Id == v.Id
                                     select verl).FirstOrDefault();

                dal.WijzigStatusVerlofaanvraag(vA, status);
                dal.WijzigRedenAfkeurenVerlofaanvraag(vA, v.RedenVoorAfkeuren);
                dal.WijzigBehandelDatumVerlofaanvraag(vA);
                dal.WijzigBehandeldDoorVerlofaanvraag(vA, teamLeader);
                werknemer = dal.VraagWerknemerOp(werknemer.PersoneelsNr.ToString());

                // ********************************************************************************
                // TO DO invullen van BehandeldDoor moet in de methode VraagWerknemerOp gebeuren !
                using (DbEindproject db = new DbEindproject())
                {
                    foreach (VerlofAanvraag item in werknemer.Verlofaanvragen)
                    {
                        if (item.Id == vA.Id)
                        {
                            item.BehandeldDoor = (from aanvraag in db.Verlofaanvragen
                                                  where aanvraag.Id == item.Id
                                                  select aanvraag.BehandeldDoor).FirstOrDefault();
                        }
                    }
                }
                // ********************************************************************************

                Session["werknemer"] = werknemer;
            }
            else
            { throw new NullReferenceException("Session[werknemer] is null."); }

            return werknemer;
        }
    }
}