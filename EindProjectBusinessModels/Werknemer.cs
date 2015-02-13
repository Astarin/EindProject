using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindProjectBusinessModels
{
    public class Werknemer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersoneelsNr { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Email { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public string Paswoord { get; set; }
        public Dictionary<int, int> VerlofDagenPerJaar { get; set; }
        [Required]
        public virtual Team Team { get; set; }
        public List<VerlofAanvraag> Verlofaanvragen { get; set; }
        public bool TeamLeader { get; set; }

        public Werknemer()
        {
            Verlofaanvragen = new List<VerlofAanvraag>();
        }
    }
}