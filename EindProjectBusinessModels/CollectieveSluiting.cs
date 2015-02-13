using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel
{
    public abstract class CollectieveSluiting
    {
        public DateTime StartDatum { get; set; }
        public string Omschrijving { get; set; }
        public bool Terugkerend { get; set; }
    }

    public class Feestdag : CollectieveSluiting
    {

    }

    public class CollectiefVerlof: CollectieveSluiting
    {
        public DateTime EindDatum { get; set; }
    }
}