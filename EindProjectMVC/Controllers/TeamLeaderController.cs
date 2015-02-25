using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EindProjectDAL;
using EindProjectBusinessModels;
using EindProjectMVC.Models;

namespace EindProjectMVC.Controllers
{
    public class TeamLeaderController : Controller
    {
        //
        // GET: /TeamLeader/
        public ActionResult Index(String ErrorMsg)
        {
            DalMethodes Dal = new DalMethodes();
            Werknemer teamLeader;
            Team team;

            if (Session["currentUser"] != null)
            {
                teamLeader = (Werknemer)Session["currentUser"];
                team = teamLeader.Team;
            }
            else
            {
                return RedirectToAction("Index", "Login"); ;
            }

            List<Werknemer> wnList = Dal.GeefTeamleden(team);

            var qry = from w in wnList
                      select new SelectListItem
                      {
                          Text = String.Format("{0} {1} {2}", w.PersoneelsNr.ToString(), w.Naam, w.Voornaam),
                          Value = w.PersoneelsNr.ToString()
                      };
            ViewBag.ddlTeamLeden = qry.ToList();
            List<String> statusLijst = new List<string>();
            statusLijst.Add("");
            foreach (var item in Enum.GetValues(typeof(Aanvraagstatus)))
            {
                statusLijst.Add(item.ToString());
            }
            SelectList sl = new SelectList(statusLijst);
            ViewBag.ddlStatus = sl;
            if (String.IsNullOrEmpty(ErrorMsg)){ViewBag.ErrorMsg="";}
            else { ViewBag.ErrorMsg = ErrorMsg; }
            return View();

        }

