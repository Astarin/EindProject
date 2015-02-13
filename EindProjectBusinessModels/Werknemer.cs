using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel
{
    public class Werknemer
    {
        public string Naam { get; set; }
        public string Vooornaam { get; set; }
        public string Email { get; set; }
        public DateTime Geboortedatum { get; set; }
        public int PersoneelsNr { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public string Paswoord { get; set; }
        public Dictionary<string, int> VerlofDagenPerJaar { get; set; }
    }
}