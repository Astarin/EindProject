using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindProjectBusinessModels
{
    public abstract class CollectieveSluiting
    {
         [ScaffoldColumn(false)]
        public int Id { get; set; }

         [Required(ErrorMessage = "Je moet een startdatum opgegeven")]
         [DataType(DataType.Date)]
        public DateTime StartDatum { get; set; }

         [Required(ErrorMessage = "Je moet een startdatum opgegeven")]
         [DataType(DataType.Text)]
        public string Omschrijving { get; set; }

         [Required]
        //[Range(typeof (bool),"true","false",ErrorMessage = "Duid aan of de sluiting al dan niet terugkerend is." )]
        public bool Terugkerend { get; set; }
    }

    public class Feestdag : CollectieveSluiting
    {

    }

    public class CollectiefVerlof : CollectieveSluiting
    {
        public DateTime EindDatum { get; set; }
    }
}