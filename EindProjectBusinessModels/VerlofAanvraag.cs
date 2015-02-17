using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindProjectBusinessModels
{
    public enum Aanvraagstatus { Ingediend, Goedgekeurd, Afgekeurd, Geannuleerd }

    public class VerlofAanvraag
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Je moet een startdatum opgegeven")]
        [DataType(DataType.Date)]
        public DateTime StartDatum { get; set; }

        [Required(ErrorMessage = "Je moet een Einddatum opgegeven")]
        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; }

        [ScaffoldColumn(false)]  // niet opvragen de aanvraagdatum moet in code aangemaakt worden(DataTime.now)
        public DateTime AanvraagDatum { get; set; }

        [ScaffoldColumn(false)]  // niet opvragen de aanvraagstatus moet in code aangemaakt worden
        public Aanvraagstatus Toestand { get; set; }

        [ScaffoldColumn(false)]
        public DateTime BehandelDatum { get; set; }

        [ScaffoldColumn(false)]
        public Werknemer BehandeldDoor { get; set; }

        // De aanvraagdatum word bij creatie aangemaakt.
        public VerlofAanvraag()
        {
            AanvraagDatum = DateTime.Now;
        }
    }
}
