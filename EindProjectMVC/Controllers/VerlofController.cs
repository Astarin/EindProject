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
                if (werknemer.TeamLeader) Session["teamleader"] = werknemer;
                // TO DO: als werknemer in Team HR --> Session["hr"] = werknemer;
                ViewBag.ShowHR = werknemer.TeamLeader ? "" : "disabled='disabled'"; // aan te passen voor HR
                ViewBag.ShowTeamLeader = werknemer.TeamLeader ? "" : "disabled='disabled'";
                ViewBag.ShowWerknemer = "";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");;
            }
        }

        public ActionResult WerknemerAction(string PersoneelsNr)
        {
            if (PersoneelsNr == String.Empty || PersoneelsNr == null)
            {
                return View();
            }
            else
            {
                DalMethodes methode = new DalMethodes();

                Werknemer wn = methode.VraagWerknemerOp(PersoneelsNr, "", "").FirstOrDefault();

                return View("WerknemerIngelogd", wn);
            }
        }

        public ActionResult VerlofIndienen(VerlofAanvraag aanvraag, string PersoneelsNr)
        {
            DalMethodes methode = new DalMethodes();

            Werknemer wn = methode.VraagWerknemerOp(PersoneelsNr, "", "").FirstOrDefault();

            methode.IndienenVerlofaanvraag(wn, aanvraag);

            wn = methode.VraagWerknemerOp(PersoneelsNr, "", "").FirstOrDefault();

            return View("WerknemerIngelogd", wn);
        }
    }
}