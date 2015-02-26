using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EindProjectBusinessModels;
using System.Reflection;
using System.Data.Entity;
using System.Net.Mail;

namespace EindProjectDAL
{
    public class DalMethodes
    {

        /************** 1. BEHEREN VAN WERKNEMERS *******************/


        /**************************
         *                        *
         * 1.1 Beheren Werknemers *
         *                        *
         **************************/

        /**************************************
         * 1.1.1. Toevoegen van een werknemer *
         **************************************
         * Bernd 17/02/15                     *
         **************************************/
        public void VoegWerknemerToeAanDb(Werknemer werknemer, int teamCode)
        {
            // nodig om duplicatie van team te voorkomen
            using (DbEindproject db = new DbEindproject())
            {
                try
                {
                    werknemer.Team = HaalTeamVoorWerknemerUitDb(werknemer, teamCode, db);
                    SetInitieelPaswoord(werknemer);
                    db.Werknemers.Add(werknemer);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
        /*****************************************************
         * Hulpmethode Team ophalen voor bestaande werknemer *
         *****************************************************
         * Bernd                                             *
         *****************************************************/
        private Team HaalTeamVoorWerknemerUitDb(Werknemer werknemer, int teamCode, DbEindproject db)
        {
            Team team = (from t in db.Teams
                         where t.Code == teamCode
                         select t).First<Team>()
                              ;
            return team;
        }


        /**********************************
         * 1.1.2. Opvragen van werknemers *
         **********************************/
        /***************************************************************
         * 1.1.2. a. Opvragen van werknemers volgens bepaalde criteria *
         ***************************************************************
         * Roel 16/02/15                                               *
         ***************************************************************/
        public List<Werknemer> VraagWerknemerOp(string personeelsNr, string naam, string voornaam)
        {
            using (DbEindproject db = new DbEindproject())
            {
                List<Werknemer> wnLijst = (from w in db.Werknemers.Include(w => w.Team).Include(w => w.Verlofaanvragen)
                                           where w.Naam.Contains(naam)
                                           && w.Voornaam.Contains(voornaam)
                                           && w.PersoneelsNr.ToString().Contains(personeelsNr)
                                           orderby w.Naam, w.Voornaam, w.PersoneelsNr
                                           select w).ToList<Werknemer>();

                return wnLijst;
            }
            // throw new Exception("Er liep iets fout tijdens het opvragen van 0, 1 of meerdere werknemers");
        }

        public Werknemer VraagWerknemerOp(string personeelsNr)
        {
            using (DbEindproject db = new DbEindproject())
            {
                var wn = (from w in db.Werknemers.Include(w => w.Team).Include(w => w.Verlofaanvragen).Include(w => w.JaarlijksVerlof)
                          where w.PersoneelsNr.ToString() == personeelsNr
                          select w).FirstOrDefault();

                return (Werknemer)wn;
            }

        }
        /******************************************
        * 1.1.2. b. Opvragen van alle werknemers *
        ******************************************
        * David 16/02/15                         *
        ******************************************/
        public List<Werknemer> VraagAlleWerknemersOp()
        {
            return VraagWerknemerOp(string.Empty, string.Empty, string.Empty);
        }


        /*********************************
        * 1.1.3. Wijzigen van werknemers *
        **********************************
        * Roel 16/02/15                  *
        **********************************/
        public void WijzigWerknemerProperty(Werknemer werknemer, int teamCode)
        {

            using (DbEindproject db = new DbEindproject())
            {
                Werknemer wn = (from w in db.Werknemers
                                where w.PersoneelsNr == werknemer.PersoneelsNr
                                select w).FirstOrDefault();

                werknemer.Team = HaalTeamVoorWerknemerUitDb(werknemer, teamCode, db);
                // alle properties handmatig copieren. Vuile code maar kan moeilijk anders
                wn.Adres = werknemer.Adres;
                wn.Email = werknemer.Email;
                wn.Geboortedatum = werknemer.Geboortedatum;
                wn.Gemeente = werknemer.Gemeente;
                wn.JaarlijksVerlof = werknemer.JaarlijksVerlof;
                wn.Naam = werknemer.Naam;
                //wn.Paswoord = werknemer.Paswoord; niet op deze manier aanpassen!
                wn.PersoneelsNr = werknemer.PersoneelsNr;
                wn.Postcode = werknemer.Postcode;
                wn.Team = werknemer.Team;
                //wn.TeamLeader = werknemer.TeamLeader; // bij wijziging moet de teamleader niet gewijzigd kunnen worden
                wn.Verlofaanvragen = werknemer.Verlofaanvragen;
                wn.Voornaam = werknemer.Voornaam;

                //wn = werknemer; // werkt niet dbSet ziet de verandering niet omdat geheugenblok adres niet is veranderd.
                db.SaveChanges();
            }
        }

        /**************************
         *                        *
         * 1.2. Beheren van Teams *
         *                        *
        ***************************/

        /******************************
         * 1.2.1. Toevoegen van teams *
         ******************************
         * David 16/02/15             *
         ******************************/
        public void VoegTeamToeAanDb(Team team)
        {
            /*
             * Team wordt bepaald door autogenerated key (Code)
             * zelfde omschrijving kan en dient niet opgevangen (dixit Ann)
            */
            using (DbEindproject db = new DbEindproject())
            {
                try
                {
                    db.Teams.Add(team);
                    db.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }


        /********************************************
         * 1.2.2. Beheren van teamverantwoordelijken *
         ********************************************
         * David 16/02/15                           *
         ********************************************/

        public void BeheerTeamVerantwoordelijke(Werknemer werknemer)
        {
            /*
             * Huidige teamleader teamleader af maken en
             * geselecteerde werknemer teamleader maken
            */
            Team theTeam = werknemer.Team;
            using (DbEindproject db = new DbEindproject())
            {
                Werknemer huidigTL = (from wn in db.Werknemers.Include(wn => wn.Team)
                                      where wn.Team.Code == theTeam.Code
                                         && wn.TeamLeader
                                      select wn).FirstOrDefault();
                if (huidigTL != null)
                {
                    huidigTL.TeamLeader = false;
                }
                try
                {
                    Werknemer wn = (from w in db.Werknemers.Include(w => w.Team)
                                    where w.PersoneelsNr == werknemer.PersoneelsNr
                                    select w).FirstOrDefault();

                    wn.TeamLeader = true;
                    db.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }

        /********************************************
       * 1.2.2. Beheren van teamverantwoordelijken  - teamnaam wijzigen*
       ********************************************
       * David 19/02/15                           *
       ********************************************/
        public void WijzigTeamNaam(Team team)
        {
            using (DbEindproject db = new DbEindproject())
            {
                Team t = (from tmp in db.Teams
                          where tmp.Code == team.Code
                          select tmp).FirstOrDefault();
                try
                {
                    t.Naam = team.Naam;
                    db.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }

        }

        /**********************************************
         * Hulpmethode Heeft Team al een teamleader ? *
         **********************************************
         * onbekend auteur                            *
         **********************************************/
        public List<Team> OpvragenAlleTeams()
        {
            using (DbEindproject db = new DbEindproject())
            {
                List<Team> teamLijst = (from w in db.Teams
                                        select w).ToList<Team>();

                return teamLijst;
            }
        }



        /**********************************************
         * Hulpmethode Heeft Team al een teamleader ? *
         **********************************************
         * David 18/02/15                             *
        ***********************************************/
        public bool IsErAlEenTeamLeader(Team team)
        {
            using (DbEindproject db = new DbEindproject())
            {
                var tl = (from wn in db.Werknemers
                          where wn.Team.Code == team.Code
                             && wn.TeamLeader == true
                          select wn).FirstOrDefault();

                if (tl != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Werknemer GeefTeamLeader(Team team)
        {
            using (DbEindproject db = new DbEindproject())
            {
                Werknemer tl = (from wn in db.Werknemers
                                where wn.Team.Code == team.Code
                                   && wn.TeamLeader == true
                                select wn).FirstOrDefault();
                return tl;
            }
        }

        public Team GeefTeamMetCode(int code)
        {
            using (DbEindproject db = new DbEindproject())
            {
                Team team = (from t in db.Teams
                             where t.Code == code
                             select t).First();
                return team;
            }
        }

        /*****************************
         * 1.2.3. Opvragen van teams *
         *****************************
         * David 15/02/15            *
        *****************************/
        public List<Team> OpvragenTeams(string code, string teamnaam, string teamleader)
        {
            //  De medewerker geeft 0, 1 of meer van volgende criteria op:
            //   - Gedeelte van teamnaam
            //   - Gedeelte van naam van teamverantwoordelijke
            //   - Code
            // Het systeem toont de gegevens (code; naam; nummer, naam en voornaam teamverantwoordelijke;
            // nummer naam en voornaam van alle werknemers die tot het team behoren ) van de teams die aan
            // alle opgegeven criteria voldoen.  De gegevens zijn gesorteerd op teamnaam.  Binnen een team
            // zijn de gegevens van de werknemers gesorteerd op naam en voornaam van de werknemers.
            return null;
        }




        /**********************************
         * 1.2.4 Verwijderen van een team *
         **********************************/
        public void VerwijderTeam(Team team)
        {
            using (DbEindproject db = new DbEindproject())
            {
                var wn = from w in db.Werknemers
                         where w.Team.Code == team.Code
                         select w;
                // Zijn er nog werknemers die tot dit team behoren?
                // Zo ja, team blijft bestaan
                if (wn != null)
                {
                    throw new Exception("Er bestaan nog werknemers in dit team.");
                }
                else
                {
                    try
                    {
                        // Als er geen werknermers meer tot het team behoren, kan het gerust verwijdert worden
                        db.Teams.Remove(team);
                        db.SaveChanges();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }


        /*********************************
         * 1.2.5. Opvragen van Teamleden *
         *********************************
         * Roel 16/02/15                 *
         *********************************/
        public List<Werknemer> GeefTeamleden(Team team)
        {
            using (DbEindproject db = new DbEindproject())
            {
                List<Werknemer> wnLijst = (from w in db.Werknemers.Include(v => v.Verlofaanvragen)
                                           where w.Team.Code == team.Code
                                           select w).ToList<Werknemer>();
                return wnLijst;
            }
            throw new Exception("Opvragen Teamleden mislukt.");
        }

        /************** 2. BEHEREN VAN JAARLIJKSE VERLOVEN *******************/

        /******************************************
         *                                        *
         * 2.1. Toevoegen van jaarlijkse verloven *
         *                                        *
         ******************************************
         * Roel 16/02/15                          *
         ******************************************/
        public void WijzigJaarlijksVerlof(Werknemer werknemer, JaarlijksVerlof jaarlijksverlof)
        {
            if (jaarlijksverlof.Jaar < DateTime.Now.Year)
                throw new Exception("Het opgegeven jaartal ligt in het verleden.");
            if (jaarlijksverlof.AantalDagen < 0 || jaarlijksverlof.AantalDagen > 50)
                throw new Exception("Het aantal verlofdagen moet tussen 0(incl.) en 50(incl.) liggen.");
            using (DbEindproject db = new DbEindproject())
            {
                Werknemer wn = (from w in db.Werknemers.Include(w => w.JaarlijksVerlof)
                                where w.PersoneelsNr == werknemer.PersoneelsNr
                                select w).FirstOrDefault();

                foreach (JaarlijksVerlof jv in wn.JaarlijksVerlof)
                {
                    if (jv.Jaar == jaarlijksverlof.Jaar)
                    {
                        throw new Exception("De verlofdagen voor dit jaar voor deze werknemer waren al ingevuld.");
                    }
                }

                wn.JaarlijksVerlof.Add(jaarlijksverlof);
                db.SaveChanges();
            }
        }

        /****************************************************
         *                                                  *
         * 2.2. Beheren feestdagen & verplichte verlofdagen *
         *                                                  *
         ****************************************************/

        /************************************
         *                                  *
         * 2.3. Beheren van verlofaanvragen *
         *                                  *
         ************************************/

        /***************************************
         * 2.3.1. Indienen van verlofaanvragen *
         ***************************************/

        /***************************************************************************************
         * 2.3.1.1. Indienen van verlofaanvragen met geldige gegevens en voldoende verlofdagen *
         ***************************************************************************************
         * Roel 16/02/15                                                                       *
         ***************************************************************************************/
        public void IndienenVerlofaanvraag(Werknemer werknemer, VerlofAanvraag va)
        {
            using (DbEindproject db = new DbEindproject())
            {
                Werknemer wn = (from w in db.Werknemers.Include(w => w.Verlofaanvragen).Include(w => w.JaarlijksVerlof)
                                where w.PersoneelsNr == werknemer.PersoneelsNr
                                select w).FirstOrDefault();

                if (va.EindDatum < va.StartDatum)
                {
                    throw new Exception("De einddatum moet voor de begindatum komen.");
                }
                if (va.StartDatum < DateTime.Now.AddDays(14))
                {
                    throw new Exception("De startdatum moet minstens 14 kalenderdagen na vandaag komen.");
                }

                foreach (VerlofAanvraag item in wn.Verlofaanvragen)
                {
                    if (item.Toestand != Aanvraagstatus.Geannuleerd && item.Toestand != Aanvraagstatus.Afgekeurd)
                    {
                        if (OverlappendePeriode(va, item))
                        {
                            throw new Exception("U heeft een openstaande verlofaanvraag staan in deze periode.");
                        }
                    }
                }

                // de boolean zal onthouden of er voor die dag een verlofdag genomen wordt
                // of niet (weekend, collectieve, feestdag ...)
                Dictionary<DateTime, bool> periode = new Dictionary<DateTime, bool>();

                TimeSpan span = va.EindDatum - va.StartDatum;

                for (int i = 0; i <= span.Days; i++)
                {
                    periode[va.StartDatum.AddDays(i)] = true;
                }

                // geen verlofdagen aftrekken voor weekends
                for (int i = 0; i < periode.Count; i++)
                {
                    if (periode.ElementAt(i).Key.DayOfWeek == DayOfWeek.Saturday
                        || periode.ElementAt(i).Key.DayOfWeek == DayOfWeek.Sunday)
                    {
                        periode[periode.ElementAt(i).Key] = false;
                    }
                }

                // geen verlofdagen aftrekken voor collectieve sluitingen
                foreach (CollectieveSluiting item in db.Sluitingsdagen)
                {
                    if (periode.ContainsKey(item.StartDatum))
                    {
                        periode[item.StartDatum] = false;
                    }

                    if (item is CollectiefVerlof)
                    {
                        CollectiefVerlof col = (CollectiefVerlof)item;

                        TimeSpan spanCol = col.EindDatum - col.StartDatum;

                        for (int i = 0; i <= spanCol.Days; i++)
                        {
                            if (periode.ContainsKey(col.StartDatum.AddDays(i)))
                            {
                                periode[col.StartDatum.AddDays(i)] = false;
                            }
                        }
                    }
                }

                Dictionary<int, int> jvVanWn = new Dictionary<int, int>();
                foreach (JaarlijksVerlof item in wn.JaarlijksVerlof)
                {
                    jvVanWn[item.Jaar] = item.AantalDagen;
                }

                foreach (VerlofAanvraag item in wn.Verlofaanvragen)
                {


                    // al gebruikte verlofdagen berekenen (zie boven)
                    // aparte methode maken


                }

                foreach (KeyValuePair<DateTime, bool> kvp in periode)
                {
                    if (kvp.Value)
                    {
                        try
                        {
                            jvVanWn[kvp.Key.Year]--;
                        }
                        catch (Exception)
                        {
                            throw new Exception(String.Format("Uw verlofdagen voor het jaar {0} zijn nog onbekend, val het hr-team hiervoor lastig.", kvp.Key.Year));
                        }

                        if (jvVanWn[kvp.Key.Year] < 0)
                        {
                            throw new Exception(String.Format("U heeft te weinig verlofdagen in {0} voor deze verlofaanvraag.", kvp.Key.Year));
                        }
                    }
                }

                wn.Verlofaanvragen.Add(va);
                va.Toestand = Aanvraagstatus.Ingediend;
                db.SaveChanges();
            }
        }

        private bool OverlappendePeriode(VerlofAanvraag va1, VerlofAanvraag va2)
        {
            TimeSpan span1 = va1.EindDatum - va1.StartDatum;
            TimeSpan span2 = va2.EindDatum - va2.StartDatum;

            for (int i = 0; i <= span1.Days; i++)
            {
                for (int j = 0; j <= span2.Days; j++)
                {
                    if (va1.StartDatum.AddDays(i).Date == va2.StartDatum.AddDays(j).Date)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /*****************************************************************************************
         * 2.3.1.2. Indienen van verlofaanvragen met geldige gegevens en onvoldoende verlofdagen *
         *****************************************************************************************/

        /****************************************************************
         * 2.3.1.3. Indienen van verlofaanvragen met ongeldige gegevens *
         ****************************************************************/


        /*********************************************************************
         * 2.3.2. Annuleren van verlofaanvragen                              *
         * 2.3.3. Goedkeuren van verlofaanvragen door teamverantwoordelijken *
         * 2.3.4. Goedkeuren van verlofaanvragen door het systeem            *
         * 2.3.5. Afkeuren van verlofaanvragen                               *
         *********************************************************************
         * Frank                                                             *
         *********************************************************************/
        public void WijzigBehandelDatumVerlofaanvraag(VerlofAanvraag verlofaanvraag)
        {
            using (DbEindproject db = new DbEindproject())
            {
                VerlofAanvraag aanvraag = (from v in db.Verlofaanvragen
                                           where v.Id == verlofaanvraag.Id
                                           select v).FirstOrDefault();
                aanvraag.BehandelDatum = DateTime.Now;
                db.SaveChanges();
            }
        }

        public void WijzigGelezenVerlofaanvraag(VerlofAanvraag verlofaanvraag, bool isGelezen)
        {
            using (DbEindproject db = new DbEindproject())
            {
                VerlofAanvraag aanvraag = (from v in db.Verlofaanvragen
                                           where v.Id == verlofaanvraag.Id
                                           select v).FirstOrDefault();
                aanvraag.Gelezen = isGelezen;
                db.SaveChanges();
            }
        }
        public void WijzigBehandeldDoorVerlofaanvraag(VerlofAanvraag verlofaanvraag, Werknemer werknemer)
        {
            using (DbEindproject db = new DbEindproject())
            {
                VerlofAanvraag aanvraag = (from v in db.Verlofaanvragen
                                           where v.Id == verlofaanvraag.Id
                                           select v).FirstOrDefault();

                Werknemer teamleader = (from w in db.Werknemers
                                        where w.PersoneelsNr == werknemer.PersoneelsNr
                                        select w).FirstOrDefault();

                aanvraag.BehandeldDoor = teamleader;
                db.SaveChanges();
            }
        }

        public bool HeeftVerlofaanvraagStatus(VerlofAanvraag verlofaanvraag, Aanvraagstatus status)
        {
            using (DbEindproject db = new DbEindproject())
            {
                VerlofAanvraag aanvraag = (from v in db.Verlofaanvragen
                                           where v.Id == verlofaanvraag.Id
                                           select v).FirstOrDefault();
                return aanvraag.Toestand == status;
            }
        }

        public void WijzigStatusVerlofaanvraag(VerlofAanvraag verlofaanvraag, Aanvraagstatus status)
        {
            using (DbEindproject db = new DbEindproject())
            {
                VerlofAanvraag aanvraag = (from v in db.Verlofaanvragen
                                           where v.Id == verlofaanvraag.Id
                                           select v).FirstOrDefault();
                aanvraag.Toestand = status;
                db.SaveChanges();
            }
        }

        public void AnnuleerVerlofAanvraag(string aanvraagId)
        {
            using (DbEindproject db = new DbEindproject())
            {
                VerlofAanvraag aanvraag = (from v in db.Verlofaanvragen
                                           where v.Id.ToString() == aanvraagId
                                           select v).FirstOrDefault();
                if (aanvraag.StartDatum > DateTime.Now)
                {
                    aanvraag.Toestand = Aanvraagstatus.Geannuleerd;
                }
                db.SaveChanges();
            }
        }

        public void WijzigRedenAfkeurenVerlofaanvraag(VerlofAanvraag verlofaanvraag, String reden)
        {
            using (DbEindproject db = new DbEindproject())
            {
                VerlofAanvraag aanvraag = (from v in db.Verlofaanvragen
                                           where v.Id == verlofaanvraag.Id
                                           select v).FirstOrDefault();
                aanvraag.RedenVoorAfkeuren = reden;
                db.SaveChanges();
            }
        }

        public void SetGelezen(VerlofAanvraag x, bool p)
        {
            using (DbEindproject db = new DbEindproject())
            {
                VerlofAanvraag aanvraag = (from v in db.Verlofaanvragen
                                           where x.Id == v.Id
                                           select v).FirstOrDefault();
                aanvraag.Gelezen = p;
                db.SaveChanges();
            }
        }

        /*********************************************
         * 2.3.6. Opvragen lijst met verlofaanvragen *
         *********************************************/

        /***************************************************************************
         * 2.3.6.1. Opvragen lijst met verlofaanvragen door teamverantwoordelijken *
         ***************************************************************************/

        /***************************************************************
         * 2.3.6.2. Opvragen lijst met verlofaanvragen door werknemers *
         ***************************************************************/

        /************************************************************
         * 2.3.6.3. Opvragen lijst met verlofaanvragen door systeem *
         ************************************************************/

        /*******************************************************************************
         * Maken van melding ivm nieuwe status verlofaanvragen bij aanloggen werknemer *
         *******************************************************************************/
        public String MaakVerlofaanvraagLoginMelding(VerlofAanvraag aanvraag)
        {
            return CreateBody(null, null, aanvraag);
        }


        /************** 3. BEHEREN VAN ADV dagenJAARLIJKSE VERLOVEN *******************/

        /*********************************
         *                               *
         * 3.1. Registreren van overuren *
         *                               *
         *********************************/


        /********************************
         *                              *
         * 3.2. Aanvragen van ADV dagen *
         *                              *
         ********************************/


        /************** 4. AUTHORISATIE *******************/

        /***********************
         * Paswoordbehandeling *
         ***********************/
        /*********************************************** 
         * Wijzigen van het paswoord van een werknemer *
         ***********************************************/
        public void WijzigPaswoord(Werknemer w, String paswoord)
        {
            using (DbEindproject db = new DbEindproject())
            {
                Werknemer wn = (from werkn in db.Werknemers
                                where werkn.PersoneelsNr == w.PersoneelsNr
                                select werkn).FirstOrDefault();
                wn.Paswoord = paswoord;
                db.SaveChanges();
            }
        }

        public void SetInitieelPaswoord(Werknemer w)
        {
            w.Paswoord = w.Voornaam;
            //WijzigPaswoord(w, w.Voornaam);
        }

        /************** 5. GEBRUIK VAN KALENDER VOOR VISUALISATIE *******************/

        /*********************************************************************
         *                                                                   *
         * 5.1. Gebruik van een kalender bij indienen van een verlofaanvraag *
         *                                                                   *
         *********************************************************************/

        /********************************************************************************************
         *                                                                                          *
         * 5.2. Gebruik van een kalender bij het opvragen van de verlofaanvragen door een werknemer *
         *                                                                                          *
         ********************************************************************************************/

        /*******************************************************************************************************
         *                                                                                                     *
         * 5.3. Gebruik van een kalender bij het opvragen van de verlofaanvragen door de teamverantwoordelijke *
         *                                                                                                     *
         *******************************************************************************************************/



        /************** 6. E-MAIL *******************/

        /*****************************************************************************
         *                                                                           *
         * 6.1. Verzenden van email bij indienen of annuleren van een verlofaanvraag *
         * 6.2. Verzenden van email bij goekeuren of afkeuren van een verlofaanvraag *
         *                                                                           *
         *****************************************************************************/

        public void StuurMail(Werknemer zender, Werknemer ontvanger, VerlofAanvraag aanvraag)
        {
            MailAddress bestemmeling = new MailAddress(ontvanger.Email);
            MailAddress verzender = new MailAddress(zender.Email);

            MailMessage bericht = new MailMessage(verzender, bestemmeling);
            bericht.Subject = String.Format("Een verlofaanvraag is {0}.", aanvraag.Toestand);
            bericht.Body = CreateBody(zender, ontvanger, aanvraag);

            SmtpClient smtp = new SmtpClient("127.0.0.1");
            smtp.Send(bericht);
        }

        private String CreateBody(Werknemer zender, Werknemer ontvanger, VerlofAanvraag aanvraag)
        {
            StringBuilder sb = new StringBuilder();
            if (zender != null)
            {
                sb.AppendFormat("Melding door:\n {0} {1}\n PersNr. {2}\n {3}\n\n", zender.Voornaam, zender.Naam, zender.PersoneelsNr, zender.Email);
            }
            if (ontvanger != null)
            {
                sb.AppendFormat("Melding aan:\n {0} {1}\n PersNr. {2}\n {3}\n\n", ontvanger.Voornaam, ontvanger.Naam, ontvanger.PersoneelsNr, ontvanger.Email);
            }
            sb.AppendFormat("De verlofaanvraag met onderstaande gegevens werd {0} {1} {2}:\n",
                              aanvraag.Toestand,
                              aanvraag.BehandeldDoor == null ? "" : String.Format("door {0}", aanvraag.BehandeldDoor.Voornaam),
                              aanvraag.BehandeldDoor == null ? "" : aanvraag.BehandeldDoor.Naam
                              );
            sb.Append(Environment.NewLine);
            sb.AppendFormat("Aanvraagdatum : {0}", aanvraag.AanvraagDatum);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("Startdatum : {0}", aanvraag.StartDatum);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("Einddatum : {0}", aanvraag.EindDatum);
            sb.Append(Environment.NewLine);
            if (aanvraag.BehandeldDoor != null)
            {
                sb.AppendFormat("Aanvraag behandeld op : {0}", aanvraag.BehandelDatum);
                sb.Append(Environment.NewLine);
                sb.AppendFormat("Aanvraag behandeld door : {0} {1}", aanvraag.BehandeldDoor.Voornaam, aanvraag.BehandeldDoor.Naam);
                sb.Append(Environment.NewLine);
            }
            if (aanvraag.Toestand == Aanvraagstatus.Afgekeurd)
            {
                sb.AppendFormat("Reden weigering : {0}", aanvraag.RedenVoorAfkeuren);
            }
            return sb.ToString();
        }

        public Werknemer GetWerknemerWithUsernamePasw(string username, string paswoord)
        {
            using (DbEindproject db = new DbEindproject())
            {
                Werknemer werknemer = (from w in db.Werknemers.Include(w => w.JaarlijksVerlof).Include(w => w.Verlofaanvragen)
                                       where w.UserName == username
                                       && w.Paswoord == paswoord
                                       select w).FirstOrDefault();
                return werknemer;

            }
        }
        public bool IsUserNameInGebruik(string username)
        {
            using (DbEindproject db = new DbEindproject())
            {
                var usern = (from u in db.Werknemers
                             where u.UserName.ToUpper() == username.ToUpper()
                             select u.UserName).FirstOrDefault();
                if (string.IsNullOrEmpty(usern))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /************** 7. Algemene serviceroutines *******************/

        public List<String> GeefListAanvraagstatus()
        {
            List<String> lijst = new List<string>();
            foreach (var item in Enum.GetValues(typeof(Aanvraagstatus)))
            {
                lijst.Add(item.ToString());
            }
            return lijst;
        }
    }
}
