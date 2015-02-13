using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindProjectBusinessModels
{
    public class VerlofAanvraag
    {
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public DateTime AanvraagDatum { get; set; }
        public string Toestand { get; set; }
        public int Personeelsnr { get; set; }
    }
}
