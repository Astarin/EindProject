using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EindProjectBusinessModels;
using System.Reflection;
using System.Data.SqlClient;

namespace EindProjectDAL
{
    public class DalMethodes
    {
        // Verijsten
        //1.1.1 Toevoegen van een werknemer     *
        //1.2.1. Toevoegen van een team         *
        //1.2.2 Beheren teamverantwoordelijken  *
        //2.1 Toevoegen van jaarlijkse verloven *
        //2.3.1.1 indien van verlofaanvragen met geldige gegevens en voldoende verlofdagen  *
        //2.3.3 Goedkeuren van verlofaanvragen door teamverantwoordelijken                 * 
        //2.3.5 afkeuren van verlofaanvragen

        //1.1.1 Toevoegen van een werknemer
        public void VoegWerkenerToeAanDb()
        {

        }

        public List<Werknemer> VraagAlleWerkenmersOp()
        {
            return new List<Werknemer>();   // todo
        }

        public List<Werknemer> VraagWerkenmerOp(string naam, string voornaam, string personeelsNr)
        {
            using (DbEindproject db = new DbEindproject())
            {
                List<Werknemer> wnLijst = (from w in db.Werknemers
                                           where w.Naam.Contains(naam)
                                           && w.Voornaam.Contains(voornaam)
                                           && w.PersoneelsNr.ToString().Contains(personeelsNr)
                                           orderby w.Naam, w.Voornaam, w.PersoneelsNr
                                           select w).ToList<Werknemer>();

                return wnLijst;
            }

            throw new Exception("Er liep iets fout tijdens het opvragen van 0, 1 of meerdere werknemers");
        }

        public void WijzigWerknemerProperty(Werknemer werknemer)
        {
            using (DbEindproject db = new DbEindproject())
            {
                Werknemer wn = (from w in db.Werknemers
                                where w.PersoneelsNr == werknemer.PersoneelsNr
                                select w).FirstOrDefault();

                // wn = werknemer;
                wn.Voornaam = werknemer.Voornaam;

                db.Entry(wn).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
        }

        //1.2.2 Beheren teamverantwoordelijken
        public void VoegTeamToeAanDb(Team team)
        {

        }
        //1.2.2 Beheren teamverantwoordelijken
        public void BeheerTeamVerantwoordelijke(Team team)
        {

        }
        public List<Werknemer> GeefTeamleden(Team team)
        {
            using (DbEindproject db = new DbEindproject())
            {
                List<Werknemer> wnLijst = (from w in db.Werknemers
                                           where w.Team.Code == team.Code
                                           select w).ToList<Werknemer>();
                return wnLijst;
            }
            throw new Exception("Opvragen Teamleden mislukt.");
        }

        //2.1 Toevoegen van jaarlijkse verloven *
        public void SetVerlofWerkNemer(Werknemer werknemer, int verlofDagen, int jaar)
        {
            using (DbEindproject db = new DbEindproject())
            {
                Werknemer wn = (from w in db.Werknemers
                                where w.PersoneelsNr == werknemer.PersoneelsNr
                                select w).FirstOrDefault();
                wn.VerlofDagenPerJaar.Add(jaar, verlofDagen);
                db.SaveChanges();
            }
        }

        //2.3.1.1 indien van verlofaanvragen met geldige gegevens en voldoende verlofdagen 
        public Aanvraagstatus IndienenVerlofaanvraag(Werknemer werknemer, VerlofAanvraag verlofaanvraag)
        {
            using (DbEindproject db = new DbEindproject())
            {
                Werknemer wn = (from w in db.Werknemers
                                where w.PersoneelsNr == werknemer.PersoneelsNr
                                select w).FirstOrDefault();
                wn.Verlofaanvragen.Add(verlofaanvraag);
                verlofaanvraag.Toestand = Aanvraagstatus.Ingediend;
                db.SaveChanges();
                return Aanvraagstatus.Ingediend;
            }
            throw new Exception("Er liep iets fout in de methode IndienenVerlofaanvraag in DAL");
        }

        //2.3.3 Goedkeuren van verlofaanvragen door teamverantwoordelijken      
        //2.3.5 afkeuren van verlofaanvragen
        public void WijzigStatusVerlofaanvraag(VerlofAanvraag verlofaanvraag, Aanvraagstatus status)
        {

        }







    }
}
