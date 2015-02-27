using EindProjectBusinessModels;
using EindProjectDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EindProjectMVC.Models;
using System.Data.Entity.Infrastructure;


namespace EindProjectMVC.Controllers
{
    [Autorisatie(RolOmschrijving = "Hr")]
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
            //eerst laten staan handeld rechten af!
            //if (!LoginMethode.HeeftRechten(Rol.Hr))
            //{
            //    return RedirectToAction("Index", "Login");
            //}
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
                        if (!methode.IsUserNameInGebruik(werknemer.UserName))
                        {
                            methode.VoegWerknemerToeAanDb(werknemer, int.Parse(team));
                            ViewBag.OkMsg = string.Format("De werkenemer: {0} is succevol aangemaakt", werknemer.Naam);
                        }
                        else
                        {
                            ViewBag.ErrorMsg = "De usernaam bestaat reeds. De nieuwe werknemer is niet toegevoegd. ";
                        }
                    }
                    return View();
                }
                catch (DbUpdateException)
                {
                    ViewBag.ErrorMsg = "Er is een fout opgetreden de datum is mogelijk niet goed ingevuld.";
                    //throw; //todo iet met de exc.Message
                }
                catch (Exception exc)
                {
                    ViewBag.ErrorMsg = "Er is een fout opgetreden de bewerking is niet uitgevoerd.";
                    //throw; //todo iet met de exc.Message
                }
            }
            return View();

        }

        public ActionResult HrSelecteerWerknemer()
        {
            List<Werknemer> werknemers = methode.VraagAlleWerknemersOp();
            return View(werknemers);
        }

        public ActionResult HrZoekWerknemer(string personeelsNr, string werknemerNaam, string werknemerVoorNaam)
        {
            List<Werknemer> werknemers = methode.VraagWerknemerOp(personeelsNr, werknemerNaam, werknemerVoorNaam);

            return View("HrSelecteerWerknemer", werknemers);
        }

        public ActionResult HrWijzigWerknemer(int? werknemerId)
        {
            // nog een doorvewijzing inlassen naar de meer volldige methode.
            // overgang van selectie naar werknemer
            NieuweTeamslijstAanmaken();
            if (werknemerId == null)
            {
                //TODO ERROR
            }
            Werknemer werknemer = methode.VraagWerknemerOp(werknemerId.ToString()); // geef de 0 en normaal enige terug
            
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
                    ViewBag.OkMsg = string.Format("De werknemer: {0} is aangepast.", werknemer.Naam);
                }
                else
                {
                    ViewBag.ErrorMsg = "Kan {0} geen teamleader maken. Er bestaat reeds een teamleader.";
                }
            }
            catch (DbUpdateException)
            {
                ViewBag.ErrorMsg = "Er is een fout opgetreden de datum is mogelijk niet goed ingevuld.";
                //throw; //todo iet met de exc.Message
            }
            catch (Exception exc)
            {
                ViewBag.ErrorMsg = "Er is een fout opgetreden de bewerking is niet uitgevoerd.";
                throw;
                //todo iet met de exc.Message
            }

            return View(werknemer);
        }

        public ActionResult HrTeamToevoegen(Team team)
        {
            if (team.Naam == null)
            {
                return View();
            }

            if (team.Naam != String.Empty && team.Naam != null)
            {
                methode.VoegTeamToeAanDb(team);
                ViewBag.OkMsg = "Het team is toegevoegd.";
            }
            else
            {
                ViewBag.ErrorMsg = "Het team is niet toegevoegd. Controleer of de naam correct is ingevuld.";
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
                    ViewBag.ErrorMsg = e.Message;
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

        /*************************************
         * HR/HrTeamverantwoordelijkeBeheren *
         *************************************
         * David 18/02/15                    *
         *************************************/
        public ActionResult HrTeamVerantwoordelijkeBeheren(string OpvragenTeam, string AlleTeams, string Bewaren, Team team, string Teamleden)
        {
            // Wat hebben we nodig :
            // lijst van teams met
            //  - code / naam van het team
            //  - alle werknemers v/h team
            //  - teamleader

            List<Team> alleteams = methode.OpvragenAlleTeams().ToList();
            ViewBag.AlleTeams = alleteams;
            if (OpvragenTeam != null)
            {
                // op submit button Opvragen geklikt
                // Team (via code) opvragen en doorgeven aan de view
                ViewBag.Zichtbaar = "display:inline";
                Team tmpTeam = methode.GeefTeamMetCode(int.Parse(AlleTeams));

                // list met werknemers voor dropdownbox
                List<Werknemer> lijstwerknemers = methode.GeefTeamleden(tmpTeam);
                ViewBag.LijstWerknemers = lijstwerknemers;

                return View(tmpTeam);
            }
            else
                if (Bewaren != null)
                {
                    // op submit button Bewaren geklikt
                    ViewBag.Melding = "aanpassingen  bewaard";
                    ViewBag.Zichtbaar = "display:none";
                    Team tmpTeam = methode.GeefTeamMetCode(int.Parse(AlleTeams));
                    tmpTeam.Naam = team.Naam;
                    methode.WijzigTeamNaam(tmpTeam);

                    // list met werknemers voor dropdownbox
                    List<Werknemer> lijstwerknemers = methode.GeefTeamleden(tmpTeam);
                    ViewBag.LijstWerknemers = lijstwerknemers;

                    Werknemer wn = methode.VraagWerknemerOp(Teamleden);
                    methode.BeheerTeamVerantwoordelijke(wn);

                    alleteams = methode.OpvragenAlleTeams().ToList();
                    ViewBag.AlleTeams = alleteams;
                    return View(tmpTeam);
                }
                else
                {
                    ViewBag.Zichtbaar = "display:none";
                    ViewBag.LijstWerknemers = new List<Werknemer>();
                    return View(new Team());
                }
        }

        public ActionResult HRGeefLijstTeams(string code, string teamNaam, string leiderNaam)
        {
            List<TeamViewModel> viewList;
            if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(teamNaam) && string.IsNullOrEmpty(leiderNaam))
            { // geen parameters opgegeven volledige lijst tonen
                viewList = methode.OpvragenAlleTeamsVolledig();

                return View(viewList);
            }

            if (string.IsNullOrEmpty(code))
            {
                code = "0";
            }
            viewList = methode.OpvragenTeamsVolledig(int.Parse(code), teamNaam, leiderNaam);
            if (viewList.Count == 0 && string.IsNullOrEmpty(teamNaam) && string.IsNullOrEmpty(leiderNaam))
            {
                viewList = methode.OpvragenTeamsVolledig(0, "", "");
            }
            return View(viewList);
        }

        public ActionResult VerwijderTeam(string teamCode)
        {
            try
            {
                Team team = methode.GeefTeamMetCode(int.Parse(teamCode));
                if (team == null)
                {
                    ViewBag.ErrorMsg = string.Format("Er is geen team met de opgegeve code: {0} gevonden", teamCode);
                    return View("HrGeefLijstTeams");
                }
                Werknemer werknemer = methode.GeefTeamLeader(team);
                if (werknemer == null)
                {
                    methode.VerwijderTeam(team);
                    ViewBag.OkMsg = string.Format("Het team: {0} is succesvol verwijderd.", team.Naam);
                }
                else
                {
                    ViewBag.ErrorMsg = string.Format("Er is geen team verwijderd, het team heeft nog een teamleider.", teamCode);
                }
            }
            catch(TeamHeeftWerknemerException)
            {
                ViewBag.ErrorMsg = string.Format("Team is niet verwijderd: Er zijn nog werknemers toegewezen aan het team");
            }
            return View("HrGeefLijstTeams");
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
                    return false;
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