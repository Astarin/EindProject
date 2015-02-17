using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EindProjectBusinessModels
{
    class DbEindprojectInitializer : DropCreateDatabaseAlways<DbEindproject>
    {
        protected override void Seed(DbEindproject db)
        {
            db.Database.Log = Console.Write;


            // Testdata

            // Collectieve verloven
            // Feestdagen 2015
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Nieuwjaar", StartDatum = new DateTime(2015, 01, 01), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Pasen", StartDatum = new DateTime(2015, 04, 05), Terugkerend = false });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Paasmaandag", StartDatum = new DateTime(2015, 04, 06), Terugkerend = false });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Feest van de Smurf", StartDatum = new DateTime(2015, 05, 01), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "O.L.Grote Smurf Hemelvaart", StartDatum = new DateTime(2015, 05, 14), Terugkerend = false });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Pinksteren", StartDatum = new DateTime(2015, 05, 24), Terugkerend = false });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Pinkstermaandag", StartDatum = new DateTime(2015, 05, 25), Terugkerend = false });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Nationaal SmurfFeest", StartDatum = new DateTime(2015, 07, 21), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "O.L.Smurfin Hemelvaart", StartDatum = new DateTime(2015, 08, 15), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Allerheiligen", StartDatum = new DateTime(2015, 11, 01), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Allerzielen", StartDatum = new DateTime(2015, 11, 02), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Wapenstilstand", StartDatum = new DateTime(2015, 11, 11), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Kerstmis", StartDatum = new DateTime(2015, 12, 25), Terugkerend = true });
            // Feestdagen 2016
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Nieuwjaar", StartDatum = new DateTime(2016, 01, 01), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Pasen", StartDatum = new DateTime(2016, 03, 27), Terugkerend = false });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Paasmaandag", StartDatum = new DateTime(2016, 03, 28), Terugkerend = false });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Feest van de Smurf", StartDatum = new DateTime(2016, 05, 01), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "O.L.Grote Smurf Hemelvaart", StartDatum = new DateTime(2016, 05, 05), Terugkerend = false });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Pinksteren", StartDatum = new DateTime(2016, 05, 15), Terugkerend = false });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Pinkstermaandag", StartDatum = new DateTime(2016, 05, 16), Terugkerend = false });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Nationaal SmurfFeest", StartDatum = new DateTime(2016, 07, 21), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "O.L.Smurfin Hemelvaart", StartDatum = new DateTime(2016, 08, 15), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Allerheiligen", StartDatum = new DateTime(2016, 11, 01), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Allerzielen", StartDatum = new DateTime(2016, 11, 02), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Wapenstilstand", StartDatum = new DateTime(2016, 11, 11), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Kerstmis", StartDatum = new DateTime(2016, 12, 25), Terugkerend = true });
            // collectief verlof 2015
            db.Sluitingsdagen.Add(new CollectiefVerlof { Omschrijving = "zomerverlof", StartDatum = new DateTime(2015, 07, 06), EindDatum = new DateTime(2015, 07, 26), Terugkerend = false });
            db.Sluitingsdagen.Add(new CollectiefVerlof { Omschrijving = "winterverlof", StartDatum = new DateTime(2015, 12, 26), EindDatum = new DateTime(2015, 12, 31), Terugkerend = false });
            // collectief verlof 2016
            db.Sluitingsdagen.Add(new CollectiefVerlof { Omschrijving = "zomerverlof", StartDatum = new DateTime(2016, 07, 04), EindDatum = new DateTime(2015, 07, 24), Terugkerend = false });
            db.Sluitingsdagen.Add(new CollectiefVerlof { Omschrijving = "winterverlof", StartDatum = new DateTime(201, 12, 26), EindDatum = new DateTime(2015, 12, 31), Terugkerend = false });


            //Teams
            Team smurfTeam1 = new Team { Naam = "HR" };
            db.Teams.Add(smurfTeam1);
            Team smurfTeam2 = new Team { Naam = "Lol smurfen" };
            db.Teams.Add(smurfTeam2);
            Team smurfTeam3 = new Team { Naam = "Bril smurfen" };
            db.Teams.Add(smurfTeam3);
            Team smurfTeam4 = new Team { Naam = "Mopper smurfen" };
            db.Teams.Add(smurfTeam4);
            Team smurfTeam5 = new Team { Naam = "Potige smurgen" };
            db.Teams.Add(smurfTeam5);

            db.SaveChanges();   // Teams worden opgeslagen, zodat ze kunnen gebruikt worden bij aanmaak van werknemers


            // Werknemers
            // Werknemers met 'normaal' aantal vakantiedagen (2015 = 30, 2016 = 32)
            // Werknemers wiens personeelsnummer deelbaar is door 5 => Niet standaard verlofdagen (2015 = 20, 2016 = 30)
            // Werknemers 1,5,11,.. zijn de teamleaders

            // standaardVerlofDagen
            List<JaarlijksVerlof> standaardVerlofDagen = new List<JaarlijksVerlof>();
            standaardVerlofDagen.Add(new JaarlijksVerlof { Jaar = 2015, AantalDagen = 30 });
            standaardVerlofDagen.Add(new JaarlijksVerlof { Jaar = 2016, AantalDagen = 32 });
            
            List<JaarlijksVerlof> nietStandaardVerlofDagen = new List<JaarlijksVerlof>();
            nietStandaardVerlofDagen.Add(new JaarlijksVerlof { Jaar = 2015, AantalDagen = 20});
            nietStandaardVerlofDagen.Add(new JaarlijksVerlof { Jaar = 2016, AantalDagen = 30 });

            // SmurfTeam 1 : Smurf 1 t.e.m. 5
            Werknemer smurf1 = new Werknemer
            {
                TeamLeader = true,
                Naam = "Smurf",
                Voornaam = "Grote",
                Email = "grote.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1842, 09, 22),
                Adres = "Smurfenstraat 1",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam1
            };

            Werknemer smurf2 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Muziek",
                Email = "muziek.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1889, 05, 14),
                Adres = "Smurfenstraat 2",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam1
            };

            Werknemer smurf3 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Vlieg",
                Email = "vlieg.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1905, 01, 18),
                Adres = "Smurfenstraat 3",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam1
            };

            Werknemer smurf4 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Smurfatje",
                Email = "smurfatje.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1925, 04, 04),
                Adres = "Smurfenstraat 4",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam1
            };

            Werknemer smurf5 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Luilak",
                Email = "luilak.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1887, 08, 11),
                Adres = "Smurfenstraat 5",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = nietStandaardVerlofDagen,
                Team = smurfTeam1
            };


            // SmurfTeam 2 : Smurf 6 t.e.m. 10
            Werknemer smurf6 = new Werknemer
            {
                TeamLeader = true,
                Naam = "Smurf",
                Voornaam = "Lol",
                Email = "lol.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1875, 12, 02),
                Adres = "Smurfenstraat 6",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam2
            };

            Werknemer smurf7 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Smul",
                Email = "smul.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1901, 03, 03),
                Adres = "Smurfenstraat 7",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam2
            };

            Werknemer smurf8 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Grap",
                Email = "frap.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1915, 11, 28),
                Adres = "Smurfenstraat 8",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam2
            };

            Werknemer smurf9 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Dicht",
                Email = "dicht.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1884, 10, 16),
                Adres = "Smurfenstraat 9",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam2
            };

            Werknemer smurf10 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Knutsel",
                Email = "knutsel.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1894, 09, 20),
                Adres = "Smurfenstraat 10",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = nietStandaardVerlofDagen,
                Team = smurfTeam2
            };

            // SmurfTeam 3 : Smurf 10 t.e.m. 15
            Werknemer smurf11 = new Werknemer
            {
                TeamLeader = true,
                Naam = "Smurf",
                Voornaam = "Bril",
                Email = "bril.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1897, 06, 12),
                Adres = "Smurfenstraat 11",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam3
            };

            Werknemer smurf12 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Ruimte",
                Email = "ruimte.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1916, 10, 05),
                Adres = "Smurfenstraat 12",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam3
            };

            Werknemer smurf13 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Honderste",
                Email = "honderste.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1899, 08, 02),
                Adres = "Smurfenstraat 13",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam3
            };

            Werknemer smurf14 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Leerling",
                Email = "leerling.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1946, 08, 08),
                Adres = "Smurfenstraat 14",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam3
            };

            Werknemer smurf15 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Dieren",
                Email = "dieren.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1909, 02, 20),
                Adres = "Smurfenstraat 15",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = nietStandaardVerlofDagen,
                Team = smurfTeam3
            };

            // SmurfTeam 4 : Smurf 16 t.e.m. 20
            Werknemer smurf16 = new Werknemer
            {
                TeamLeader = true,
                Naam = "Smurf",
                Voornaam = "Mopper",
                Email = "mopper.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1896, 06, 13),
                Adres = "Smurfenstraat 16",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam4
            };

            Werknemer smurf17 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Schele",
                Email = "schele.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1900, 12, 05),
                Adres = "Smurfenstraat 17",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam4
            };

            Werknemer smurf18 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Rara",
                Email = "rare.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1899, 08, 02),
                Adres = "Smurfenstraat 18",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam4
            };

            Werknemer smurf19 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Kok",
                Email = "kok.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1906, 01, 05),
                Adres = "Smurfenstraat 19",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam4
            };

            Werknemer smurf20 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Beeldhouw",
                Email = "beeldhouw.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1911, 05, 30),
                Adres = "Smurfenstraat 20",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = nietStandaardVerlofDagen,
                Team = smurfTeam4
            };

            // SmurfTeam 5 : Smurf 21 t.e.m. 25
            Werknemer smurf21 = new Werknemer
            {
                TeamLeader = true,
                Naam = "Smurf",
                Voornaam = "Potige",
                Email = "potige.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1886, 05, 13),
                Adres = "Smurfenstraat 21",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam5
            };

            Werknemer smurf22 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Super",
                Email = "super.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1920, 09, 12),
                Adres = "Smurfenstraat 22",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam5
            };

            Werknemer smurf23 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Klungel",
                Email = "klungel.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1897, 02, 03),
                Adres = "Smurfenstraat 23",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam5
            };

            Werknemer smurf24 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Verliefde",
                Email = "verliefde.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1889, 01, 24),
                Adres = "Smurfenstraat 24",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = standaardVerlofDagen,
                Team = smurfTeam5
            };

            Werknemer smurf25 = new Werknemer
            {
                TeamLeader = false,
                Naam = "Smurf",
                Voornaam = "Baby",
                Email = "baby.smurf@smurfnet.com",
                Geboortedatum = new DateTime(1992, 09, 11),
                Adres = "Smurfenstraat 25",
                Postcode = "2340",
                Gemeente = "Smurfstad",
                Paswoord = "Passwoord",
                JaarlijksVerlof = nietStandaardVerlofDagen,
                Team = smurfTeam5
            };

            // 25 smurfen gelijk verdeeld over 5 teams //
            db.Werknemers.Add(smurf1);
            db.Werknemers.Add(smurf2);
            db.Werknemers.Add(smurf3);
            db.Werknemers.Add(smurf4);
            db.Werknemers.Add(smurf5);
            db.Werknemers.Add(smurf6);
            db.Werknemers.Add(smurf7);
            db.Werknemers.Add(smurf8);
            db.Werknemers.Add(smurf9);
            db.Werknemers.Add(smurf10);
            db.Werknemers.Add(smurf11;
            db.Werknemers.Add(smurf12);
            db.Werknemers.Add(smurf13);
            db.Werknemers.Add(smurf14);
            db.Werknemers.Add(smurf15);
            db.Werknemers.Add(smurf16);
            db.Werknemers.Add(smurf17);
            db.Werknemers.Add(smurf18);
            db.Werknemers.Add(smurf19);
            db.Werknemers.Add(smurf20);
            db.Werknemers.Add(smurf21);
            db.Werknemers.Add(smurf22);
            db.Werknemers.Add(smurf23);
            db.Werknemers.Add(smurf24);
            db.Werknemers.Add(smurf25);

            db.SaveChanges();


            // Verlofaanvragen
                // Reeds Goedgekeurde
                // Reeds Afgekeurde
                // Geannuleerde
                // Ingediende  (nog goed- of af te keuren)
            
            VerlofAanvraag verlofAanvraag1 = new VerlofAanvraag
            {
                AanvraagDatum = new DateTime(2015, 02, 28),
                StartDatum = new DateTime(2015, 04, 01),
                EindDatum = new DateTime(2015, 04, 10),
                Toestand = Aanvraagstatus.Goedgekeurd
            };

            smurf2.Verlofaanvragen.Add(verlofAanvraag1);
            smurf4.Verlofaanvragen.Add(verlofAanvraag1);
            smurf6.Verlofaanvragen.Add(verlofAanvraag1);
            smurf8.Verlofaanvragen.Add(verlofAanvraag1);
            smurf10.Verlofaanvragen.Add(verlofAanvraag1);
            smurf12.Verlofaanvragen.Add(verlofAanvraag1);
            smurf14.Verlofaanvragen.Add(verlofAanvraag1);
            smurf16.Verlofaanvragen.Add(verlofAanvraag1);
            smurf18.Verlofaanvragen.Add(verlofAanvraag1);
            smurf20.Verlofaanvragen.Add(verlofAanvraag1);
            smurf22.Verlofaanvragen.Add(verlofAanvraag1);
            smurf24.Verlofaanvragen.Add(verlofAanvraag1);

            VerlofAanvraag verlofAanvraag2 = new VerlofAanvraag
            {
                AanvraagDatum = new DateTime(2015, 02, 28),
                StartDatum = new DateTime(2015, 05, 11),
                EindDatum = new DateTime(2015, 05, 15),
                Toestand = Aanvraagstatus.Goedgekeurd
            };

            smurf2.Verlofaanvragen.Add(verlofAanvraag2);
            smurf6.Verlofaanvragen.Add(verlofAanvraag2);
            smurf14.Verlofaanvragen.Add(verlofAanvraag2);
            smurf18.Verlofaanvragen.Add(verlofAanvraag2);
            smurf22.Verlofaanvragen.Add(verlofAanvraag2);


            VerlofAanvraag verlofAanvraag3 = new VerlofAanvraag
            {
                AanvraagDatum = new DateTime(2015, 02, 28),
                StartDatum = new DateTime(2015, 05, 11),
                EindDatum = new DateTime(2015, 05, 15),
                Toestand = Aanvraagstatus.Afgekeurd
            };

            smurf10.Verlofaanvragen.Add(verlofAanvraag3);
            smurf20.Verlofaanvragen.Add(verlofAanvraag3);

            VerlofAanvraag verlofAanvraag4 = new VerlofAanvraag
            {
                AanvraagDatum = new DateTime(2015, 02, 28),
                StartDatum = new DateTime(2015, 06, 29),
                EindDatum = new DateTime(2015, 07, 03),
                Toestand = Aanvraagstatus.Afgekeurd
            };

            smurf4.Verlofaanvragen.Add(verlofAanvraag4);
            smurf14.Verlofaanvragen.Add(verlofAanvraag4);
            smurf24.Verlofaanvragen.Add(verlofAanvraag4);


            VerlofAanvraag verlofAanvraag5 = new VerlofAanvraag
            {
                AanvraagDatum = new DateTime(2015, 02, 28),
                StartDatum = new DateTime(2015, 08, 12),
                EindDatum = new DateTime(2015, 08, 19),
                Toestand = Aanvraagstatus.Afgekeurd
            };

            smurf2.Verlofaanvragen.Add(verlofAanvraag5);
            smurf12.Verlofaanvragen.Add(verlofAanvraag5);
            smurf22.Verlofaanvragen.Add(verlofAanvraag5);


            VerlofAanvraag verlofAanvraag6 = new VerlofAanvraag
            {
                AanvraagDatum = new DateTime(2015, 02, 28),
                StartDatum = new DateTime(2015, 08, 12),
                EindDatum = new DateTime(2015, 08, 19),
                Toestand = Aanvraagstatus.Ingediend
            };

            smurf8.Verlofaanvragen.Add(verlofAanvraag6);
            smurf16.Verlofaanvragen.Add(verlofAanvraag6);
            
            db.SaveChanges();


        }
    }
}