        public ActionResult InfoForWerknemer(int? ddlTeamLeden, String ddlStatus, String btnSubmit, String txtStartDatum, String txtEindDatum)
        {
            String ErrorMsg = "";
            DalMethodes dal = new DalMethodes();
            Werknemer werknemer = null;
            List<Werknemer> werknemers = new List<Werknemer>();
            if (btnSubmit == "Toon verlofaanvragen")
            {
                DateTime startDt;
                DateTime EindDt;
                if (!String.IsNullOrEmpty(txtStartDatum) && !DateTime.TryParse(txtStartDatum, out startDt))
                {
                    ErrorMsg = "Geef een geldige startdatum in of maak het veld leeg.";
                }
                if (!String.IsNullOrEmpty(txtEindDatum) && !DateTime.TryParse(txtEindDatum, out EindDt))
                {
                    ErrorMsg = "Geef een geldige einddatum in of maak het veld leeg.";
                }
                if (String.IsNullOrEmpty(ErrorMsg))
                {
                    Team team = ((Werknemer)Session["currentUser"]).Team;
                    werknemers = dal.GeefTeamleden(team);
                    VulBehandeldDoorVelden(werknemers);
                    List<String> GeldigeStatussen = new List<string>();
                    if (String.IsNullOrEmpty(ddlStatus))
                    {
                        GeldigeStatussen = GeefListAanvraagstatus();
                    }
                    else
                    {
                        GeldigeStatussen.Add(ddlStatus);
                    }
                    ViewBag.Status = GeldigeStatussen;
                    Session["werknemer"] = werknemers;
                    return View("InfoForAllWerknemers");
                }
                else
                {
                    return RedirectToAction("Index", new { ErrorMsg = ErrorMsg });
                }
            }
            else
            {
                if (ddlTeamLeden == null)
                {
                    if (Session["personeelsNr"] != null)
                    {
                        ddlTeamLeden = (int)Session["personeelsNr"];
                        werknemer = dal.VraagWerknemerOp(ddlTeamLeden.ToString());
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    werknemer = dal.VraagWerknemerOp(ddlTeamLeden.ToString());
                }
                werknemers.Add(werknemer);
                Session["werknemer"] = werknemers;
                return View(werknemers[0]);
            }
        }

        private List<String> GeefListAanvraagstatus()
        {
            List<String> lijst = new List<string>();
            foreach (var item in Enum.GetValues(typeof(Aanvraagstatus)))
            {
                lijst.Add(item.ToString());
            }
            return lijst;
        }

        private void VulBehandeldDoorVelden(List<Werknemer> werknemers)
        {
            // ********************************************************************************
            // TO DO invullen van BehandeldDoor moet in de methode VraagWerknemerOp gebeuren !
            using (DbEindproject db = new DbEindproject())
            {
                foreach (Werknemer w in werknemers)
                {
                    foreach (VerlofAanvraag item in w.Verlofaanvragen)
                    {
                        item.BehandeldDoor = (from aanvraag in db.Verlofaanvragen
                                              where aanvraag.Id == item.Id
                                              select aanvraag.BehandeldDoor).FirstOrDefault();
                    }
                }
            }
            // ********************************************************************************
        }

        public ActionResult GoedkeurenStatusVerlofAanvraag(VerlofAanvraag v)
        {
            // Verlofaanvraag v is een gedeeltelijk ingevulde verlofaanvraag om de parameters
            // uit de form door te geven (id en RedenVoorAfkeuren)
            Werknemer werknemer = WijzigStatusVerlofAanvraag(v, Aanvraagstatus.Goedgekeurd);
            return View("InfoForWerknemer", werknemer);
        }

        public ActionResult AfkeurenStatusVerlofAanvraag(VerlofAanvraag v)
        {
            // Verlofaanvraag v is een gedeeltelijk ingevulde verlofaanvraag om de parameters
            // uit de form door te geven (id en RedenVoorAfkeuren)
            Werknemer werknemer = WijzigStatusVerlofAanvraag(v, Aanvraagstatus.Afgekeurd);
            return View("InfoForWerknemer", werknemer);
        }

        private Werknemer WijzigStatusVerlofAanvraag(VerlofAanvraag v, Aanvraagstatus status)
        {
            // Id is de id van de verlofaanvraag.
            Werknemer werknemer;
            Werknemer teamLeader;
            @ViewBag.ErrorMsg = "";
            DalMethodes dal = new DalMethodes();
            if (Session["currentUser"] == null)
            {
                throw new ArgumentNullException("Session[currentUser] is null in WijzigStatusVerlofAanvraag.");
            }
            else
            {
                teamLeader = (Werknemer)Session["currentUser"];
            }
            if (Session["werknemer"] != null)
            {
                werknemer = ((List<Werknemer>)Session["werknemer"])[0];

                // vanuit de gedeeltelijk ingevulde Verlofaanvraag wordt op het Id de
                // Verlofaanvraag uit de Werknemer opgehaald
                VerlofAanvraag vA = (from verl in werknemer.Verlofaanvragen
                                     where verl.Id == v.Id
                                     select verl).FirstOrDefault();

                // Verlofaanvragen mogen enkel goedgekeurd of afgekeurd worden indien
                // ze in toestand "Ingediend" zijn.
                if (dal.HeeftVerlofaanvraagStatus(vA, Aanvraagstatus.Ingediend))
                {
                    if (!(status == Aanvraagstatus.Afgekeurd && String.IsNullOrEmpty(v.RedenVoorAfkeuren)))
                    {
                        dal.WijzigStatusVerlofaanvraag(vA, status);
                        dal.WijzigRedenAfkeurenVerlofaanvraag(vA, v.RedenVoorAfkeuren);
                        dal.WijzigBehandelDatumVerlofaanvraag(vA);
                        dal.WijzigBehandeldDoorVerlofaanvraag(vA, teamLeader);
                        werknemer = dal.VraagWerknemerOp(werknemer.PersoneelsNr.ToString());
                        dal.WijzigGelezenVerlofaanvraag(vA, false);

                        // ********************************************************************************
                        // TO DO invullen van BehandeldDoor moet in de methode VraagWerknemerOp gebeuren !
                        using (DbEindproject db = new DbEindproject())
                        {
                            foreach (VerlofAanvraag item in werknemer.Verlofaanvragen)
                            {
                                item.BehandeldDoor = (from aanvraag in db.Verlofaanvragen
                                                      where aanvraag.Id == item.Id
                                                      select aanvraag.BehandeldDoor).FirstOrDefault();
                            }

                            // De verlofaanvraag is nodig voor het sturen van een mail. Omdat de aangepaste
                            // verlofaanvraag enkel in de Db zit, moet die eerst terug opgevraagd worden:
                            vA = (from aanvraag in db.Verlofaanvragen
                                  where aanvraag.Id == vA.Id
                                  select aanvraag).FirstOrDefault();
                        }
                        // ********************************************************************************

                        List<Werknemer> werknemers = new List<Werknemer>();
                        werknemers.Add(werknemer);
                        Session["werknemer"] = werknemers;
                        dal.StuurMail(teamLeader, werknemer, vA);
                    }
                    else
                    {
                        @ViewBag.ErrorMsg = "Reden voor afkeuren moet ingevuld zijn.";
                    }
                }
                else
                {
                    @ViewBag.ErrorMsg = "Verlofaanvraag moet in status ingediend zijn.";
                }
            }
            else
            { throw new NullReferenceException("Session[werknemer] is null."); }

            return werknemer;
        }
    }
}