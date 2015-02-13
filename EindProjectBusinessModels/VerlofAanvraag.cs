using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindProjectBusinessModels
{
    public enum Aanvraagstatus {Ingediend, Goedgekeurd, Afgekeurd, Geannuleerd }
    public class VerlofAanvraag
    {
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public DateTime AanvraagDatum { get; set; }
        public Aanvraagstatus Toestand { get; set; }
    }
}
