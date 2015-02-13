using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindProjectBusinessModels
{
    public class Team
    {
        public string Naam { get; set; }
        public int Code { get; set; }
        public Werknemer  TeamLeider { get; set; }
     
    }
}