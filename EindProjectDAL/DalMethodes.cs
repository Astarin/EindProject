using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EindProjectBusinessModels;
using System.Reflection;

namespace EindProjectDAL
{
    class DalMethodes
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

        public List<Werknemer> VraagWerkenmerOp(string naam, string voornaam, int personeelsNr)
        {
            //op naam sorteren.
            return new List<Werknemer>(); // todo
        }

        public void WijzigWerknemerProperty(Werknemer werkenmer)
        {

            return;
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
            return new List<Werknemer>();
        }

        //2.1 Toevoegen van jaarlijkse verloven *
        public void SetVerlofWerkNemer(Werknemer werknemer, int verlofDagen, int jaar)
        {

        }

        //2.3.1.1 indien van verlofaanvragen met geldige gegevens en voldoende verlofdagen 
        public Aanvraagstatus IndienenVerlofaanvraag(Werknemer werknemer, VerlofAanvraag verlofaanvraag)
        {
            return Aanvraagstatus.Goedgekeurd;
        }

        //2.3.3 Goedkeuren van verlofaanvragen door teamverantwoordelijken      
        //2.3.5 afkeuren van verlofaanvragen
        public void WijzigStatusVerlofaanvraag(VerlofAanvraag verlofaanvraag, Aanvraagstatus status)
        {

        }







    }
}
