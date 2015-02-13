using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel
{
    public class Team
    {
        public string Naam { get; set; }
        public string Code { get; set; }
        public int TeamLeider { get; set; }
        public List<Werknemer> Werknemers { get; set; }
    }
}