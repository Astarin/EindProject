﻿using System;
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

            Team smurfTeam = new Team { Naam = "Smurfen" };

            db.Teams.Add(smurfTeam);
            db.SaveChanges();

            Dictionary<int, int> standaardVerlofDagen = new Dictionary<int, int>();
            standaardVerlofDagen.Add(2015, 20);
            standaardVerlofDagen.Add(2016, 21);

            Team anderSmurfTeam = new Team { Naam = "Nog Smurfen" };

            Werknemer smurf1 = new Werknemer
            {
                TeamLeader = true,
                Naam = "Smurf",
                Voornaam = "Lelijke",
                Email = "lelijke@smurf.com",
                Geboortedatum = new DateTime(1892, 09, 22),
                Adres = "Smurfenstraat 1",
                Postcode = "2340",
                Gemeente = "SmurfenDorp",
                Paswoord = "Hash",
                VerlofDagenPerJaar = standaardVerlofDagen,
                Team = smurfTeam,
            };

            db.Werknemers.Add(smurf1);

            db.Werknemers.Add(new Werknemer
            {
                Naam = "Smurf",
                Voornaam = "Dikke",
                Email = "dikke@smurf.com",
                Geboortedatum = new DateTime(1902, 09, 22),
                Adres = "Smurfenstraat 2",
                Postcode = "2340",
                Gemeente = "SmurfenDorp",
                Paswoord = "Hash",
                VerlofDagenPerJaar = standaardVerlofDagen,
                Team = smurfTeam,
            });

            Dictionary<int, int> nietStandaardVerlofDagen = new Dictionary<int, int>();
            nietStandaardVerlofDagen.Add(2015, 3);
            nietStandaardVerlofDagen.Add(2016, 11);

            db.Werknemers.Add(new Werknemer
            {
                Naam = "Smurf",
                Voornaam = "Smalle",
                Email = "smalle@smurf.com",
                Geboortedatum = new DateTime(1902, 03, 21),
                Adres = "Smurfenstraat 3",
                Postcode = "2340",
                Gemeente = "SmurfenDorp",
                Paswoord = "Hash",
                VerlofDagenPerJaar = nietStandaardVerlofDagen,
                Team = smurfTeam,
            });

            Werknemer tmpSmurf = new Werknemer
            {
                TeamLeader = true,
                Naam = "Smurf",
                Voornaam = "Korte",
                Email = "korte@smurf.com",
                Geboortedatum = new DateTime(1892, 09, 22),
                Adres = "Smurfenstraat 4",
                Postcode = "2340",
                Gemeente = "SmurfenDorp",
                Paswoord = "Hash",
                VerlofDagenPerJaar = nietStandaardVerlofDagen,
                Team = anderSmurfTeam,
            };

            db.Werknemers.Add(tmpSmurf);

            db.Werknemers.Add(new Werknemer
            {
                Naam = "Smurf",
                Voornaam = "lange",
                Email = "lange@smurf.com",
                Geboortedatum = new DateTime(1930, 05, 03),
                Adres = "Smurfenstraat 5",
                Postcode = "2340",
                Gemeente = "SmurfenDorp",
                Paswoord = "Hash",
                VerlofDagenPerJaar = standaardVerlofDagen,
                Team = anderSmurfTeam,
            });

            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Kerstmis", StartDatum = new DateTime(2015, 12, 25), Terugkerend = true });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Nieuwjaar", StartDatum = new DateTime(2015, 01, 01), Terugkerend = true });
            db.Sluitingsdagen.Add(new CollectiefVerlof { Omschrijving = "bouwverlof", StartDatum = new DateTime(2015, 07, 01), EindDatum = new DateTime(2015, 07, 22), Terugkerend = false });
            db.Sluitingsdagen.Add(new Feestdag { Omschrijving = "Nationale Smurfendag", StartDatum = new DateTime(2015, 07, 21), Terugkerend = true });

            VerlofAanvraag verlofAanvraag1 = new VerlofAanvraag
            {
                AanvraagDatum = new DateTime(2015, 02, 13),
                StartDatum = new DateTime(2015, 03, 20),
                EindDatum = new DateTime(2015, 03, 27),
                Toestand = Aanvraagstatus.Ingediend
            };

            tmpSmurf.Verlofaanvragen.Add(verlofAanvraag1);

            VerlofAanvraag verlofaanvraag2 = new VerlofAanvraag
            {
                AanvraagDatum = new DateTime(2015, 01, 13),
                StartDatum = new DateTime(2015, 02, 16),
                EindDatum = new DateTime(2015, 02, 21),
                Toestand = Aanvraagstatus.Goedgekeurd
            };

            tmpSmurf.Verlofaanvragen.Add(verlofaanvraag2);

            VerlofAanvraag VerlofAanvraag3 = new VerlofAanvraag
            {
                AanvraagDatum = new DateTime(2015, 02, 13),
                StartDatum = new DateTime(2015, 04, 13),
                EindDatum = new DateTime(2015, 04, 27),
                Toestand = Aanvraagstatus.Ingediend
            };

            smurf1.Verlofaanvragen.Add(VerlofAanvraag3);

            db.SaveChanges();
        }
    }
}