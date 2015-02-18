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
        List<Team> TeamsNieuweWerknemer;
        //
        // GET: /HR/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HrNieuweWerknemer(Werknemer werknemer, string team)
        {
            //dropdownlijst opvullen
            NieuweTeamslijstAanmaken();

            if (werknemer.Naam == null)
            {
                // TODO wss niets
            }
            else
            {
                try
                {
                    if (TeamLeaderControle(werknemer, team))
                    {
                        methode.VoegWerknemerToeAanDb(werknemer, int.Parse(team));
                    }
                }
                catch (Exception exc)
                {
                    //todo iet met de exc.Message
                }

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
            // overgang van selectie naar werknemer
            NieuweTeamslijstAanmaken();
            if (werknemerId == null)
            {
                //TODO ERROR
            }
            Werknemer werknemer = methode.VraagWerknemerOp(werknemerId.ToString(), "", "")[0]; // geef de 0 en normaal enige terug
            return View(werknemer);
        }
        [HttpPost]
        public ActionResult HrWijzigWerknemer(Werknemer werknemer, string team)
        {
            NieuweTeamslijstAanmaken();
            try
            {
                if (TeamLeaderControle(werknemer, team))
                {
                    //Werkt niet werknemer word niet aangepast? Dal Methode OK?
                    methode.WijzigWerknemerProperty(werknemer, int.Parse(team));
                }
            }
            catch (Exception exc)
            {
                //todo iet met de exc.Message
            }

            return View(werknemer);
        }

        public ActionResult HrTeamToevoegen(Team team)
        {
            if (team.Naam != String.Empty && team.Naam != null)
            {
                methode.VoegTeamToeAanDb(team);
            }

            return View();
        }

        public ActionResult HrWJaarlijksVerlofToevoegen(string werknemer, JaarlijksVerlof jaarlijksVerlof, string submit, string btnSelect)
        {
            if (submit != null)
            {
                btnSelect = "notnull";
                try
                {
                    Werknemer wn = methode.VraagWerknemerOp(werknemer);
                    methode.WijzigJaarlijksVerlof(wn, jaarlijksVerlof);
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                }
            }

            if (btnSelect != null)
            {
                Werknemer wn = methode.VraagWerknemerOp(werknemer);
                ViewBag.SelectedWnVerlof = wn.JaarlijksVerlof;
            }

            List<Werknemer> wns = methode.VraagAlleWerknemersOp();
            var qry = from w in wns
                      select new SelectListItem
             {
                 Text = String.Format("{0} {1} ", w.Voornaam, w.Naam),
                 Value = w.PersoneelsNr.ToString()
             };
            ViewBag.Werknemers = qry.ToList();

            return View();
        }

        public ActionResult HrTeamVerantwoordelijkeBeheren()
        {
            return View();
        }

        private void NieuweTeamslijstAanmaken()
        {
            if (TeamsNieuweWerknemer == null)
            {
                TeamsNieuweWerknemer = methode.OpvragenAlleTeams();
                var qry = from w in TeamsNieuweWerknemer
                          select new SelectListItem
                          {
                              Text = String.Format("{0} {1} ", w.Code.ToString(), w.Naam),
                              Value = w.Code.ToString()
                          };
                ViewBag.TeamsNieuweWerknemerDL = qry.ToList();
            }
        }
        private bool TeamLeaderControle(Werknemer werknemer, string team)
        {
            // Methode kan kijken of de teamleadereigenschap in orde is.
            if (werknemer.TeamLeader == true) // de Werknemer moet aangemaakt worden als teamleader.
            {
                if (methode.IsErAlEenTeamLeader(methode.GeefTeamMetCode(int.Parse(team))))
                {
                    throw new Exception("TODO: Team heeft al een teamleider.");
                }
                else
                {
                    // Team heeft nog geen teamleader dus werknemer kan toegevoegd worden.
                    return true;
                }
            }
            else
            {
                //
                return true;
            }
        }
    }
}