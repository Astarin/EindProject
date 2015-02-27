using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EindProjectBusinessModels;
using EindProjectDAL;

namespace EindProjectMVC.Controllers
{
    public class VerlofController : Controller
    {
        //
        // GET: /Verlof/
        public ActionResult Index(int? ddlTeamLeden)
        {
            DalMethodes dal = new DalMethodes();
            if (ddlTeamLeden != null)
            {
                //ddlTeamLeden bevat hier het personeelsnr.
                Werknemer werknemer = dal.VraagWerknemerOp(ddlTeamLeden.ToString());
                Session["werknemer"] = werknemer;
                Session["currentUser"] = werknemer;
                ViewBag.ShowHR = werknemer.IsHr ? "" : "disabled='disabled'";
                ViewBag.ShowTeamLeader = werknemer.TeamLeader ? "" : "disabled='disabled'";
                ViewBag.ShowWerknemer = "";
                // return View();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login"); ;
            }
        }

        public ActionResult VerlofIndienen(VerlofAanvraag aanvraag, string btnSubmit, string ddlStatus, String txtFilterStartDatum, String txtFilterEindDatum)
        {
            DalMethodes methode = new DalMethodes();

            Werknemer wn = (Werknemer)Session["currentUser"];

            wn = methode.VraagWerknemerOp(wn.PersoneelsNr.ToString());

            // De werknemer heeft de gewijzigde toestanden van zijn verlofaanvragen gezien
            foreach (VerlofAanvraag item in wn.Verlofaanvragen)
            {
                methode.SetGelezen(item, true);
            }

            if (btnSubmit != null && btnSubmit == "Verlof Aanvragen")
            {
                try
                {
                    methode.IndienenVerlofaanvraag(wn, aanvraag);

                    // Om de gegevens te refreshen
                    wn = methode.VraagWerknemerOp(wn.PersoneelsNr.ToString());

                    // ********************************************************************************
                    // TO DO invullen van BehandeldDoor moet in de methode VraagWerknemerOp gebeuren !
                    using (DbEindproject db = new DbEindproject())
                    {
                        foreach (VerlofAanvraag item in wn.Verlofaanvragen)
                        {
                            item.BehandeldDoor = (from a in db.Verlofaanvragen
                                                  where a.Id == item.Id
                                                  select a.BehandeldDoor).FirstOrDefault();
                        }
                    }
                    // ********************************************************************************

                    VerlofAanvraag vA = wn.Verlofaanvragen.Find(x => x.Id == aanvraag.Id);
                    try
                    {
                        methode.StuurMail(wn, methode.GeefTeamLeader(wn.Team), vA);
                    }
                    catch (System.Net.Mail.SmtpException nmse)
                    {
                        return View("MailError", nmse);
                    }
                }
                catch (Exception exc)
                {
                    ViewBag.Error = exc.Message;
                }
            }

            PrepareFilterDatumVeldenInViewBag(txtFilterStartDatum, txtFilterEindDatum);
            PrepareStatusInViewBag(ddlStatus);

            ViewBag.ErrorMsg = ViewBag.Error;

            // Om de gegevens te refreshen
            wn = methode.VraagWerknemerOp(wn.PersoneelsNr.ToString());
            return View("WerknemerIngelogd", wn);
        }

        public ActionResult VerlofAnnuleren(string aanvraagId, string ddlStatus, String txtFilterStartDatum, String txtFilterEindDatum)
        {
            DalMethodes methode = new DalMethodes();
            Werknemer wn = (Werknemer)Session["currentUser"];

            methode.AnnuleerVerlofAanvraag(aanvraagId);

            wn = methode.VraagWerknemerOp(wn.PersoneelsNr.ToString());
            // ********************************************************************************
            // TO DO invullen van BehandeldDoor moet in de methode VraagWerknemerOp gebeuren !
            using (DbEindproject db = new DbEindproject())
            {
                foreach (VerlofAanvraag item in wn.Verlofaanvragen)
                {
                    item.BehandeldDoor = (from aanvraag in db.Verlofaanvragen
                                          where aanvraag.Id == item.Id
                                          select aanvraag.BehandeldDoor).FirstOrDefault();
                }
            }
            // ********************************************************************************

            VerlofAanvraag vA = wn.Verlofaanvragen.Find(x => x.Id.ToString() == aanvraagId);
            try
            {
                methode.StuurMail(wn, methode.GeefTeamLeader(wn.Team), vA);
            }
            catch (System.Net.Mail.SmtpException nmse)
            {
                return View("MailError", nmse);
            }

            PrepareFilterDatumVeldenInViewBag(txtFilterStartDatum, txtFilterEindDatum);
            PrepareStatusInViewBag(ddlStatus);

            return View("WerknemerIngelogd", wn);

        }

        private void PrepareStatusInViewBag(String ddlStatus)
        {
            DalMethodes methode = new DalMethodes();

            // maak de lijst van statussen die in de dropdownlist voor de filter moeten getoond worden.
            List<String> statusLijst = new List<string>();
            statusLijst.Add("");
            foreach (var item in Enum.GetValues(typeof(Aanvraagstatus)))
            {
                statusLijst.Add(item.ToString());
            }
            SelectList sl = new SelectList(statusLijst);
            ViewBag.ddlStatus = sl;

            // Maak lijst van statussen voor verlofaanvragen die zichtbaar moeten zijn in 
            // de lijst van verlofaanvragen in de view
            List<String> GeldigeStatussen = new List<string>();
            if (String.IsNullOrEmpty(ddlStatus))
            {
                GeldigeStatussen = methode.GeefListAanvraagstatus();
            }
            else
            {
                GeldigeStatussen.Add(ddlStatus);
            }
            ViewBag.Status = GeldigeStatussen;
        }

        private void PrepareFilterDatumVeldenInViewBag(string txtStartDatum, string txtEindDatum)
        {
            DateTime startDt;
            DateTime eindDt;
            String ErrorMsg = String.Empty;
            if (String.IsNullOrEmpty(txtStartDatum))
            {
                // geen startdatum opgegeven - niets in het veld ingegeven
                startDt = DateTime.MinValue;
            }
            else if (!DateTime.TryParse(txtStartDatum, out startDt))
            {
                ErrorMsg += "Geef een geldige startdatum in of maak het veld leeg." + Environment.NewLine;
            }
            // ofwel is ErrorMsg ingevuld, ofwel bevat startDt een geldige datum.
            if (String.IsNullOrEmpty(txtEindDatum))
            {
                // geen startdatum opgegeven - niets in het veld ingegeven
                eindDt = DateTime.MaxValue;
            }
            else if (!DateTime.TryParse(txtEindDatum, out eindDt))
            {
                ErrorMsg += "Geef een geldige eindDatum in of maak het veld leeg." + Environment.NewLine;
            }
            // ofwel is ErrorMsg ingevuld, ofwel bevat eindDt een geldige datum.

            // laad velden in ViewBag
            ViewBag.ErrorMsg = ErrorMsg;
            ViewBag.startDt = startDt;
            ViewBag.eindDt = eindDt;
        }
    }
}